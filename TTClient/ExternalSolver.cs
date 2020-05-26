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
        MessageQueue queue;
        private System.Messaging.Message[] messages;
        private int selectedTicketIndex = 0;
        private ArrayList availableTickets = new ArrayList();

        public ExternalSolver(User currentUser)
        {
            InitializeComponent();
        }

        private void initilizeQeue()
        {
            try
            {
                if (!MessageQueue.Exists(qeuePath.Trim()))
                {
                    queue = new MessageQueue(qeuePath.Trim());
                    Console.WriteLine("Não existia. criada");

                    this.label1.Text = "Fila de mensagens criada.";
                }
                else
                {
                    queue.Path = qeuePath;
                    this.label1.Text = "Fila de mensagens já existente criada.";
                }

                queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(Ticket) });
            }
            catch (MessageQueueException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch (Exception ex1)
            {
                System.Console.WriteLine(ex1.Message);
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
            initilizeQeue();

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
    }
}
