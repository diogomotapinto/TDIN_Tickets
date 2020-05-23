<%@ Page Language="C#" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>List</title>
    <style type="text/css">
        body {
            height: 100vh;
            display: flex;
            justify-content: space-around;
            align-items: center;
            flex-direction: column;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1 runat="server">Author</h1>
        <asp:DropDownList ID="DropDownList1" OnSelectedIndexChanged="onChange" runat="server" DataTextField="Name" DataValueField="Id">
        </asp:DropDownList>
        <asp:Button ID="Button2" runat="server" OnClick="onChange" Text="See Tickets" />
        <div>
            <asp:GridView ID="GridView1" runat="server"></asp:GridView>
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="Red"></asp:Label>
        </div>

    </form>
</body>
</html>
