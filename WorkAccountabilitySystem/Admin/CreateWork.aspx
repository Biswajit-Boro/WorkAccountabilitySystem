<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateWork.aspx.cs" Inherits="WorkAccountabilitySystem.Admin.CreateWork" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Work</title>
</head>
<body>
    <form id="form1" runat="server">
    <h3>Create Work Item</h3>

<asp:TextBox ID="txtDesc" runat="server"
    Width="400" TextMode="MultiLine" Rows="5" />

<br /><br />

<asp:DropDownList ID="ddlPriority" runat="server">
    <asp:ListItem Text="Low" Value="Low" />
    <asp:ListItem Text="Medium" Value="Medium" Selected="True" />
    <asp:ListItem Text="High" Value="High" />
</asp:DropDownList>

<br /><br />

<asp:Button ID="btnCreate" runat="server"
    Text="Create"
    OnClick="Create_Click" />

    </form>
</body>
</html>
