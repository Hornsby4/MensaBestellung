<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPageFoodExchange.aspx.cs" Inherits="MensaBestellung.UserPageFoodExchange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="userStyle.css" rel="stylesheet" type="text/css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
</body>
    <div class ="Navigation">
        <br />
        <img src="https://www.kmh-gmbh.at/fileadmin/kmh/LINKS/htl.png" width:"50px" style="height: 100px; width: 159px">
        <br />
        <asp:Label ID="Label1" runat="server" Font-Size="Large" ForeColor="White" Text="Essensbörse"></asp:Label>
        <br />
        <asp:Label ID="lbl_exchangeAndName" runat="server" ForeColor="White"></asp:Label>
        <br />
        <br />
        <asp:Button class="Button" ID="btn_saveExchangeFoodOrder" runat="server" Text="Einkauf speichern" BackColor="#00CC66" />
        <br />
        <asp:Button class="Button" ID="btn_throwAwayExchangeFoodChanges" runat="server" Text="Änderungen verwerfen" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_goBack" runat="server" Text="Zurück" OnClick="btn_goBack_Click" />
    </div>
    <div class="PageContent">
            
    </div>
    </form>
    </form>
</html>
