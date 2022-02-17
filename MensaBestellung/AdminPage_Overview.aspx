<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage_Overview.aspx.cs" Inherits="MensaBestellung.AdminPage" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="generalStyle.css" rel="stylesheet" type="text/css"/>
    
    <title></title>
    <style type="text/css">
        .dishBox{
            width:stretch;
            height:stretch;
        }
        .menuCreationTable{
              width: 100%;
              padding: 0px;
              text-align:center;
              text-anchor:start;
              table-layout:fixed;
              vertical-align:central;
    
        }
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
            width: 187px;
            height: 32px;
        }
        .auto-style53 {
            width: 314px;
            height: 32px;
        }
        .auto-style61 {
            width: 313px;
            height: 32px;
        }

        .auto-style62 {
            width: 199px;
        }
        .auto-style63 {
            width: 210px;
            height: 38px;
        }
        .auto-style64 {
            height: 38px;
            width: 74px;
        }
        .auto-style65 {
            height: 38px;
            width: 86px;
        }
        .auto-style66 {
            height: 38px;
            width: 335px;
        }
        .auto-style67 {
            height: 38px;
            width: 46px;
        }
        .auto-style68 {
            height: 38px;
            width: 382px;
        }
        .auto-style69 {
            height: 38px;
        }
        .auto-style70 {
            position: absolute;
            left: 220px;
            top: 50px;
            bottom: -102px;
            right: 10px;
        }

        .auto-style71 {
            height: 21px;
        }
        .auto-style72 {
            width: 199px;
            height: 21px;
        }

        .auto-style73 {
            width: 210px;
            height: 35px;
        }
        .auto-style74 {
            height: 35px;
            width: 74px;
        }
        .auto-style75 {
            height: 35px;
            width: 86px;
        }
        .auto-style76 {
            height: 35px;
            width: 335px;
        }
        .auto-style77 {
            height: 35px;
            width: 46px;
        }
        .auto-style78 {
            height: 35px;
            width: 382px;
        }
        .auto-style79 {
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
        <asp:Button class="Button" ID="btn_nextWeek" runat="server" Text="Nächste Woche" OnClick="btn_nextWeek_Click" />
        <br />
        <asp:Button class="Button" ID="btn_lastWeek" runat="server" Text="Vorherige Woche" OnClick="btn_lastWeek_Click" />
        <br />
        <asp:Button class="Button" ID="btn_deleteSelected" runat="server" Text="Ausgewählte Löschen" BackColor="#00CC66" OnClick="btn_deleteSelected_Click" />
        <br />
        <br />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_goToUserPage" runat="server" Text="Zur Bestellseite" OnClick="btn_goToUserPage_Click" />
        <br />
        <asp:Button class="Button" ID="btn_close" runat="server" Text="Beenden" OnClick="btn_close_Click" />
    </div>
    <div class="auto-style70">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <table id="tbl_weekTable" class="auto-style45" border="1">
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
                <td class="auto-style73" headers="Datum;Bestellete">&nbsp;<asp:Label ID="lbl_monday_date" runat="server" /></td>
                <td class="auto-style74"><asp:Label ID="lbl_monday_menuSum" runat="server" /></td>
                <td class="auto-style75"><asp:Label ID="lbl_monday_menuSumX" runat="server" /></td>
                <td class="auto-style76">
                         <table class="tableMenu">
                            <tr>
                                <td class="auto-style15">Beilage: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_sideDishMonday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 1: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish1Monday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 2: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish2Monday" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                </td>
                <td class="auto-style77">5,60</td>
                <td class="auto-style78">
                                    <asp:Label ID="lbl_MonExchangeEndDate" runat="server"></asp:Label>
                                </td>
                <td class="auto-style79">
                    <asp:CheckBox ID="CheckBox_monday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style31" headers="Datum;Bestellete"><asp:Label ID="lbl_tuesday_date" runat="server" /></td>
                <td class="auto-style32"><asp:Label ID="lbl_tuesday_menuSum" runat="server" /></td>
                <td class="auto-style33"><asp:Label ID="lbl_tuesday_menuSumX" runat="server" /></td>
                <td class="auto-style34">
                         <table class="tableMenu">
                            <tr>
                                <td class="auto-style15">Beilage: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_sideDishTuesday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 1: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish1Tuesday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 2: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish2Tuesday" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                </td>
                <td class="auto-style35">5,60</td>
                <td class="auto-style48">
                                    <asp:Label ID="lbl_TueExchangeEndDate" runat="server"></asp:Label>
                                </td>
                <td class="auto-style37">
                    <asp:CheckBox ID="CheckBox_tuesday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style31" headers="Datum;Bestellete"><asp:Label ID="lbl_wednesday_date" runat="server" /></td>
                <td class="auto-style32"><asp:Label ID="lbl_wednesday_menuSum" runat="server" /></td>
                <td class="auto-style33"><asp:Label ID="lbl_wednesday_menuSumX" runat="server" /></td>
                <td class="auto-style34">
                         <table class="tableMenu">
                            <tr>
                                <td class="auto-style15">Beilage: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_sideDishWednesday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 1: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish1Wednesday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 2: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish2Wednesday" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                </td>
                <td class="auto-style35">5,60</td>
                <td class="auto-style48">
                                    <asp:Label ID="lbl_WedExchangeEndDate" runat="server"></asp:Label>
                                </td>
                <td class="auto-style37">
                    <asp:CheckBox ID="CheckBox_wednesday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style63" headers="Datum;Bestellete"><asp:Label ID="lbl_thursday_date" runat="server" /></td>
                <td class="auto-style64"><asp:Label ID="lbl_thursday_menuSum" runat="server" /></td>
                <td class="auto-style65"><asp:Label ID="lbl_thursday_menuSumX" runat="server" /></td>
                <td class="auto-style66">
                         <table class="tableMenu">
                            <tr>
                                <td class="auto-style15">Beilage: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_sideDishThursday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 1: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish1Thursday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 2: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish2Thursday" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                </td>
                <td class="auto-style67">5,60</td>
                <td class="auto-style68">
                                    <asp:Label ID="lbl_ThuExchangeEndDate" runat="server"></asp:Label>
                                </td>
                <td class="auto-style69">
                    <asp:CheckBox ID="CheckBox_thursday" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="auto-style31" headers="Datum;Bestellete"><asp:Label ID="lbl_friday_date" runat="server" /></td>
                <td class="auto-style32"><asp:Label ID="lbl_friday_menuSum" runat="server" /></td>
                <td class="auto-style33"><asp:Label ID="lbl_friday_menuSumX" runat="server" /></td>
                <td class="auto-style34">
                         <table class="tableMenu">
                            <tr>
                                <td class="auto-style71">Beilage: </td>
                                <td class="auto-style72">
                                    <asp:Label ID="lbl_sideDishFriday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 1: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish1Friday" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Hauptspeise 2: </td>
                                <td class="auto-style62">
                                    <asp:Label ID="lbl_dish2Friday" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                </td>
                <td class="auto-style35">5,60</td>
                <td class="auto-style48">
                                    <asp:Label ID="lbl_FriExchangeEndDate" runat="server"></asp:Label>
                                </td>
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
        <table class="menuCreationTable" id="tbl_createMenu" border="1">
            <tr>
                <td class="auto-style49">
                    <asp:TextBox ID="txt_datePicker" runat="server" BackColor="White" autocomplete="off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txt_datePicker_CalendarExtender" runat="server" BehaviorID="txt_datePicker_CalendarExtender" FirstDayOfWeek="Monday" TargetControlID="txt_datePicker" DaysModeTitleFormat="MMMM y" Format="yyyy-MM-dd" />
                &nbsp;</td>
                <td class="auto-style61">
                    <ajaxToolkit:ComboBox ID="comboB_maindish1" runat="server" AutoCompleteMode="Suggest" DropDownStyle="DropDown" CssClass="dishBox"> 
                    </ajaxToolkit:ComboBox>
                </td>
                <td class="auto-style61">
                    <ajaxToolkit:ComboBox ID="comboB_maindish2" runat="server" AutoCompleteMode="Suggest" DropDownStyle="DropDown" >
                    </ajaxToolkit:ComboBox>
                </td>
                <td class="auto-style53">
                    <ajaxToolkit:ComboBox ID="comboB_sidedish" runat="server" AutoCompleteMode="Suggest" DropDownStyle="DropDown">
                    </ajaxToolkit:ComboBox>
                </td>
            </tr>
        </table>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lbl_infoLabel" runat="server" ForeColor="#CC0000"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button class="Button" ID="btn_saveNewMenu" runat="server" Text="Menü speichern" BackColor="#00CC66" OnClick="btn_saveNewMenu_Click" />
        &nbsp;
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

