<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_RegisterModel5_Create.aspx.cs" Inherits="Event_Event_RegisterModel5_Create" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource2" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>
<%@ Register Src="~/Event/UserControl/PageLoader.ascx" TagPrefix="uc1" TagName="PageLoader" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowDialogMsg() {
            $(function () {
                $("#dialogMsg").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    width: "700px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", },
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

        function ShowRegisterSccessDialog() {
            $(function () {
                $("#dialogRegisterSccess").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    width: "700px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", },
                    buttons: {
                        Close: function () {
                            $('#<%=btnGoBackPage.ClientID%>')[0].click();
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel2").style.display = "block";
                    }
                });
            });

        }

        //失敗通知 訊息開窗
        function ShowDialogFailed(ErrMsg) {
            $(function () {
                $("#dialogFailed").dialog({
                    title: document.getElementById('<%=lblWarningText.ClientID%>').innerText,
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel3").style.display = "block";
                        $('#<%= lblErrMsg.ClientID %>').text(ErrMsg);

                    }
                });
            });

        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageLoader runat="server" ID="PageLoader" />
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon3.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL_PageName" runat="server" CssClass="PageTitle" Text="馬上報名" meta:resourcekey="LABEL_PageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px"></tr>
    </table>
    <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblStation" runat="server" Text="勤務地" meta:resourcekey="lblStationResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmpid" runat="server" Text="工號" meta:resourcekey="lblEmpidResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCName" runat="server" Text="中文姓名" meta:resourcekey="lblCNameResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEName" runat="server" Text="英文姓名" meta:resourcekey="lblENameResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDepartment" runat="server" Text="部門" meta:resourcekey="lblDepartmentResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtStation" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtStationResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtEmpid" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtEmpidResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtCName" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtCNameResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtEName" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtENameResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDepartment" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtDepartmentResource1"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="txtFileUpload1" runat="server" Text="上傳附件1" meta:resourcekey="txtFileUpload1Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:FileUpload ID="FileUpload1" runat="server" meta:resourcekey="FileUpload1Resource2" />
            </td>
            <td colspan="2"></td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="lblDescription1" runat="server" Text="上傳附件1之說明" meta:resourcekey="lblDescription1Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtDescription1" runat="server" CssClass="QueryField" TextMode="MultiLine" Width="100%" Height="60px" meta:resourcekey="txtDescription1Resource1"></asp:TextBox>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="txtFileUpload2" runat="server" Text="上傳附件2" meta:resourcekey="txtFileUpload2Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:FileUpload ID="FileUpload2" runat="server" meta:resourcekey="FileUpload2Resource2" />
            </td>
            <td colspan="2"></td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="lblDescription2" runat="server" Text="上傳附件2之說明" meta:resourcekey="lblDescription2Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtDescription2" runat="server" CssClass="QueryField" TextMode="MultiLine" Width="100%" Height="60px" meta:resourcekey="txtDescription2Resource1"></asp:TextBox>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="txtFileUpload3" runat="server" Text="上傳附件3" meta:resourcekey="txtFileUpload3Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:FileUpload ID="FileUpload3" runat="server" meta:resourcekey="FileUpload3Resource2" />
            </td>
            <td colspan="2"></td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="lblDescription3" runat="server" Text="上傳附件3之說明" meta:resourcekey="lblDescription3Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtDescription3" runat="server" CssClass="QueryField" TextMode="MultiLine" Width="100%" Height="60px" meta:resourcekey="txtDescription3Resource1"></asp:TextBox>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋" meta:resourcekey="lblCommentResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="100px" CssClass="QueryField" meta:resourcekey="txtCommentResource1"></asp:TextBox>
            </td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Button ID="btnSummit" runat="server" Text="送出" CssClass="Button" OnClick="btnSummit_Click" OnClientClick="ShowProgressBar();" meta:resourcekey="btnSummitResource1" />
                <asp:Button ID="btnCannel" runat="server" Text="取消" CssClass="Button" OnClick="btnCannel_Click" meta:resourcekey="btnCannelResource1" />
            </td>
        </tr>
    </table>

    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none" meta:resourcekey="ContentPanel1Resource1">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogRegisterSccess" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none" meta:resourcekey="ContentPanel2Resource1">
            <asp:Label ID="lblRegisterSccess" runat="server" Text="報名成功" meta:resourcekey="lblRegisterSccessResource1"></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none" meta:resourcekey="ContentPanel3Resource1">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。" meta:resourcekey="lblFailedResource1"></asp:Label>
            <asp:Label ID="lblErrMsg" runat="server" meta:resourcekey="lblErrMsgResource1"></asp:Label><br />
        </asp:Panel>
    </div>

    <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="False" meta:resourcekey="lblRequiredResource1"></asp:Label>
    <asp:Label ID="lblUnselect" runat="server" Text="- 未指定 -" Visible="False" meta:resourcekey="lblUnselectResource1"></asp:Label>
    <asp:Label ID="lblLimitReached" runat="server" Text="此方案報名人數已達上限，請重新選擇其他方案" Visible="False" meta:resourcekey="lblLimitReachedResource1"></asp:Label>
    <asp:Label ID="lblSendMailFailed" runat="server" Text="但報名成功通知mail寄送失敗。" Visible="False" meta:resourcekey="lblSendMailFailedResource1"></asp:Label>
    <asp:Button ID="btnGoBackPage" runat="server" Text="" OnClick="btnGoBackPage_Click" Style="display: none;" meta:resourcekey="btnGoBackPageResource1" />
    <asp:Label ID="lblRegisterErrMsg" runat="server" Text="報名資料新增發生錯誤。" Visible="False" meta:resourcekey="lblRegisterErrMsgResource1"></asp:Label>

    <asp:Label ID="lblEmailSubject" runat="server" Text="【通知】活動填寫完成_{0}" Visible="False" meta:resourcekey="lblEmailSubjectResource1"></asp:Label>
    <asp:Label ID="lblEmailContent1" runat="server" Text="您好:" Visible="False" meta:resourcekey="lblEmailContent1Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent2" runat="server" Text="此封信件為通知您參與了『<a href='{0}'>{1}</a>』，並完成報名。" Visible="False" meta:resourcekey="lblEmailContent2Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent3" runat="server" Text="相關報名資訊，可以至網站『<a href='{0}'>我的活動</a>』頁面中查看！" Visible="False" meta:resourcekey="lblEmailContent3Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent4" runat="server" Text="如果有任何問題請聯絡活動單位負責人，謝謝。" Visible="False" meta:resourcekey="lblEmailContent4Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent5" runat="server" Text="※此信件為系統發送通知使用，請勿直接回覆。" Visible="False" meta:resourcekey="lblEmailContent5Resource1"></asp:Label>

    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblWarningText" runat="server" Text="警告" meta:resourcekey="lblWarningTextResource1" Style="display: none;"></asp:Label>
</asp:Content>

