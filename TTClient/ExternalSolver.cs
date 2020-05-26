using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Messaging;
using TTService;
using System.Collections;
using System.ServiceModel.Channels;

namespace TTClient
{
    public partial class ExternalSolver : Form
    {
        private string qeuePath = @".\private$\ticketsqeue";
        private string notificationqeuePath = @".\private$\notification";
        MessageQueue queue = new MessageQueue(), notificationQeue = new MessageQueue();
        private System.Messaging.Message[] messages;
        private int selectedTicketIndex = -1;
        private ArrayList availableTickets = new ArrayList();
        TTProxy proxy = new TTProxy();

        public ExternalSolver(User currentUser)
        {
            InitializeComponent();
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

        private void loadMessagesToList()
        {
            listBox1.Items.Clear();


            int index = 0;
            foreach (Ticket t in availableTickets)
            {
                listBox1.Items.Insert(index, t.Title);
                index++;
            }
        }

        private void QueueReceiver(object src, ReceiveCompletedEventArgs rcea)
        {
            System.Messaging.Message msg = queue.EndReceive(rcea.AsyncResult);
            
            Ticket received = (Ticket)msg.Body;

            availableTickets.Add(received);

            this.label1.Text = "Novo ticket: " + received.Title;

            queue.BeginReceive();

            loadMessagesToList();
        }

        private void ExternalSolver_Load(object sender, EventArgs e)
        {
            initializeQeue(queue, qeuePath);
            initializeQeue(notificationQeue, notificationqeuePath);

            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Ticket) });
            notificationQeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            messages = queue.GetAllMessages();

            foreach (System.Messaging.Message msq in messages)
            {
                availableTickets.Add(msq.Body);
                // Console.WriteLine("Id = " + msq.Id);
            }

            loadMessagesToList();

            queue.ReceiveCompleted += QueueReceiver;
            queue.BeginReceive();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedTicketIndex = listBox1.SelectedIndex;
            displayTicketInfo();

        }

        private void displayTicketInfo()
        {
            Ticket toDisplay = (Ticket)availableTickets[selectedTicketIndex];

            textBox2.Text = "";

            textBox2.Text += "Title: " + toDisplay.Title;

            textBox2.Text += "\r\n\r\nDescription: " + toDisplay.Description;

            textBox2.Text += "\r\n\r\nCreated by " + toDisplay.AuthorName + " on " + toDisplay.Creation.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(selectedTicketIndex == -1)
            {
                this.label1.Text = "Tem de selecionar um ticket";
                return;
            }

            Ticket toAnswer = (Ticket)availableTickets[selectedTicketIndex];

            if(proxy.addSecondaryAnswer(toAnswer.Id, textBox1.Text))
            {
                this.label1.Text = "Resposta enviada";
                sendNotification(toAnswer.Id);
            }
            else
            {
                this.label1.Text = "Resposta não enviada";
            }
            
        }

        private void sendNotification(string id)
        {
            try
            {
                this.notificationQeue.Send(id);
            }
            catch (MessageQueueException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex1)
            {
                Console.WriteLine(ex1.Message);
            }
        }
    }
}
