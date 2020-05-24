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
        public string user;
        Dictionary<string, string> dic;
        public Login()
        {


            InitializeComponent();

            proxy = new TTProxy();
            dataTable = proxy.GetUsers();
            var namesList = new List<String>();
            dic = new Dictionary<string, string>();
            DataColumn dataColumn = dataTable.Columns["Name"];
            DataColumn idColumn = dataTable.Columns["Id"];

            foreach (DataRow row in dataTable.Rows)
            {
                String elemName = row.Field<string>(dataColumn);
                int elemId = row.Field<int>(idColumn);
                dic.Add(elemName, elemId.ToString());
                namesList.Add(elemName);
            }

            //Setup data binding
            comboBox1.DataSource = namesList;
            comboBox1.DisplayMember = "Name";

        }

        private void onClick(object sender, EventArgs e)
        {
            user = comboBox1.Text;
            ITPage iTPage = new ITPage(user, dic[user]);
            iTPage.Tag = this;
            iTPage.Show(this);
            Hide();

        }
    }



}
