using System;
using System.Data;
using System.ServiceModel;
using TTService;

public partial class Users : System.Web.UI.Page
{
    TTProxy proxy;
    protected void Page_Load(object sender, EventArgs e)
    {
        proxy = new TTProxy();
    }

    public void onclick(object sender, EventArgs e)
    {
        string username = user.Text;
        proxy.AddUser(username, "employee");
    }
}