<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRemark.aspx.cs" Inherits="WorkAccountabilitySystem.Admin.AddRemark" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Admin Remark</title>
</head>
<body>
    <form id="form1" runat="server">
       <h3>Add Admin Remark</h3>

<asp:TextBox ID="txtProgressId" runat="server"
    Placeholder="ProgressId" />

<br /><br />

<asp:TextBox ID="txtRemark" runat="server"
    Width="400" TextMode="MultiLine" Rows="4" />

<br /><br />

<asp:Button ID="btnAdd" runat="server"
    Text="Add Remark"
    OnClick="Add_Click" />

</form>
</body>
</html>
