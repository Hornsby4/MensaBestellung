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
        <img src="https://www.kmh-gmbh.at/fileadmin/kmh/LINKS/htl.png" width:"50px" style="height: 100px; width: 159px">
        <br />
        <asp:Label ID="lbl_Header" runat="server" Text="Menüs bestellen" Font-Size="Large" ForeColor="White"></asp:Label>
        <br />
        <asp:Label ID="lbl_name" runat="server" ForeColor="White"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_nextWeek" runat="server" Text="Nächste Woche" />
        <br />
        <asp:Button class="Button" ID="btn_lastWeek" runat="server" Text="Vorherige Woche" />
        <br />
        <asp:Button class="Button" ID="btn_saveOrder" runat="server" Text="Bestellungen speichern" BackColor="#00CC66" />
        <br />
        <asp:Button class="Button" ID="btn_throwAwayChanges" runat="server" Text="Änderungen verwerfen"/>
        <br />
        <asp:Button class="Button" ID="btn_buyMoreFood" runat="server" Text="Essen nachkaufen" OnClick="btn_buyMoreFood_Click" />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_goToAdminPage" runat="server" Text="Zur Administration" OnClick="btn_goToAdminPage_Click" />
        <br />
        <asp:Button class="Button" ID="btn_close" runat="server" Text="Beenden" />
    </div>
    <div class="PageContent">
            
    </div>
    </form>

</html>
