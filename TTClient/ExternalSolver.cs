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

namespace TTClient
{
    public partial class ExternalSolver : Form
    {
        private string qeuePath = @".\private$\myfirstq";
        MessageQueue queue;
        private System.Messaging.Message[] messages;
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

        private void ExternalSolver_Load(object sender, EventArgs e)
        {
            initilizeQeue();

            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

            try
            {
                messages = queue.GetAllMessages();
                foreach (System.Messaging.Message msq in messages)
                {
                    Console.WriteLine("--------------------\r\n");
                    Console.WriteLine("Body = " + (string)msq.Body);
                    Console.WriteLine("\r\n");
                    Console.WriteLine("Id = " + msq.Id);
                    Console.WriteLine("\r\n--------------------\r\n");
                }
            }
            catch (Exception)
            {

            }



        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
