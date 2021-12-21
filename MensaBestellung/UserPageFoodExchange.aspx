<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPageFoodExchange.aspx.cs" Inherits="MensaBestellung.UserPageFoodExchange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="generalStyle.css" rel="stylesheet" type="text/css"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 137px;
        }
        .auto-style2 {
            width: 160px;
        }
        .auto-style3 {
            width: 241px;
        }
        .auto-style4 {
            width: 173px;
        }
        .auto-style5 {
            height: 25px;
        }
    </style>
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
            <table class="generalTable">
            <tr>
                <th class="TableHeaders">Datum</th>
                <th class="TableHeaders">Essen nachkaufen</th>
                <th class="TableHeaders">Menü</th>
                <th class="TableHeaders">Anbieter</th>
                
            </tr>
            <tr>
                <td class="auto-style1"> 
                    <asp:Label ID="lbl_date" runat="server"></asp:Label>
                </td>
                <td class="auto-style2"> 
                    <asp:CheckBox ID="chkBox_buyExchangeFood" runat="server" />
                </td>
                <td class="auto-style3"> 
                    <table class="tableMenu">
                        <tr>
                            <td>Beilage: </td>
                            <td>
                                <asp:Label ID="lbl_sideDish" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Menü 1: </td>
                            <td class="auto-style5">
                                <asp:Label ID="lbl_menu1" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Menü 2: </td>
                            <td>
                                <asp:Label ID="lbl_menu2" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="auto-style4">
                    
                    <asp:Label ID="lbl_provider" runat="server"></asp:Label>
                    
                </td>
           </tr>
        </table>
        </div>
    </div>
    </form>
</html>
