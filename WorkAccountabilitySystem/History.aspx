<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="WorkAccountabilitySystem.History" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Work History</title>
</head>
<body>
<form id="form1" runat="server">

<h2>Work History</h2>

<asp:Repeater ID="rptHistory" runat="server">
<ItemTemplate>
    <div style="border:1px solid #ccc; padding:10px; margin-bottom:10px">
        <b>User:</b> <%# Eval("Username") %><br />
        <b>Status:</b> <%# Eval("Status") %><br />
        <b>User Remark:</b> <%# Eval("UserRemark") %><br />
        <b>Date:</b> <%# Eval("CreatedDate") %><br />
        <%# Eval("AdminRemark") %>
    </div>
</ItemTemplate>
</asp:Repeater>

<hr />

<h3>Admin Actions</h3>

<asp:Repeater ID="rptAdmin" runat="server">
<ItemTemplate>
    <div style="border:1px dashed #999; padding:8px; margin-bottom:6px">
        <b>Action:</b> <%# Eval("ActionType") %><br />
        <b>Reason:</b> <%# Eval("Reason") %><br />
        <b>By:</b> <%# Eval("Username") %><br />
        <b>Date:</b> <%# Eval("CreatedDate") %>
    </div>
</ItemTemplate>
</asp:Repeater>

</form>
</body>
</html>