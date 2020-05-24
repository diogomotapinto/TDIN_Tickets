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

namespace TTClient
{
    public partial class ExternalSolver : Form
    {
        private string qeuePath = @".\private$\myfirstq";
        MessageQueue queue;
        private System.Messaging.Message[] messages;
        private int selectedTicketIndex = 0;
        private ArrayList availableTickets = new ArrayList();

        public ExternalSolver()
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
                    textBox1.Text = "Não existia. criada";
                    Console.WriteLine("Não existia. criada");
                }
                else
                {
                    queue.Path = qeuePath;
                    textBox1.Text = "Ja existia";
                    Console.WriteLine("já existia");
                }
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
            availableTickets.Add(new Ticket("Duarte", "dnc.1@gmail.com", "Não consigo ter VPN", "Já tentei de tudo mas esta merda nao da.", new DateTime(2020, 2, 11)));
            availableTickets.Add(new Ticket("d", "b", "titlo2", "hello", new DateTime()));
            availableTickets.Add(new Ticket("e", "b", "titlo3", "hello", new DateTime()));
            int index = 0;
            foreach (Ticket t in availableTickets)
            {
                listBox1.Items.Insert(index, t.Title);
                index++;
            }
        }


        private void ExternalSolver_Load(object sender, EventArgs e)
        {
            initilizeQeue();

            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            messages = queue.GetAllMessages();

            foreach (System.Messaging.Message msq in messages)
            {
                Console.WriteLine("--------------------\r\n");
                Console.WriteLine("Body = " + (string)msq.Body);
                Console.WriteLine("\r\n");
                Console.WriteLine("Id = " + msq.Id);
                Console.WriteLine("\r\n--------------------\r\n");
            }

            loadMessagesToList();
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
