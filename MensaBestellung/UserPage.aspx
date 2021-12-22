<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="MensaBestellung.UserPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="generalStyle.css" rel="stylesheet" type="text/css"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 125px;
        }
        .auto-style2 {
            width: 171px;
        }
        .auto-style3 {
            width: 232px;
        }
        .auto-style4 {
            width: 323px;
        }
        .auto-style5 {
            width: 125px;
            height: 27px;
        }
        .auto-style6 {
            height: 27px;
        }
        .auto-style7 {
            width: 87px;
        }
        .auto-style8 {
            width: 105px;
        }
        .auto-style9 {
            position: absolute;
            text-align: center;
            width: 200px;
            bottom: 0px;
            right: 1112px;
            left: 0px;
            top: 0px;
            background-color: indianred;
        }
        .auto-style10 {
            width: 170px;
        }
        .auto-style11 {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
</body>
    <div class ="auto-style9">
        <br />
        <img src="/resources/htl.png" width:"50px" style="height: 100px; width: 159px">
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
        <asp:Button class="Button" ID="btn_saveOrder" runat="server" Text="Bestellungen speichern" BackColor="#00CC66" OnClick="btn_saveOrder_Click" />
        <br />
        <asp:Button class="Button" ID="btn_throwAwayChanges" runat="server" Text="Änderungen verwerfen"/>
        <br />
        <asp:Button class="Button" ID="btn_buyMoreFood" runat="server" Text="Essen nachkaufen" OnClick="btn_buyMoreFood_Click" />
        <br />
        <br />
        <asp:Button class="Button" ID="btn_goToAdminPage" runat="server" Text="Zur Administration" OnClick="btn_goToAdminPage_Click" Visible="False" />
        <br />
        <asp:Button class="Button" ID="btn_close" runat="server" Text="Beenden" OnClick="btn_close_Click" />
    </div>
    <div class="PageContent">
        <table class="generalTable">
            <tr>
                <th class="TableHeaders">Datum</th>
                <th class="TableHeaders">Essen bestellt</th>
                <th class="TableHeaders">In Essensbörse anbieten</th>
                <th class="TableHeaders">Menü</th>
                <th class="TableHeaders">Preis</th>
                <th class="TableHeaders">Essensbörse</th>
            </tr>
            <tr >
                <td> 
                    <asp:Label ID="lbl_dateMonday" runat="server"></asp:Label>
                </td>
                <td> 
                    <asp:CheckBox ID="chkBox_foodMonday" runat="server" />
                </td>
                <td> 
                    <asp:CheckBox ID="chkBox_foodExchangeMonday" runat="server" />
                </td>
                <td>
                    <table class="tableMenu">
                        <tr>
                            <td>Beilage: </td>
                            <td>
                                <asp:Label ID="lbl_sideDishMonday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 1: </td>
                            <td>
                                <asp:Label ID="lbl_menu1Monday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 2: </td>
                            <td>
                                <asp:Label ID="lbl_menu2Monday" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Label ID="lbl_priceMonday" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_closeFoodExchangeMonday" runat="server"></asp:Label>
                </td>
           </tr>
           <tr>
                <td class="auto-style1" > 
                    <asp:Label ID="lbl_dateTuesday" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkBox_foodTuesday" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="chkBox_foodExchangeTuesday" runat="server" />
                </td>
                <td>
                    <table class="tableMenu">
                        <tr>
                            <td>Beilage: </td>
                            <td>
                                <asp:Label ID="lbl_sideDishTuesday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 1: </td>
                            <td>
                                <asp:Label ID="lbl_menu1Tuesday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 2: </td>
                            <td>
                                <asp:Label ID="lbl_menu2Tuesday" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
               <td>
                   <asp:Label ID="lbl_priceTuesday" runat="server"></asp:Label>
                </td>
               <td>
                   <asp:Label ID="lbl_closeFoodExchangeTuesday" runat="server"></asp:Label>
                </td>
           </tr>
            <tr>
                <td class="auto-style5" > 
                    <asp:Label ID="lbl_dateWendesday" runat="server"></asp:Label>
                </td>
                <td class="auto-style6">
                    <asp:CheckBox ID="chkBox_foodWendesday" runat="server" />
                </td>
                <td class="auto-style6">
                    <asp:CheckBox ID="chkBox_foodExchangeWendesday" runat="server" />
                </td>
                <td>
                     <table class="tableMenu">
                        <tr>
                            <td>Beilage: </td>
                            <td>
                                <asp:Label ID="lbl_sideDishWendesday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 1: </td>
                            <td>
                                <asp:Label ID="lbl_menu1Wendesday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 2: </td>
                            <td>
                                <asp:Label ID="lbl_menu2Wendesday" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Label ID="lbl_priceWendesday" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_closeFoodExchangeWendesday" runat="server"></asp:Label>
                </td>
           </tr>
            <tr>
                <td class="auto-style1" > 
                    <asp:Label ID="lbl_dateThursday" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkBox_foodThursday" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="chkBox_foodExchangeThursday" runat="server" />
                </td>
                <td>
                    <table class="tableMenu">
                        <tr>
                            <td class="auto-style11">Beilage: </td>
                            <td class="auto-style11">
                                <asp:Label ID="lbl_sideDishThursday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 1: </td>
                            <td>
                                <asp:Label ID="lbl_menu1Thursday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 2: </td>
                            <td>
                                <asp:Label ID="lbl_menu2Thursday" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <asp:Label ID="lbl_priceThursday" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbl_closeFoodExchangeThursday" runat="server"></asp:Label>
                </td>
           </tr>
           <tr>
                <td class="auto-style1" > 
                    <asp:Label ID="lbl_dateFriday" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkBox_foodFriday" runat="server" />
                </td>
                <td>
                    <asp:CheckBox ID="chkBox_foodExchangeFriday" runat="server" />
                </td>
                <td>
                    <table class="tableMenu">
                        <tr>
                            <td>Beilage: </td>
                            <td class="auto-style10">
                                <asp:Label ID="lbl_sideDishFriday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 1: </td>
                            <td class="auto-style10">
                                <asp:Label ID="lbl_menu1Friday" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 2: </td>
                            <td class="auto-style10">
                                <asp:Label ID="lbl_menu2Friday" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
               <td>
                   <asp:Label ID="lbl_priceFriday" runat="server"></asp:Label>
                </td>
               <td>
                   <asp:Label ID="lbl_closeFoodExchangeFriday" runat="server"></asp:Label>
                </td>
           </tr>
        </table>
        <asp:Label runat="server" ID="lbl_Info" ForeColor="Red"></asp:Label>
    </div>
    </form>
</html>
