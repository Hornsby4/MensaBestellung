<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="MensaBestellung.RegistrationPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="generalStyle.css" rel="stylesheet" type="text/css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <header class="SignInHeader">
             <asp:Label ID="lbl_SignInHeaderFormat" runat="server" Text="ANMELDUNG"></asp:Label>
        </header>
    <div class="SignInContent">
        <table align="center">
            <tr>
                <td><asp:Label ID="lbl_signInLastname" runat="server" Text="Nachname: "></asp:Label></td>
                <td><asp:TextBox ID="txt_signInLastName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lbl_userEmail" runat="server" Text="Email: "></asp:Label></td>
                <td><asp:TextBox ID="txt_userEmail" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btn_saveUse" runat="server" Text="Speichern" Height="34px" Width="113px" />
    </div> 
    </form>
</body>
</html>
