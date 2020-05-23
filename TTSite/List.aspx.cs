using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTService;

public partial class List : System.Web.UI.Page
{
    TTProxy proxy;
    protected void Page_Load(object sender, EventArgs e)
    {
        proxy = new TTProxy();
        if (!Page.IsPostBack)
        {                           // only on first request of a session
            DropDownList1.DataSource = proxy.GetUsers();
            DropDownList1.DataBind();
        }
    }

    public void onChange(object sender, EventArgs e)
    {
        if (DropDownList1.SelectedIndex > 0)
        {
            GridView1.DataSource = proxy.GetTickets(DropDownList1.SelectedValue);
            GridView1.DataBind();
            GridView1.Visible = true;
            Label2.Text = "";
        }
        else
        {
            GridView1.Visible = false;
            Label2.Text = "Select an Author!";
        }
    }
}