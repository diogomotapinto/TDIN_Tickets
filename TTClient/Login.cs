using System;
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
        public string user;
        public Login()
        {


            InitializeComponent();

            proxy = new TTProxy();
            dataTable = proxy.GetUsers();
            var namesList = new List<String>();
            DataColumn dataColumn = dataTable.Columns["Name"];


            foreach (DataRow row in dataTable.Rows)
            {
                String elemName = row.Field<string>(dataColumn);
                namesList.Add(elemName);
            }

            //Setup data binding
            comboBox1.DataSource = namesList;
            comboBox1.DisplayMember = "Name";

        }

        private void onClick(object sender, EventArgs e)
        {
            user = comboBox1.Text;
            ITPage iTPage = new ITPage(user);
            iTPage.Tag = this;
            iTPage.Show(this);
            Hide();

        }
    }



}
