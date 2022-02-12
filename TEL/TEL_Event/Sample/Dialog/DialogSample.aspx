<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DialogSample.aspx.cs" Inherits="Sample_Dialog_DialogSample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <link href="../../Scripts/jquery-ui.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.12.4.js"></script>
    <script src="../../Scripts/jquery-1.12.4.min.js"></script>
    <script src="../../Scripts/jquery-ui.js"></script>
    <script src="../../Scripts/jquery-ui.min.js"></script>
    <script src="../../Scripts/jQuery-Timepicker/jquery-ui-timepicker-addon.min.js"></script>
    <link href="../../Scripts/jQuery-Timepicker/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />
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
                        document.getElementById("ContentPanel1").style.display = "block";
                    }
                });
            });

        };
        function ShowDialogLoadPage() {

            $(function () {

                $("#dialogLoadPage").dialog({
                    title: "",
                    modal: true,
                    width: "645px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+100", },
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });

                $("#dialogLoadPage").load('Default.aspx');

            });
        }
    </script>
    <form id="form1" runat="server">
        <div>
            <div id="dialog" title="Dialog Title">
                <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
                    <%--dialog content--%>
                    <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </div>

            <div id="dialogLoadPage" title="Dialog Title">
            </div>
            <asp:Button ID="btnPopupDialog" runat="server" Text="PopupDialog" OnClick="btnPopupDialog_Click" />
            <%--檢視後端開窗--%>
            <asp:Button ID="btnViewDialog" runat="server" Text="btnViewDialog" OnClick="btnViewDialog_Click" />
            <%--檢視前端開窗--%>
            <asp:Button ID="btnViewDialogClient" runat="server" Text="btnViewDialogClient" OnClientClick="ShowDialogLoadPage();return false;" />
            <%--備註：
            當 $("#dialogLoadPage").load('xxx.aspx') 的xxx.aspx頁面中，有load jquery-1.12.4.js、jquery-1.12.4.min.js 時，dialog的close會壞掉，所以要把buttons 中的 Close拿掉，用右上角的X關閉視窗。
            反之，如果 xxx.aspx頁面沒有load jquery-1.12.4.js、jquery-1.12.4.min.js 的話，Close 事件可以work。--%>

        </div>
    </form>
</body>
</html>
