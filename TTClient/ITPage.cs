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
        public MessageQueue qeue;
        private System.Messaging.Message[] messages;
        string user;
        public ITPage(string user, string id)
        {
            InitializeComponent();
            proxy = new TTProxy();
            qeue = new MessageQueue();
            qeue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            this.user = user;
            // get tickets from a user and display them
            DataTable tickets = proxy.GetTickets(id);
            dataGridView1.DataSource = tickets;

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
            receiveTB.Text = (string)msq.Body;
        }

        private void sendMsg(object sender, EventArgs e)
        {
            qeue.Send((string)sendTB.Text.Trim());
        }
    }
}
