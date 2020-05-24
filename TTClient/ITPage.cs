using System;
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
        MessageQueue queue;
        private System.Messaging.Message[] messages;
        string user;
        public ITPage(string user, string id)
        {
            InitializeComponent();
            proxy = new TTProxy();
            this.user = user;
            // get tickets from a user and display them
            DataTable tickets = proxy.GetTickets(id);
            dataGridView1.DataSource = tickets;

            if (MessageQueue.Exists(qeuePath.Trim()))
            {
                Console.WriteLine("MessageQeue Exists");
            }
            else
            {
                Console.WriteLine("Message Qeue Doesn't Exists");
                CreateQeue();
            }

        }

        public void CreateQeue()
        {
            try
            {
                MessageQueue.Create(qeuePath.Trim());
                Console.WriteLine("Message Qeue Created");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }


    }
}
