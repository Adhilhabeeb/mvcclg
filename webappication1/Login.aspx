<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="webappication1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
       <form id="form1" runat="server">
        <div>
            <h2>Login Page</h2>
            <label for="email">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" />
            <br />
            <label for="password">Password:</label>
            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" />
            <br />
            <asp:Button ID="btnLogin" Text="Login" runat="server" OnClick="btnLogin_Click" />
            <br />
            <asp:Label ID="lblMessage" ForeColor="Red" runat="server" />
        </div>
    </form>
</body>
</html>
