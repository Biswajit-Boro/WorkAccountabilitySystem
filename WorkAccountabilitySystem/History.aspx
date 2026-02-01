<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="WorkAccountabilitySystem.History" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Work History</title>
</head>
<body>
<form id="form1" runat="server">

<h2>Work History</h2>

<!-- USER + ADMIN DISCUSSION HISTORY -->
<asp:Repeater ID="rptHistory" runat="server">
<ItemTemplate>

    <!-- CHANGED: cleaner container -->
    <div style="border:1px solid #ddd; padding:12px; margin-bottom:12px; border-radius:4px">

        <!-- User section -->
        <div>
            <b>User:</b> <%# Eval("Username") %><br />
            <b>Status:</b> <%# Eval("Status") %><br />
            <b>Date:</b> <%# Eval("CreatedDate") %>
        </div>

        <div style="margin-top:6px; padding:8px; background:#f7f7f7">
            <b>User Remark:</b><br />
            <%# Eval("UserRemark") %>
        </div>

        <!-- CHANGED: admin reply styled separately -->
        <%# string.IsNullOrEmpty(Eval("AdminRemark").ToString())
            ? ""
            : "<div style='margin-top:8px; padding:8px; background:#eef5ff'>" +
              "<b>Admin Reply:</b><br />" +
              Eval("AdminRemark") +
              "</div>" %>

    </div>

</ItemTemplate>
</asp:Repeater>

<hr />

<h3>Admin Actions (Authority Log)</h3>

<!-- ADMIN AUTHORITY ACTIONS -->
<asp:Repeater ID="rptAdmin" runat="server">
<ItemTemplate>

    <!-- CHANGED: action styled as system event -->
    <div style="border-left:4px solid #999; padding:8px; margin-bottom:8px; background:#fafafa">
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