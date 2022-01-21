<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="DialogSample.aspx.cs" Inherits="Sample_Dialog_DialogSample" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%-- ↓↓不用加 --%>
    <link href="../../Scripts/jquery-ui.css" rel="stylesheet">
    <script src="./../Scripts/jquery-1.12.4.js"></script>
    <script src="../../Scripts/jquery-1.12.4.min.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <%-- ↑↑不用加 --%>
    <script type="text/javascript">
        function ShowPopup() {
            $(function () {
                $("#dialog").dialog({
                    title: "標題",
                    modal: true,
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

        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--dialog--%>
    <div id="dialog" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <%--dialog content--%>
            <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>
    <asp:Button ID="btnPopupDialo" runat="server" Text="PopupDialog" OnClick="btnPopupDialo_Click"/>
</asp:Content>

