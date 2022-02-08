<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Event_Default" StylesheetTheme="Event" Culture="auto" UICulture="auto" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>


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

        .ddlColor20 {
            background-color: #FAAA8D !important;
        }

        .ddlColor21 {
            background-color: #A65523 !important;
        }
    </style>
    <script>
        function ShowDialogEventView() {
            $(function () {
                //$("#dialogView").load('Event_Create.aspx?id=af3bb0d8-3850-4e29-b469-400f372d3868');

                $("#dialogEventView").dialog({
                    title: "",
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
                        document.getElementById("ContentPlaceHolder1_ContentPanel1").style.display = "block";
                    }
                });
            });

        }

        function ShowDialogRegisterModel1_Create(id, registermodel) {
            
            $(function () {
                $("#dialogRegisterCreate").load('Event_RegisterModel' + registermodel + '_Create.aspx?id=' + id);

                $("#dialogRegisterCreate").dialog({
                    title: "",
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
                <asp:Image runat="server" ImageUrl="~/Master/images/DefaultEvent.jpg" Height="40px"></asp:Image>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEventName" runat="server" Text="活動名稱"></asp:Label>
            </td>
            <td style="padding-left: 5px;">
                <asp:Label ID="lblEventCategory" runat="server" Text="活動分類"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbEventName" runat="server" CssClass="QueryField"></asp:TextBox>
            </td>
            <td style="padding-left: 5px;">
                <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass="QueryField">
                </asp:DropDownList>
            </td>
            <td style="padding-left: 5px;">
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" CssClass="Button" />
            </td>
        </tr>
    </table>
    <div>
        <asp:Table ID="Table1" runat="server"></asp:Table>
    </div>
    <div id="dialogRegisterCreate" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none">
                
        </asp:Panel>
    </div>

    <div id="dialogEventView" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
        </asp:Panel>
    </div>
    <asp:Label ID="lblLimit" runat="server" Text="無限制" Visible="false"></asp:Label>
    <asp:Label ID="lblSignup" runat="server" Text="馬上報名" Visible="false"></asp:Label>
    <asp:Label ID="lblView" runat="server" Text="檢視報名" Visible="false"></asp:Label>
    <asp:Label ID="lblNYStart" runat="server" Text="尚未開放報名" Visible="false"></asp:Label>
    <asp:Label ID="item_all" runat="server" Text="- 全部 -" style="display:none"></asp:Label>




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

