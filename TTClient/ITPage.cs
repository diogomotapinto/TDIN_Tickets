using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Threading;
using TTService;
using System.Timers;

namespace TTClient
{
    public partial class ITPage : Form
    {
        User currentUser;
        TTProxy proxy;
        private string qeuePath = @".\private$\ticketsqeue";
        private string notificationqeuePath = @".\private$\notification";
        public MessageQueue qeue;
        public MessageQueue notificationQeue;
        private System.Messaging.Message[] messages;
        ArrayList ticketList;
        ArrayList userList;
        Ticket selectedTicket;
        User selectedUser;
        
        public ITPage(User u)
        {
            InitializeComponent();
            currentUser = u;
            proxy = new TTProxy();

            userList = new ArrayList();

            // get tickets from a user and display them
        }

        private void initializeQeue(MessageQueue m, string path)
        {
            if (MessageQueue.Exists(path))
            {
                Console.WriteLine(path + " MessageQeue Exists");
                m.Path = path;

            }
            else
            {
                Console.WriteLine("Message Qeue Doesn't Exists");
                CreateQeue(m, path);
            }
        }

        public DataTable filterData()
        {
            DataTable tickets = proxy.GetTicketsAssign(currentUser.Id.ToString());
            foreach (DataRow row in tickets.Rows)
            {
                string elemState = row["State"].ToString();
                if (!(elemState == "unassigned" || elemState == currentUser.Username || elemState == "solved" || elemState == "waiting for answers"))
                {
                    row.Delete();
                }
            }
            return tickets;
        }

        public void loadUsers()
        {
            DataTable dataTable = proxy.GetUsers();

            DataColumn dataColumn = dataTable.Columns["Name"];
            DataColumn idColumn = dataTable.Columns["Id"];
            DataColumn roleColumn = dataTable.Columns["Role"];

            foreach (DataRow row in dataTable.Rows)
            {
                String elemName = row.Field<string>(dataColumn);
                int elemId = row.Field<int>(idColumn);
                string role = row.Field<string>(roleColumn);

                userList.Add(new TTService.User(elemId, elemName, role));
            }
        }

        public void usersDropDown()
        {
            int index = 0;
            foreach (User u in userList)
            {
                usersCB.Items.Insert(index, u.Username);
                index++;
            }
        }

        public void CreateQeue(MessageQueue m, string path)
        {
            try
            {
                m = MessageQueue.Create(path);
                Console.WriteLine(path + " Message Qeue Created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void notificationReceiver(object src, ReceiveCompletedEventArgs rcea)
        {
            /*System.Messaging.Message msg = notificationQeue.EndReceive(rcea.AsyncResult);

            string received = (string)msg.Body;

            this.label1.Text = "Departamente respondeu ao ticket " + msg;

              */

            fetchTickets();

            Console.WriteLine("tickets fetched");

            notificationQeue.BeginReceive();
        }

        private bool sendMessageToExternalSolver(Ticket t)
        {
            try
            {
                this.qeue.Send(t);
            }
            catch (MessageQueueException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.Message);
                return false;
            }
            return true;
        }

        private void sendMsg(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex == -1)
            {
                return;
            }

            string id = selectedTicket.Id;

            string secundaryTitle = textBox1.Text;

            string secundaryDescription = textBox2.Text;

            DateTime moment = DateTime.Now;

            Ticket toSend = new Ticket(currentUser.Username, "example@gmail.com", secundaryTitle, secundaryDescription, moment, id);
            
            if(sendMessageToExternalSolver(toSend))
            {
                this.statusLabel.Text = "Ticket enviado";
                proxy.AddWating(id, secundaryTitle, secundaryDescription);
                fetchAndAddTicketsToList();
            }
            else
            {
                this.statusLabel.Text = "Falha ao enviar ticket";
            }

        }

        private void onSubmit(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1 &&  usersCB.SelectedIndex != -1)
            {
                proxy.updateAssigned(selectedUser.Username, selectedTicket.Id);
                fetchAndAddTicketsToList();
            }
        }

        private void SubmitAnswer(object sender, EventArgs e)
        {
            string answer = answerBox.Text;
            if (listBox1.SelectedIndex != -1)
            {
                proxy.AddAnswer(answer, selectedTicket.Id);
                fetchAndAddTicketsToList();
                answerBox.Text = "";
            }
        }

        private void loadTicketsToListBox()
        {
            listBox1.Items.Clear();
            int index = 0;

            foreach(Ticket t in ticketList)
            {
                listBox1.Items.Insert(index,t.Id.ToString() + " - " + t.Title + " - " + t.State);
                index++;
            }
        }

        private void fetchAndAddTicketsToList()
        {
            fetchTickets();
            loadTicketsToListBox();
        }

        private void fetchTickets()
        {
            Console.WriteLine("Fetching tickets");
            DataTable tickets = filterData();
            ticketList = Ticket.getTickets(tickets);
        }

        private void ITPage_Load(object sender, EventArgs e)
        {
            fetchAndAddTicketsToList();
            loadUsers();

            usersDropDown();

            qeue = new MessageQueue();
            notificationQeue = new MessageQueue();
            qeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Ticket) });
            notificationQeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            initializeQeue(qeue, qeuePath);
            initializeQeue(notificationQeue, notificationqeuePath);

            notificationQeue.ReceiveCompleted += notificationReceiver;
            notificationQeue.BeginReceive();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fetchAndAddTicketsToList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTicket = (Ticket)ticketList[listBox1.SelectedIndex];
            displayTicketInfo();
            selectLabel.Text = "Id of the Ticket selected: " + selectedTicket.Id + "";
        }

        private void displayTicketInfo()
        {
            textBox3.Text = "";

            textBox3.Text += "Title: " + selectedTicket.Title;

            textBox3.Text += "\r\n\r\nDescription: " + selectedTicket.Description;

            textBox3.Text += "\r\n\r\nCreated by " + selectedTicket.AuthorName + " on " + selectedTicket.Creation.ToString();

            if (selectedTicket.SecundaryQuestionTitle.Length != 0)
            {
                textBox3.Text += "\r\n\r\nSecundary quetion title: " + selectedTicket.SecundaryQuestionTitle;
                textBox3.Text += "\r\n\r\nSecundary quetion Description: " + selectedTicket.SecundaryQuestionDescription;
                textBox3.Text += "\r\n\r\nSecundary quetion Answer: " + selectedTicket.SecundaryQuestionAnswer;
            }
        }

        private void usersCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedUser = (User)userList[usersCB.SelectedIndex];
        }
    }
}
