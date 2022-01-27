<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPageFoodExchange.aspx.cs" Inherits="MensaBestellung.UserPageFoodExchange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="generalStyle.css" rel="stylesheet" type="text/css"/>
    <title></title>
    </head>
<body>
    <form id="form1" runat="server">
    <div class ="Navigation">
        <br />
        <img src="/resources/htl.png" width:"50px" style="height: 100px; width: 159px"/>
        <br />
        <asp:Label ID="Label1" runat="server" Font-Size="Large" ForeColor="White" Text="Essensbörse"></asp:Label>
        <br />
        <asp:Label ID="lbl_exchangeAndName" runat="server" ForeColor="White"></asp:Label>
        <br />
        <br />
        <asp:Button class="Button" ID="btn_saveExchangeFoodOrder" runat="server" Text="Einkauf speichern" BackColor="#00CC66" OnClick="btn_saveExchangeFoodOrder_Click" />
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
            &nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_text" runat="server" Text="Der Preis je Gericht beträgt 5,60€ - Die Essensbörse schließt täglich um 13:30 Uhr !"></asp:Label>
&nbsp;<asp:GridView ID="gv_foodExchange" runat="server" CellPadding="4" ForeColor="#333333" Width="841px" AutoGenerateColumns="False">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="menuDate" HeaderText="Datum" DataFormatString="{0:d}" HtmlEncode="false"/>
                <asp:templatefield HeaderText="Essen nachkaufen" >
                <itemtemplate>
                    <asp:checkbox ID="buy" runat="server" OnCheckedChanged="SelectCheckBox_OnCheckedChanged" AutoPostBack="true"/>
                </itemtemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:templatefield>
                <asp:BoundField DataField="menu" HeaderText="Menü" HtmlEncode="false"/>
                <asp:BoundField DataField="seller" HeaderText="Anbieter" HtmlEncode="false"/>
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <br />
            <asp:Label ID="lbl_info" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
