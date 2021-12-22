<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="MensaBestellung.AdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="generalStyle.css" rel="stylesheet" type="text/css"/>
    
    <title></title>
    <style type="text/css">
        .auto-style1 {
            height: 33px;
        }
        .auto-style7 {
            width: 210px;
            height: 33px;
        }
        .auto-style11 {
            width: 210px;
            height: 24px;
        }
        .auto-style13 {
            height: 24px;
        }
        .auto-style14 {
            height: 24px;
            width: 86px;
        }
        .auto-style15 {
            height: 33px;
            width: 86px;
        }
        .auto-style16 {
            height: 24px;
            width: 335px;
        }
        .auto-style17 {
            height: 33px;
            width: 335px;
        }
        .auto-style20 {
            height: 24px;
            width: 74px;
        }
        .auto-style21 {
            height: 33px;
            width: 74px;
        }
        .auto-style24 {
            height: 24px;
            width: 46px;
        }
        .auto-style25 {
            height: 33px;
            width: 46px;
        }
        .auto-style29 {
            width: 374px;
            height: 24px;
        }
        .auto-style30 {
            width: 374px;
            height: 33px;
        }
        .auto-style31 {
            width: 210px;
            height: 27px;
        }
        .auto-style32 {
            height: 27px;
            width: 74px;
        }
        .auto-style33 {
            height: 27px;
            width: 86px;
        }
        .auto-style34 {
            height: 27px;
            width: 335px;
        }
        .auto-style35 {
            height: 27px;
            width: 46px;
        }
        .auto-style36 {
            width: 374px;
            height: 27px;
        }
        .auto-style37 {
            height: 27px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
</body>
    <div class ="Navigation">
        <br />
        <img src="/resources/htl.png" width:"50px" style="height: 100px; width: 159px">
        <br />
        <asp:Label ID="lbl_Header" runat="server" Text="Menüs Verwalten" Font-Size="Large" ForeColor="White"></asp:Label>
        <br />
        <asp:Label ID="lbl_name" runat="server" ForeColor="White"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_nextWeek" runat="server" Text="Nächste Woche" />
        <br />
        <asp:Button class="Button" ID="btn_lastWeek" runat="server" Text="Vorherige Woche" />
        <br />
        <asp:Button class="Button" ID="btn_saveChanges" runat="server" Text="Änderungen speichern" BackColor="#00CC66" />
        <br />
        <asp:Button class="Button" ID="btn_throwAwayChanges" runat="server" Text="Änderungen verwerfen"/>
        <br />
        <asp:Button class="Button" ID="btn_buyMoreFood" runat="server" Text="Menüs Erstellen" />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_goToUserPage" runat="server" Text="Zur Bestellseite" OnClick="btn_goToUserPage_Click" />
        <br />
        <asp:Button class="Button" ID="btn_close" runat="server" Text="Beenden" />
    </div>
    <div class="PageContent">
        <br />
        <br />
        <br />
        <table id="tbl_weekTable" class="TableClass">
            <tr>
                <td class="TableHeaders" rowspan="1">Datum</td>
                <td class="TableHeaders">ΣBestellte Menüs</td>
                <td class="TableHeaders">ΣMenüs in Essensbörse</td>
                <td class="TableHeaders">Menüs</td>
                <td class="TableHeaders">Preis</td>
                <td class="TableHeaders">Essensbörse</td>
                <td class="TableHeaders">Zum Löschen
                    <br />
                    freigeben</td>
            </tr>
            <tr>
                <td class="auto-style31" headers="Datum;Bestellete"></td>
                <td class="auto-style32"></td>
                <td class="auto-style33"></td>
                <td class="auto-style34">
                    <br />
                </td>
                <td class="auto-style35"></td>
                <td class="auto-style36"></td>
                <td class="auto-style37">
                    <asp:CheckBox ID="CheckBox_monday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style11" headers="Datum;Bestellete"></td>
                <td class="auto-style20"></td>
                <td class="auto-style14"></td>
                <td class="auto-style16">
                    <br />
                </td>
                <td class="auto-style24"></td>
                <td class="auto-style29"></td>
                <td class="auto-style13">
                    <asp:CheckBox ID="CheckBox_tuesday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style11" headers="Datum;Bestellete"></td>
                <td class="auto-style20"></td>
                <td class="auto-style14"></td>
                <td class="auto-style16">
                    <br />
                </td>
                <td class="auto-style24"></td>
                <td class="auto-style29"></td>
                <td class="auto-style13">
                    <asp:CheckBox ID="CheckBox_wednesday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style11" headers="Datum;Bestellete">&nbsp;</td>
                <td class="auto-style20">&nbsp;</td>
                <td class="auto-style14">&nbsp;</td>
                <td class="auto-style16">
                    <br />
                </td>
                <td class="auto-style24">&nbsp;</td>
                <td class="auto-style29">&nbsp;</td>
                <td class="auto-style13">
                    <asp:CheckBox ID="CheckBox_thursday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style7" headers="Datum;Bestellete"></td>
                <td class="auto-style21"></td>
                <td class="auto-style15"></td>
                <td class="auto-style17">
                    <br />
                </td>
                <td class="auto-style25"></td>
                <td class="auto-style30"></td>
                <td class="auto-style1">
                    <asp:CheckBox ID="CheckBox_friday" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </div>
    </form>
</html>

