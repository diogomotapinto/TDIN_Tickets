using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Windows.Forms;
using TTService;

namespace TTClient
{
    public partial class Login : Form
    {
        TTProxy proxy;
        DataTable dataTable;
        ArrayList users = new ArrayList();
        public Login()
        {


            InitializeComponent();

            proxy = new TTProxy();
            dataTable = proxy.GetUsers();
            var namesList = new List<String>();
            DataColumn dataColumn = dataTable.Columns["Name"];
            DataColumn idColumn = dataTable.Columns["Id"];
            DataColumn roleColumn = dataTable.Columns["Role"];

            foreach (DataRow row in dataTable.Rows)
            {
                String elemName = row.Field<string>(dataColumn);
                int elemId = row.Field<int>(idColumn);
                string role = row.Field<string>(roleColumn);

                users.Add(new TTService.User(elemId, elemName, role));
            }

            foreach(User user in users)
            {
                namesList.Add(user.Username);
            }

            //Setup data binding
            comboBox1.DataSource = namesList;
            comboBox1.DisplayMember = "Name";

        }

        private void onClick(object sender, EventArgs e)
        {
            User selected = (TTService.User)users[comboBox1.SelectedIndex];
            
            Form toShow = selected.Role.Equals("external_solver") ? (Form)new ExternalSolver(selected) : new ITPage(selected);

            toShow.Tag = this;
            toShow.Show(this);
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register page = new Register();
            page.Tag = this;
            page.Show(this);
            Hide();
        }
    }



}
