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

namespace TTClient
{
    public partial class ExternalSolver : Form
    {
        private string qeuePath = @".\private$\myfirstq";
        MessageQueue queue;
        private System.Messaging.Message[] messages;
        private int selectedTicketIndex = 0;
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

        private void updateNewIndex(object sender, EventArgs e)
        {
            selectedTicketIndex = listBox1.SelectedIndex;
        }

        private void loadMessagesToList()
        {
            Ticket[] a = new Ticket[] { new Ticket("a", "b", "titlo1", "hello", new DateTime()), new Ticket("d", "b", "titlo2", "hello", new DateTime()), new Ticket("e", "b", "titlo3", "hello", new DateTime()) };

            for (int i = 0; i < a.Length; i++)
            {
                listBox1.Items.Insert(i, a[i].Title);
            }

            listBox1.SelectedIndexChanged += updateNewIndex;
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

        }
    }
}
