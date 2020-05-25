using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTService;

namespace TTClient
{
    public partial class Register : Form
    {
        TTProxy proxy = new TTProxy();
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string role = comboBox1.SelectedItem.ToString();

            if(proxy.AddUser(username, role))
            {
                Login page = new Login();
                page.Tag = this;
                page.Show(this);
                Hide();
            }
            else
            {
                textBox1.Text = "";
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("employee");
            comboBox1.Items.Add("solver");
            comboBox1.Items.Add("external_solver");
        }
    }
}
