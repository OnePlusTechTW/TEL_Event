<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Event_Default" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .Color1 {
            color: #00A9E0 !important;
        }

        .Color2 {
            color: #71C5E8 !important;
        }

        .Color3 {
            color: #00629B !important;
        }

        .Color4 {
            color: #78BE20 !important;
        }

        .Color5 {
            color: #B7DD79 !important;
        }

        .Color6 {
            color: #658D1B !important;
        }

        .Color7 {
            color: #DA1884 !important;
        }

        .Color8 {
            color: #F395C7 !important;
        }

        .Color9 {
            color: #A50050 !important;
        }

        .Color10 {
            color: #00B2A9 !important;
        }

        .Color11 {
            color: #9CDBD9 !important;
        }

        .Color12 {
            color: #007367 !important;
        }

        .Color13 {
            color: #8031A7 !important;
        }

        .Color14 {
            color: #CAA2DD !important;
        }

        .Color15 {
            color: #572C5F !important;
        }

        .Color16 {
            color: #EEDC00 !important;
        }

        .Color17 {
            color: #F0EC74 !important;
        }

        .Color18 {
            color: #BBA600 !important;
        }

        .Color19 {
            color: #FF6A13 !important;
        }

        .Color20 {
            color: #FAAA8D !important;
        }

        .Color21 {
            color: #A65523 !important;
        }
    </style>
    <script>
        function ShowDialogView(page, eventid, id) {
            $(function () {
                $("#dialogView").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    width: "900px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+100", },
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });

                switch (page) {
                    case 'Event_View':
                        $("#dialogView").load('Event_View.aspx?id=' + eventid);
                        break;
                    default:
                        $("#dialogView").load(page + '.aspx?eventid=' + eventid + '&id=' + id + '&page=Default');
                        break;
                }

            });

        }

        function ShowDialogRegisterModel1_Create(id, registermodel) {
            
            $(function () {
                $("#dialogRegisterCreate").load('Event_RegisterModel' + registermodel + '_Create.aspx?id=' + id);

                $("#dialogRegisterCreate").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    width: "645px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+100", },
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder2_ContentPanel1").style.display = "block";
                    }
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/DefaultEvent.jpg" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEventName" runat="server" Text="活動名稱" meta:resourcekey="lblEventNameResource1"></asp:Label>
            </td>
            <td >&nbsp;
                <asp:Label ID="lblEventCategory" runat="server" Text="活動分類" meta:resourcekey="lblEventCategoryResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbEventName" runat="server" CssClass="QueryField" meta:resourcekey="tbEventNameResource1"></asp:TextBox>
            </td>
            <td >&nbsp;
                <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass="QueryField" meta:resourcekey="ddlEventCategoryResource1">
                </asp:DropDownList>
            </td>
            <td >&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" CssClass="Button" meta:resourcekey="btnSearchResource1" />
            </td>
        </tr>
    </table>
    <div>
        <asp:Table ID="Table1" runat="server" meta:resourcekey="Table1Resource1"></asp:Table>
    </div>
    <div id="dialogRegisterCreate" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none" meta:resourcekey="ContentPanel2Resource1">
                
        </asp:Panel>
    </div>

    <div id="dialogView" title="Dialog Title">
        
    </div>
    <asp:Label ID="lblLimit" runat="server" Text="Unlimited" Visible="False" meta:resourcekey="lblLimitResource1"></asp:Label>
    <asp:Label ID="lblSignup" runat="server" Text="馬上報名" Visible="False" meta:resourcekey="lblSignupResource1"></asp:Label>
    <asp:Label ID="lblView" runat="server" Text="檢視報名" Visible="False" meta:resourcekey="lblViewResource1"></asp:Label>
    <asp:Label ID="lblNYStart" runat="server" Text="尚未開放報名" Visible="False" meta:resourcekey="lblNYStartResource1"></asp:Label>
    <asp:Label ID="item_all" runat="server" Text="- 全部 -" style="display:none" meta:resourcekey="item_allResource1"></asp:Label>
    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>

    <%--<table>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>--%>
    <%--<div style="border-radius:20px; border:1px solid black; width:451px">
        <div >
            <asp:Image style="border-radius:17px 20px 0px 0px ;" ID="Image1" runat="server" ImageUrl="~/Event/EventThumbnail/0fc25346-8d04-455a-88a3-b165e7227c4b.png"/>
        </div>
        <div style="padding:20px">
            <div>
                <div style="font-size:20px; color:#0026ff">
                    ◆<asp:Label ID="Label9" runat="server" Text="2022年員工旅遊" Font-Size="18px" style="padding-left:5px;color:#000000;"></asp:Label>
                </div>
            </div>
            <div>
                <div style="font-size:20px;">
                    ◆<asp:Label ID="Label1" runat="server" Text="2021/1/1 ~ 2021/12/31" style="padding-left:5px;vertical-align: text-bottom;"></asp:Label>
                </div>
            </div>
            <div>
                <div style="font-size:20px;">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Master/images/1644240707820.jpg" Width="16px" /><asp:Label ID="Label2" runat="server" Text="199/250" style="padding-left:5px;"></asp:Label>
                </div>
            </div>
            <div>
                <div style="font-size:20px; color:#0026ff">
                    ◆<asp:Label ID="Label10" runat="server" Text="福委會" style="padding-left:5px; vertical-align: text-bottom; color:#0026ff;"></asp:Label>
                </div>
            </div>
            <div style="text-align:right;">
                <asp:Button ID="btnEventOpen" runat="server" Text="Button" OnClick="btnEventOpen_Click"  />
            </div>
        </div>
    </div>--%>
</asp:Content>

