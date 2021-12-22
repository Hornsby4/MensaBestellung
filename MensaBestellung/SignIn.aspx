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
                <td><asp:TextBox ID="txt_lastName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label ID="lbl_userEmail" runat="server" Text="Email: "></asp:Label></td>
                <td><asp:TextBox ID="txt_userEmail" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btn_signInUser" runat="server" Text="Anmelden" Height="34px" Width="119px" OnClick="btn_signInUser_Click" />
        <br />
        <asp:Label ID="lbl_Info" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <asp:RequiredFieldValidator ID="reqFldVal_lastname" runat="server" ErrorMessage="Bitte geben Sie Ihren Nachnamen ein." ForeColor="Red" ControlToValidate="txt_lastName" EnableClientScript="False"></asp:RequiredFieldValidator>
        <br />
        <asp:RequiredFieldValidator ID="reqFldVal_Email" runat="server" ErrorMessage="Bitte geben Sie Ihre Email-Adresse ein." ForeColor="Red" ControlToValidate="txt_userEmail" EnableClientScript="False"></asp:RequiredFieldValidator>
        <br />
        <asp:RegularExpressionValidator ID="regexVal_Email" runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txt_userEmail" ErrorMessage="Ungültiges Email-Format" EnableClientScript="False" ForeColor="Red"></asp:RegularExpressionValidator>
    </div> 
    </form>
</body>
</html>
