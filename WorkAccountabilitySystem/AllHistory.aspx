<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllHistory.aspx.cs" Inherits="WorkAccountabilitySystem.AllHistory" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>System History</title>
</head>
<body>
<form id="form1" runat="server">

<h2>System History</h2>

<!-- RIGHT-SIDE VIEW SELECTOR -->
<div style="float:right; margin-bottom:10px">
    <asp:LinkButton runat="server" ID="lnkTimeline" OnClick="ShowTimeline_Click">
        📜 Timeline View
    </asp:LinkButton>
    |
    <asp:LinkButton runat="server" ID="lnkWorkWise" OnClick="ShowWorkWise_Click">
        🗂 Work-wise View
    </asp:LinkButton>
</div>

<div style="clear:both"></div>

<!-- ================= TIMELINE VIEW ================= -->
<asp:Panel ID="pnlTimeline" runat="server">
    <asp:Repeater ID="rptAllHistory" runat="server">
    <ItemTemplate>
        <div style="border-left:4px solid #555; padding:10px; margin-bottom:12px; background:#fafafa">
            <b><%# Eval("EventTime") %></b><br />
            <b><%# Eval("EventType") %></b> — <%# Eval("Actor") %><br />
            <div style="margin-top:6px">
                <%# Eval("Description") %>
            </div>
            <div style="font-size:smaller;color:#666">
                Work ID: <%# Eval("WorkId") %>
            </div>
        </div>
    </ItemTemplate>
    </asp:Repeater>
</asp:Panel>

<!-- ================= WORK-WISE VIEW ================= -->
<asp:Panel ID="pnlWorkWise" runat="server" Visible="false">

    <asp:Repeater ID="rptWorkWise" runat="server">
    <ItemTemplate>

        <!-- WORK CONTAINER -->
        <div style="border:2px solid #2c7be5; background:#eef5ff; padding:12px; margin-bottom:18px">

            <h3>
                Work ID: <%# Eval("WorkId") %> — <%# Eval("Description") %>
            </h3>

            <!-- NESTED TIMELINE FOR THIS WORK -->
            <asp:Repeater runat="server" DataSource='<%# Eval("Events") %>'>
            <ItemTemplate>
                <div style="margin-left:15px; padding:6px; border-left:3px solid #999; margin-bottom:6px">
                    <b><%# Eval("EventTime") %></b>
                    — <b><%# Eval("Actor") %></b><br />
                    <%# Eval("Text") %>
                </div>
            </ItemTemplate>
            </asp:Repeater>

            <!-- ================= ADMIN-ONLY REOPEN ================= -->
            <asp:Panel 
                runat="server"
                Visible='<%# 
                    Eval("LatestStatus").ToString() == "Closed"   /* ADDED: only closed work */
                    && Session["Role"] != null
                    && Session["Role"].ToString() == "Admin" %>'>

                <!-- Reopen button appears ONLY for closed work -->
                <asp:Button
                    runat="server"
                    Text="Reopen Work"
                    CommandArgument='<%# Eval("WorkId") %>'
                    OnCommand="Reopen_Command"
                    Style="margin-top:10px; background:#ffeeba; border:1px solid #f0ad4e;" />
            </asp:Panel>
            <!-- ===================================================== -->

        </div>

    </ItemTemplate>
    </asp:Repeater>

</asp:Panel>

<br />
<asp:Button runat="server" Text="Back to Dashboard" OnClick="Back_Click" />

</form>
</body>
</html>