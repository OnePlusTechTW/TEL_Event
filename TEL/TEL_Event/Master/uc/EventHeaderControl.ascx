<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventHeaderControl.ascx.cs" Inherits="Master_uc_EventHeaderControl" %>
<link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">

<style type="text/css">
    .MenuBarStyle, .MenuBarStyle a:link, .MenuBarStyle a:visited, .MenuBarStyle a:active {
        color: #d3d3d3;
        font-family: Microsoft JhengHei, Georgia;
        text-decoration: none;
        border-radius: 6px;
    }

    .MenuBarStyle a:hover {
        font-family: Microsoft JhengHei, Georgia;
        text-decoration: none;
        border-radius: 6px;
    }

    .MenuBackGround {
        background-image: url('~/Master/images/menu_background.jpg');
        background-repeat: repeat-x;
        border-radius: 12px;

    }
</style>

<table cellspacing="0" cellpadding="0" style="border-width: 0px; width: 100%; height: 60px; background-image: url('../Master/images/BANNER.png')">
    <tr style="vertical-align: middle">
        <td>
            <table>
                <tr>
                    <td style="width: 5px"></td>
                    <td>
                        <asp:Menu ID="FIELD_MenuBar" runat="server" ForeColor="black" Font-Bold="true"
                            Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" CssClass="MenuBarStyle">
                            <DynamicHoverStyle Width="140px" BackColor="white" />
                            <DynamicMenuItemStyle Width="160px" Height="30px" Font-Names="Microsoft JhengHei, Georgia" Font-Size="16px"  />
                            <DynamicMenuStyle HorizontalPadding="10px" VerticalPadding="5px" BackColor="Lavender" Width="160px" CssClass="MenuBackGround" BorderColor="white" BorderStyle="Solid" BorderWidth="1px" />
                            <Items>
                                <asp:MenuItem ImageUrl="~/Master/images/MENU.png" Text="" Value="" meta:resourcekey="MenuItemResource4">
                                    <asp:MenuItem Text="首頁" Value="首頁" NavigateUrl="~/Event/Default.aspx" meta:resourcekey="Link_Home"></asp:MenuItem>
                                    <asp:MenuItem Text="我的活動" Value="我的活動" NavigateUrl="~/Event/MyEvent.aspx" meta:resourcekey="Link_MyEvent"></asp:MenuItem>
                                    <asp:MenuItem Text="過去活動" Value="過去活動" NavigateUrl="~/Event/PastEvent.aspx" meta:resourcekey="Link_PastEvent"></asp:MenuItem>
                                </asp:MenuItem>
                            </Items>
                        </asp:Menu>
                    </td>
                    <td style="width: 5px"></td>
                    <td>
                        <asp:Image ID="FIELD_Logo" runat="server" ImageUrl="~/Master/images/TEL.png" />
                    </td>
                    <td style="width: 430px"></td>
                    <td>
                        <a href="Default.aspx">
                            <img src="../Master/images/FIESTA.png" /></a>
                    </td>
                    <td style="width: 400px"></td>
                    <td style="vertical-align: middle; color:black; font-size:18px; font-family:Microsoft JhengHei, Georgia; font-weight:bold">
                        <asp:Label ID="LABEL_Username" runat="server" CssClass="LargeBold"></asp:Label>
                    </td>
                </tr>
            </table>

        </td>
    </tr>
</table>
