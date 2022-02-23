<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_RegisterModel5_Create.aspx.cs" Inherits="Event_Event_RegisterModel5_Create" StylesheetTheme="Event" Culture="auto" UICulture="auto" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function ShowDialogMsg() {
            $(function () {
                $("#dialogMsg").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
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
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    width: "700px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", },
                    buttons: {
                        Close: function () {
                            <%= btnGoBackPage.ClientID%>.click();
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
                    title: $('#<%=hfWarning.ClientID%>')[0].value,
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEmpid" runat="server" Text="工號"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCName" runat="server" Text="中文姓名"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEName" runat="server" Text="英文姓名"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtEmpid" runat="server" ReadOnly="true" CssClass="QueryField"  Width="100%"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtCName" runat="server" ReadOnly="true" CssClass="QueryField"  Width="100%"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtEName" runat="server" ReadOnly="true" CssClass="QueryField"  Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td>
                <asp:Label ID="lblDepartment" runat="server" Text="部門"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblStation" runat="server" Text="勤務地"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="true" CssClass="QueryField"  Width="100%"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtStation" runat="server" ReadOnly="true" CssClass="QueryField"  Width="100%"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="3">
                <asp:Label ID="txtFileUpload1" runat="server" Text="上傳附件1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="3">
                <asp:Label ID="lblDescription1" runat="server" Text="上傳附件1之說明"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtDescription1" runat="server" CssClass="QueryField" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="3">
                <asp:Label ID="txtFileUpload2" runat="server" Text="上傳附件2"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:FileUpload ID="FileUpload2" runat="server" />
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="3">
                <asp:Label ID="lblDescription2" runat="server" Text="上傳附件2之說明"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtDescription2" runat="server" CssClass="QueryField" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="3">
                <asp:Label ID="txtFileUpload3" runat="server" Text="上傳附件3"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:FileUpload ID="FileUpload3" runat="server" />
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="3">
                <asp:Label ID="lblDescription3" runat="server" Text="上傳附件3之說明"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtDescription3" runat="server" CssClass="QueryField" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle" >
            <td colspan="3">
                <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="135px" CssClass="QueryField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="padding-top:15px">
                <asp:Button ID="btnSummit" runat="server" Text="送出" CssClass="Button" Width="99%" OnClick="btnSummit_Click" />
            </td>
            <td style="padding-top:15px">
                <asp:Button ID="btnCannel" runat="server" Text="取消" CssClass="Button" Width="99%" OnClick="btnCannel_Click" />
            </td>
            <td>
            </td>
        </tr>
    </table>

    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogRegisterSccess" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none">
            <asp:Label ID="lblRegisterSccess" runat="server" Text="報名成功"></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。"></asp:Label>
            <asp:Label ID="lblErrMsgTxt" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label><br />
            <asp:Label ID="lblErrMsg" runat="server" Text="" Visible="false"></asp:Label><br />
        </asp:Panel>
    </div>

    <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="false"></asp:Label>
    <asp:Label ID="lblUnselect" runat="server" Text="- 未指定 -" Visible="false"></asp:Label>
    <asp:Label ID="lblLimitReached" runat="server" Text="此方案報名人數已達上限，請重新選擇其他方案" Visible="false"></asp:Label>
    <asp:Label ID="lblSendMailFailed" runat="server" Text="但報名成功通知mail寄送失敗。" Visible="false"></asp:Label>
    <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
    <asp:Button ID="btnGoBackPage" runat="server" Text="Button" OnClick="btnGoBackPage_Click" style="display:none;" />
</asp:Content>

