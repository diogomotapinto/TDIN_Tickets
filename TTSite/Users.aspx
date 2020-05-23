<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Users" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="user" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" OnClick="onclick" Text="Add User" />
        </div>
    </form>
</body>
</html>
