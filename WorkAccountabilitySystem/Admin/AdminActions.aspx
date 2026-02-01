<%@ Page Language="C#" 
    AutoEventWireup="true"
    CodeBehind="AdminActions.aspx.cs" Inherits="WorkAccountabilitySystem.Admin.AdminActions" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Action</title>
</head>
<body>
<form id="form1" runat="server">

<h3>Admin Work Action</h3>

<asp:TextBox ID="txtWorkId" runat="server"
    Placeholder="WorkId" />

<br /><br />

<asp:DropDownList ID="ddlAction" runat="server">
    <asp:ListItem Text="ForceClosed" Value="ForceClosed" />
    <asp:ListItem Text="Reopen" Value="Reopen" />
</asp:DropDownList>

<br /><br />

<asp:TextBox ID="txtReason" runat="server"
    Width="400" TextMode="MultiLine" Rows="4"
    Placeholder="Reason (mandatory)" />

<br /><br />

<asp:Button ID="btnSubmit" runat="server"
    Text="Submit Action"
    OnClick="Submit_Click" />

</form>
</body>
</html>