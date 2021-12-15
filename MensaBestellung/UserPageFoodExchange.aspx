<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPageFoodExchange.aspx.cs" Inherits="MensaBestellung.UserPageFoodExchange" %>

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
        <asp:Label ID="lbl_exchangeAndName" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Button class="Button" ID="btn_saveExchangeFoodOrder" runat="server" Text="Einkauf speichern" />
        <br />
        <asp:Button class="Button" ID="btn_throwAwayExchangeFoodChanges" runat="server" Text="Änderungen verwerfen" />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_goBack" runat="server" Text="Zurück" />
    </div>
    <div class="PageContent">
            
    </div>
    </form>
    </form>
</html>
