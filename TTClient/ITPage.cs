﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Messaging;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Threading;
using TTService;
namespace TTClient
{
    public partial class ITPage : Form
    {
        TTProxy proxy;
        private string qeuePath = @".\private$\ticketsqeue";
        private string notificationqeuePath = @".\private$\notification";
        public MessageQueue qeue;
        public MessageQueue notificationQeue;
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
            notificationQeue = new MessageQueue();
            qeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Ticket) });
            notificationQeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            userT = proxy.GetUsers();
            this.user = user;
            // get tickets from a user and display them
            DataTable tickets = filterData();
            dataGridView1.DataSource = tickets;
            ticketList = Ticket.getTickets(tickets);
            userDict = new Dictionary<string, string>();

            usersDropDown();
            ticketsDropDown();


            initializeQeue(qeue, qeuePath);
            initializeQeue(notificationQeue, notificationqeuePath);

            notificationQeue.ReceiveCompleted += notificationReceiver;
            notificationQeue.BeginReceive();
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
            DataTable tickets = proxy.GetTicketsAssign(loggedid);
            foreach (DataRow row in tickets.Rows)
            {
                string elemState = row["State"].ToString();
                if (!(elemState == "unassigned" || elemState == user || elemState == "solved" || elemState == "waiting for answers"))
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

        [STAThread]
        private void notificationReceiver(object src, ReceiveCompletedEventArgs rcea)
        {
            System.Messaging.Message msg = notificationQeue.EndReceive(rcea.AsyncResult);

            string received = (string)msg.Body;

            /*  this.Invoke(() => { this.label1.Text = "Departamente respondeu ao ticket " + received; });*/



            notificationQeue.BeginReceive();

            refreshDataTB(loggedid);
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
                this.statusLabel.Text = "Tem de selecionaruma linha";
                return;
            }

            string id = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            string secundaryTitle = textBox1.Text;

            string secundaryDescription = textBox2.Text;

            DateTime moment = DateTime.Now;

            Ticket toSend = new Ticket(user, "example@gmail.com", secundaryTitle, secundaryDescription, moment, id);

            if (sendMessageToExternalSolver(toSend))
            {
                this.statusLabel.Text = "Ticket enviado";
                proxy.AddWating(id, secundaryTitle, secundaryDescription);
                refreshDataTB(loggedid);
            }
            else
            {
                this.statusLabel.Text = "Falha ao enviar ticket";
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
