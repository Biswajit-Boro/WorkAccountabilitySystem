<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WorkAccountabilitySystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style>
        body { font-family: Arial; background:#f4f4f4; }
        .box {
            width:300px; margin:100px auto; padding:20px;
            background:white; border:1px solid #ccc;
        }
        input, button {
            width:100%; margin-top:10px; padding:8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="box">
        <h3>System Login</h3>

        <asp:TextBox ID="txtUser" runat="server" Placeholder="Username" />
        <asp:TextBox ID="txtPass" runat="server"
                     TextMode="Password" Placeholder="Password" />

        <asp:Button ID="btnLogin" runat="server"
                    Text="Login" OnClick="Login_Click" />
    </div>
    </form>
</body>
</html>
