using System.Data;
using System.Windows.Forms;
using TTService;
namespace TTClient
{
    public partial class ITPage : Form
    {
        TTProxy proxy;
        string user;
        public ITPage(string user)
        {
            InitializeComponent();
            proxy = new TTProxy();
            this.user = user;
            // get tickets from a user and display them
            DataTable tickets = proxy.GetTicketsAssign("1");
            dataGridView1.DataSource = tickets;
        }

      
    }
}
