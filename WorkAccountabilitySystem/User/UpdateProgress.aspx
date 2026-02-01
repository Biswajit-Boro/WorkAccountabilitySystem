<%@ Page Language="C#"
    AutoEventWireup="true"
    CodeBehind="UpdateProgress.aspx.cs"
    Inherits="WorkAccountabilitySystem.User.UpdateProgress" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Progress</title>
</head>
<body>
<form id="form1" runat="server">

    <h3>Update Work Progress</h3>

    <asp:DropDownList
        ID="ddlWork"
        runat="server"
        Width="400" />

    <br /><br />

    <asp:DropDownList
        ID="ddlStatus"
        runat="server">
        <asp:ListItem Text="InProgress" Value="InProgress" />
        <asp:ListItem Text="Closed" Value="Closed" />
    </asp:DropDownList>

    <br /><br />

    <asp:TextBox
        ID="txtRemark"
        runat="server"
        Width="400"
        TextMode="MultiLine"
        Rows="4" />

    <br /><br />

    <asp:Button
        ID="btnSubmit"
        runat="server"
        Text="Submit Progress"
        OnClick="Submit_Click" />

</form>
</body>
</html>