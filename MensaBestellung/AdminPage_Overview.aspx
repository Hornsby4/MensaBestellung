<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage_Overview.aspx.cs" Inherits="MensaBestellung.AdminPage" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="generalStyle.css" rel="stylesheet" type="text/css"/>
    
    <title></title>
    <style type="text/css">
        .auto-style31 {
            width: 210px;
            height: 36px;
        }
        .auto-style32 {
            height: 36px;
            width: 74px;
        }
        .auto-style33 {
            height: 36px;
            width: 86px;
        }
        .auto-style34 {
            height: 36px;
            width: 335px;
        }
        .auto-style35 {
            height: 36px;
            width: 46px;
        }
        .auto-style37 {
            height: 36px;
        }
        .auto-style45 {
            text-align: center;
            background-color: lightgray;
            height: 222px;
        }
        .auto-style46 {
            text-align: center;
            background-color: lightcoral;
            height: 40px;
        }
        .auto-style47 {
            text-align: center;
            background-color: lightcoral;
            height: 40px;
            width: 382px;
        }
        .auto-style48 {
            height: 36px;
            width: 382px;
        }
        .auto-style49 {
            text-align: center;
            width: 195px;
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
        <img src="https://www.kmh-gmbh.at/fileadmin/kmh/LINKS/htl.png" width:"50px" style="height: 100px; width: 159px">
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
        <br />
        <br />
        <table id="tbl_weekTable" class="auto-style45">
            <tr>
                <td class="auto-style46" rowspan="1">Datum</td>
                <td class="auto-style46">ΣBestellte Menüs</td>
                <td class="auto-style46">ΣMenüs in Essensbörse</td>
                <td class="auto-style46">Menüs</td>
                <td class="auto-style46">Preis</td>
                <td class="auto-style47">Essensbörse</td>
                <td class="auto-style46">Zum Löschen
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
                <td class="auto-style48"></td>
                <td class="auto-style37">
                    <asp:CheckBox ID="CheckBox_monday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style31" headers="Datum;Bestellete"></td>
                <td class="auto-style32"></td>
                <td class="auto-style33"></td>
                <td class="auto-style34">
                    <br />
                </td>
                <td class="auto-style35"></td>
                <td class="auto-style48"></td>
                <td class="auto-style37">
                    <asp:CheckBox ID="CheckBox_tuesday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style31" headers="Datum;Bestellete"></td>
                <td class="auto-style32"></td>
                <td class="auto-style33"></td>
                <td class="auto-style34">
                    <br />
                </td>
                <td class="auto-style35"></td>
                <td class="auto-style48"></td>
                <td class="auto-style37">
                    <asp:CheckBox ID="CheckBox_wednesday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style31" headers="Datum;Bestellete">&nbsp;</td>
                <td class="auto-style32">&nbsp;</td>
                <td class="auto-style33">&nbsp;</td>
                <td class="auto-style34">
                    <br />
                </td>
                <td class="auto-style35">&nbsp;</td>
                <td class="auto-style48">&nbsp;</td>
                <td class="auto-style37">
                    <asp:CheckBox ID="CheckBox_thursday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style31" headers="Datum;Bestellete"></td>
                <td class="auto-style32"></td>
                <td class="auto-style33"></td>
                <td class="auto-style34">
                    <br />
                </td>
                <td class="auto-style35"></td>
                <td class="auto-style48"></td>
                <td class="auto-style37">
                    <asp:CheckBox ID="CheckBox_friday" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        Essensplan befüllen:<br />
        <br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style49">&nbsp;
                    <asp:TextBox ID="txt_datePicker" runat="server" BackColor="White" Height="22px" Width="176px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txt_datePicker_CalendarExtender" runat="server" BehaviorID="txt_datePicker_CalendarExtender" FirstDayOfWeek="Monday" TargetControlID="txt_datePicker" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
&nbsp;<br />
        &nbsp;&nbsp;&nbsp;
        <br />
&nbsp;<br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ScriptManager ID="ajaxScriptmanager1" runat="server">
        </asp:ScriptManager>
    </div>
    </form>
</html>

