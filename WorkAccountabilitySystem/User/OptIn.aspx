<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OptIn.aspx.cs" Inherits="WorkAccountabilitySystem.User.OptIn" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Opt In to Work</title>
</head>
<body>
<form id="form1" runat="server">

<h3>Opt In to Work</h3>

<asp:Label runat="server" Text="Work ID"></asp:Label><br />

<asp:TextBox ID="txtWorkId" runat="server" />

<br /><br />
    <!-- ADDED: message label for feedback -->
<asp:Label ID="lblMsg" runat="server" ForeColor="Red" /> <!-- ADDED -->

<br /><br />

<asp:Button ID="btnOptIn" runat="server"
    Text="Opt In"
    OnClick="OptIn_Click" />

</form>
</body>
</html>
