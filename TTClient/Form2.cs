using System.Data;
using System.ServiceModel;
using System.Windows.Forms;
using TTService;

namespace TTClient
{
    public partial class Form2 : Form
    {
        TTProxy proxy;

        public Form2()
        {
            int k;

            InitializeComponent();

            proxy = new TTProxy();
            DataTable users = proxy.GetUsers();
            for (k = 1; k < users.Rows.Count; k++)
                listBox1.Items.Add(users.Rows[k][1]);   // Row 0 is empty; the author name is in column 1
        }

        private void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            string user = (listBox1.SelectedIndex + 1).ToString();   // the Author id as a string
            DataTable tickets = proxy.GetTickets(user);
            dataGridView1.DataSource = tickets;             // display all the tickets for the specified user in a data grid
        }
    }



}
