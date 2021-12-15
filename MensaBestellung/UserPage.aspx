<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="MensaBestellung.UserPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="userStyle.css" rel="stylesheet" type="text/css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
</body>
    <div class ="Navigation">
        <br />

        <asp:Label ID="lbl_orderAndName" runat="server"></asp:Label>

        <br />
        <br />
        <asp:Button class="Button" ID="btn_nextWeek" runat="server" Text="Nächste Woche" />
        <br />
        <asp:Button class="Button" ID="btn_lastWeek" runat="server" Text="Vorherige Woche" />
        <br />
        <asp:Button class="Button" ID="btn_saveOrder" runat="server" Text="Bestellungen speichern" />
        <br />
        <asp:Button class="Button" ID="btn_throwAwayChanges" runat="server" Text="Änderungen verwerfen"/>
        <br />
        <asp:Button class="Button" ID="btn_buyMoreFood" runat="server" Text="Essen nachkaufen" />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_goToAdminPage" runat="server" Text="Zur Administration" />
        <br />
        <asp:Button class="Button" ID="btn_close" runat="server" Text="Beenden" />
    </div>
    <div class="PageContent">
            
    </div>
    </form>

</html>
