<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageLoader.ascx.cs" Inherits="Event_UserControl_PageLoader" %>

<script>
    // 顯示讀取遮罩
    function ShowProgressBar() {
        displayProgress();
        displayMaskFrame();
    }

    // 隱藏讀取遮罩
    function HideProgressBar() {
        var progress = $('#divProgress');
        var maskFrame = $("#divMaskFrame");
        progress.hide();
        maskFrame.hide();
    }
    // 顯示讀取畫面
    function displayProgress() {
        var w = $(document).width();
        var h = $(window).height();
        var progress = $('#divProgress');
        progress.css({ "z-index": 999999, "top": (h / 2) - (progress.height() / 2), "left": (w / 2) - (progress.width() / 2) });
        progress.show();
    }
    // 顯示遮罩畫面
    function displayMaskFrame() {
        var w = $(window).width();
        var h = $(document).height();
        var maskFrame = $("#divMaskFrame");
        maskFrame.css({ "z-index": 999998, "opacity": 0.7, "width": w, "height": h });
        maskFrame.show();
    }
</script>

<div id="divProgress" style="text-align: center; display: none; position: fixed; top: 50%; left: 50%;">
    <asp:Image ID="imgLoading" runat="server" ImageUrl="~/Sample/Img/loading.gif" meta:resourcekey="imgLoadingResource1" />
    <br />
    <asp:Label ID="lblLoadingText" runat="server" Text="資料處理中" style="color:#1B3563; font-size:2px;" meta:resourcekey="lblLoadingTextResource1"></asp:Label>
</div>
<div id="divMaskFrame" style="background-color: #F2F4F7; display: none; left: 0px; position: absolute; top: 0px;">
</div>
