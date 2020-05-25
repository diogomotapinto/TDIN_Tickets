using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Messaging;
using System.Windows.Forms;
using TTService;
namespace TTClient
{
    public partial class ITPage : Form
    {
        TTProxy proxy;
        private string qeuePath = @".\private$\ticketsqeue";
        public MessageQueue qeue;
        private System.Messaging.Message[] messages;
        string user;
        ArrayList ticketList;
        public ITPage(string user, string id)
        {
            InitializeComponent();
            proxy = new TTProxy();
            qeue = new MessageQueue();
            qeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Ticket) });
            this.user = user;
            // get tickets from a user and display them
            DataTable tickets = proxy.GetTicketsAssign(id);
            dataGridView1.DataSource = tickets;
            ticketList = Ticket.getTickets(tickets);
            ticketList = ticketList;

            usersDropDown();
            ticketsDropDown();


            if (MessageQueue.Exists(qeuePath.Trim()))
            {
                Console.WriteLine("MessageQeue Exists");
                qeue.Path = qeuePath.Trim();

            }
            else
            {
                Console.WriteLine("Message Qeue Doesn't Exists");
                CreateQeue();
            }

        }

        public void ticketsDropDown()
        {
            var titleList = new List<string>();
            foreach (Ticket elem in ticketList)
            {
                if (elem.State == "unassigned")
                {
                    titleList.Add(elem.AuthorName);
                }
            }
            //Setup data binding
            ticketsCB.DataSource = titleList;
            ticketsCB.DisplayMember = "Title";
        }

        public void usersDropDown()
        {
            DataTable userT = proxy.GetUsers();
            var namesList = new List<String>();
            DataColumn dataColumn = userT.Columns["Name"];
            DataColumn idColumn = userT.Columns["Id"];

            foreach (DataRow row in userT.Rows)
            {
                String elemName = row.Field<string>(dataColumn);
                int elemId = row.Field<int>(idColumn);

                namesList.Add(elemName);
            }

            //Setup data binding
            usersCB.DataSource = namesList;
            usersCB.DisplayMember = "Name";
        }

        public void CreateQeue()
        {
            try
            {
                MessageQueue.Create(qeuePath.Trim());
                qeue.Path = qeuePath.Trim();
                Console.WriteLine("Message Qeue Created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        private void receiveMsg(object sender, EventArgs e)
        {
            System.Messaging.Message msq = qeue.Receive(new TimeSpan(0, 0, 2));
            msq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            /*receiveTB.Text = (string)msq.Body;*/
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
            /*qeue.Send((string)sendTB.Text.Trim());*/
            if (dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            string secundaryTitle = textBox1.Text;

            string secundaryDescription = textBox2.Text;

            DateTime moment = DateTime.Now;

            Ticket toSend = new Ticket(user, "example@gmail.com", secundaryTitle, secundaryDescription, moment);
            
            if(sendMessageToExternalSolver(toSend))
            {
                Console.WriteLine("Ticket enviado");
            }
            else
            {
                Console.WriteLine("Não enviado");
            }

        }

        private void onSubmit(object sender, EventArgs e)
        {

        }
    }
}
