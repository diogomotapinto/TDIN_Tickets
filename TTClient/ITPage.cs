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
        string loggedid;
        DataTable userT;
        string selectCell;
        Dictionary<string, string> userDict;
        ArrayList ticketList;
        public ITPage(string user, string id)
        {
            InitializeComponent();
            loggedid = id;
            selectCell = null;
            proxy = new TTProxy();
            qeue = new MessageQueue();
            userT = proxy.GetUsers();
            qeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            this.user = user;
            // get tickets from a user and display them
            DataTable tickets = filterData();
            dataGridView1.DataSource = tickets;
            ticketList = Ticket.getTickets(tickets);
            userDict = new Dictionary<string, string>();

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

        public DataTable filterData()
        {
            DataTable tickets = proxy.GetTicketsAssign(loggedid);
            foreach (DataRow row in tickets.Rows)
            {
                string elemState = row["State"].ToString();
                if (!(elemState == "unassigned" || elemState == user || elemState == "solved"))
                {
                    row.Delete();
                }
            }
            return tickets;
        }

        public void refreshDataTB(string id)
        {
            DataTable tickets = filterData();
            dataGridView1.DataSource = tickets;
        }

        public void ticketsDropDown()
        {
            var titleList = new List<string>();
            foreach (Ticket elem in ticketList)
            {
                if (elem.State == "unassigned")
                {
                    titleList.Add(elem.Id);
                }
            }
            //Setup data binding
            ticketsCB.DataSource = titleList;
            ticketsCB.DisplayMember = "Title";
        }

        public void usersDropDown()
        {
            var namesList = new List<String>();
            DataColumn dataColumn = userT.Columns["Name"];
            DataColumn idColumn = userT.Columns["Id"];

            foreach (DataRow row in userT.Rows)
            {
                String elemName = row.Field<string>(dataColumn);
                int elemId = row.Field<int>(idColumn);
                userDict.Add(elemName, elemId.ToString());
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

        private void onSubmit(object sender, EventArgs e)
        {
            // buscar id do utilizador
            string userId = userDict[usersCB.Text];
            string userName = usersCB.Text;
            // buscar id to ticket 
            Ticket tic = null;
            foreach (Ticket elem in ticketList)
            {
                if (elem.Id == ticketsCB.Text)
                {
                    tic = elem;
                }
            }
            proxy.updateAssigned(userName, tic.Id);
            refreshDataTB(loggedid);
        }



        private void SubmitAnswer(object sender, EventArgs e)
        {
            string answer = answerBox.Text;
            if (selectLabel != null)
            {
                proxy.AddAnswer(answer, selectCell);
                refreshDataTB(loggedid);
                answerBox.Text = "";
            }
        }

        private void SelectCell(object sender, DataGridViewCellEventArgs e)
        {
            selectCell = (dataGridView1.CurrentCell.RowIndex + 1).ToString();
            selectLabel.Text = "Id of the Ticket selected: " + selectCell + "";
        }
    }
}
