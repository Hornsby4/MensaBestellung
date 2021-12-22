<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="MensaBestellung.RegistrationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="generalStyle.css" rel="stylesheet" type="text/css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <header class="RegistrationHeader">
             <asp:Label ID="lbl_RegistrationHeaderFormat" runat="server" Text="ANMELDUNG"></asp:Label>
        </header>
    <div class="RegistrationContent">
        <table align="center">
            <tr>
                <td><asp:Label ID="lbl_registrationLastname" runat="server" Text="Nachname: "></asp:Label></td>
                <td><asp:TextBox ID="txt_registrationLastName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lbl_userEmail" runat="server" Text="Email: "></asp:Label></td>
                <td><asp:TextBox ID="txt_userEmail" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <asp:Button ID="btn_saveUse" runat="server" Text="Speichern" Height="34px" Width="113px" />
    </div> 
    </form>
</body>
</html>
