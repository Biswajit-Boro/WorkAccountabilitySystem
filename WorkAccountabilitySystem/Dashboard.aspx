<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WorkAccountabilitySystem.Dashboard" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dashboard</title>
</head>
<body>
<form id="form1" runat="server">


<h2>Work Dashboard</h2>

<asp:Panel ID="pnlAdmin" runat="server">
    <a href="/Admin/CreateWork.aspx">[ Create Work ]</a>
</asp:Panel>


<br />

<asp:GridView ID="gvActive" runat="server" AutoGenerateColumns="False">
    <Columns>
        <asp:BoundField DataField="WorkId" HeaderText="ID" />
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:BoundField DataField="Priority" HeaderText="Priority" />
        <asp:BoundField DataField="Status" HeaderText="Status" />
        <asp:BoundField DataField="Users" HeaderText="Users" />

        <asp:TemplateField HeaderText="User Action">
            <ItemTemplate>
                <asp:Panel runat="server" Visible='<%# IsUser() %>'>
                    <a href='/User/OptIn.aspx?workId=<%# Eval("WorkId") %>'>[ Opt In ]</a>
                    |
                    <a href='/User/UpdateProgress.aspx?workId=<%# Eval("WorkId") %>'>[ Update ]</a>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Admin Action">
            <ItemTemplate>
                <asp:Panel runat="server" Visible='<%# IsAdmin() %>'>
                  
                    <a href='/Admin/AddRemark.aspx?workId=<%# Eval("WorkId") %>'>[ Respond ]</a>
                    |
                    <a href='/Admin/AdminActions.aspx?workId=<%# Eval("WorkId") %>'>[ Force Close / Reopen ]</a>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:HyperLinkField
            Text="History"
            DataNavigateUrlFields="WorkId"
            DataNavigateUrlFormatString="History.aspx?workId={0}" />
    </Columns>
</asp:GridView>
    <asp:Button 
    ID="btnLogout" 
    runat="server" 
    Text="Logout" 
    OnClick="Logout_Click" />  

</form>
</body>
</html>