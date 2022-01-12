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
        .auto-style50 {
            width: 330px;
        }
        .auto-style51 {
            width: 1255px;
        }
        .auto-style53 {
            width: 399px;
        }
        .auto-style54 {
            width: 210px;
            height: 35px;
        }
        .auto-style55 {
            height: 35px;
            width: 74px;
        }
        .auto-style56 {
            height: 35px;
            width: 86px;
        }
        .auto-style57 {
            height: 35px;
            width: 335px;
        }
        .auto-style58 {
            height: 35px;
            width: 46px;
        }
        .auto-style59 {
            height: 35px;
            width: 382px;
        }
        .auto-style60 {
            height: 35px;
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
        <asp:Button class="Button" ID="btn_saveChanges" runat="server" Text="Ausgewählte Löschen" BackColor="#00CC66" />
        <br />
        <asp:Button class="Button" ID="btn_throwAwayChanges" runat="server" Text="Änderungen verwerfen" OnClick="btn_throwAwayChanges_Click"/>
        <br />
        <asp:Button class="Button" ID="btn_buyMoreFood" runat="server" Text="Mahlzeiten anlegen" />
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lbl_infoLabel" runat="server" ForeColor="#CC0000"></asp:Label>
        <br />
        <br />
        <table id="tbl_weekTable" class="auto-style45">
            <tr>
                <td class="auto-style46" rowspan="1">Datum</td>
                <td class="auto-style46">ΣBestellte Menüs</td>
                <td class="auto-style46">ΣMenüs in Essensbörse</td>
                <td class="auto-style46">Mahlzeiten</td>
                <td class="auto-style46">Preis</td>
                <td class="auto-style47">Essensbörse Schlussdatum</td>
                <td class="auto-style46">Zum Löschen
                    <br />
                    freigeben</td>
            </tr>
            <tr>
                <td class="auto-style54" headers="Datum;Bestellete">27.12.2021</td>
                <td class="auto-style55">2</td>
                <td class="auto-style56">2</td>
                <td class="auto-style57">
                    Suppe<br />
                    Schnitzel<br />
                    <br />
                </td>
                <td class="auto-style58">€5,60</td>
                <td class="auto-style59">18.01.2022</td>
                <td class="auto-style60">
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
        Menü erstellen:<br />
        <br />
        <table class="auto-style51">
            <tr>
                <td class="auto-style49">&nbsp;
                    <asp:TextBox ID="txt_datePicker" runat="server" BackColor="White" Height="22px" Width="176px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_MenuDate" runat="server" ControlToValidate="txt_datePicker" ErrorMessage="Menütag muss Datum beinhalten"></asp:RequiredFieldValidator>
                    <ajaxToolkit:CalendarExtender ID="txt_datePicker_CalendarExtender" runat="server" BehaviorID="txt_datePicker_CalendarExtender" FirstDayOfWeek="Monday" TargetControlID="txt_datePicker" DaysModeTitleFormat="MMMM y" Format="yyyy-MM-dd" />
                </td>
                <td class="auto-style50">
                    <asp:DropDownList ID="ddl_dish1" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style50">
                    <asp:DropDownList ID="ddl_dish2" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style53">
                    <asp:DropDownList ID="ddl_sidedish" runat="server" Width="300px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button class="Button" ID="btn_saveNewMenu" runat="server" Text="Menü speichern" BackColor="#00CC66" OnClick="btn_saveNewMenu_Click" />
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

