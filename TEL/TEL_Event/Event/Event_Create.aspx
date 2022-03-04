<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_Create.aspx.cs" Inherits="Event_Event_Create" StylesheetTheme="Event" Culture="auto" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            //活動開始日期
            $('#<%= tbEventSDate.ClientID%>').prop("readonly", true).datepicker({
                dateFormat: 'yy/mm/dd',
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $('#<%= tbEventEDate.ClientID%>').datepicker("option", "minDate", selectedDate);
                }
            });
            //活動結束日期
            $('#<%= tbEventEDate.ClientID%>').prop("readonly", true).datepicker({
                dateFormat: 'yy/mm/dd',
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $('#<%= tbEventSDate.ClientID%>').datepicker("option", "maxDate", selectedDate);
                    $('#<%= tbEventEDate.ClientID%>').val($(this).val());
                }
            });
            //報名開始日期時間
            $('#<%= tbSignupSDate.ClientID%>').prop("readonly", true).datetimepicker({
                dateFormat: 'yy/mm/dd',
                timeFormat: 'HH:mm',
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $('#<%= tbSignupEDate.ClientID%>').datepicker("option", "minDate", selectedDate);
                }
            });
            //報名結束日期時間
            $('#<%= tbSignupEDate.ClientID%>').prop("readonly", true).datetimepicker({
                dateFormat: 'yy/mm/dd',
                timeFormat: 'HH:mm',
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $('#<%= tbSignupSDate.ClientID%>').datepicker("option", "maxDate", selectedDate);
                    $('#<%= tbSignupEDate.ClientID%>').val($(this).val());
                }
            });
        });

        function ShowDialogTemplate() {
            $(function () {
                $("#dialogTemplate").dialog({
                    title: "",
                    modal: true,
                    width: "1050px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+0", },
                    buttons: {
                        Close: function () {
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

        //是否刪除 訊息開窗
        function ShowDialogDelete(id) {
            $(function () {
                $("#dialogDelete").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    buttons: [
                        {
                            text: "確定",
                            click: function () {
                                onDelete(id);
                                $(this).dialog("close");
                            }
                        },
                        {
                            text: "取消",
                            click: function () {
                                $(this).dialog("close");
                            }
                        }
                    ],
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel6").style.display = "block";
                    }
                });
            });

        };
        //刪除資料events
        function onDelete(id) {
            PageMethods.DeleteEvent(id, Success, Failure);
        }

        //刪除資料events Success callback
        function Success(result) {
            //ShowDialogSuccessReload(result);
            //刪除成功 reload gridview
            <%= btnGoBackEventPage.ClientID%>.click();
        }

        //刪除資料events Failure callback
        function Failure(error) {
            ShowDialogFailed();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Link_CreateEvents.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="建立活動"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblEventName" runat="server" Text="活動名稱"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="tbEventName" runat="server" CssClass="QueryField" Width="304px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEventCategory" runat="server" Text="活動分類" Width="150px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEventSDate" runat="server" Text="活動開始日期" Width="150px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEventEDate" runat="server" Text="活動結束日期" Width="150px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass="QueryField" Width="150px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="tbEventSDate" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="tbEventEDate" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPeopleLimit" runat="server" Text="人數限制"></asp:Label>
                <asp:Label ID="lblPeopleLimit2" runat="server" Text="(無上限勿填)"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSignupSDate" runat="server" Text="報名開始日期時間"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSignupEDate" runat="server" Text="報名結束日期時間"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbPeopleLimit" runat="server" Width="150px" CssClass="QueryField" onkeypress="if(event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="tbSignupSDate" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="tbSignupEDate" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblOrtherEventManager" runat="server" Text="其它活動管理者"></asp:Label>
                <asp:Label ID="lblOrtherEventManager2" runat="server" Text="(格式:工號,工號)"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="tbOrtherEventManager" runat="server" Width="304px" CssClass="QueryField"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSignupTemplate" runat="server" Text="報名表模板"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblQuestionnaireTemplate" runat="server" Text="問卷模板"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlSignupTemplate" runat="server" Style="width: 150px" CssClass="QueryField" OnSelectedIndexChanged="ddlSignupTemplate_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Selected="True" Value="">- 未指定 -</asp:ListItem>
                    <asp:ListItem Value="1">活動報名(簡易)</asp:ListItem>
                    <asp:ListItem Value="2">活動報名(詳細)</asp:ListItem>
                    <asp:ListItem Value="6">活動報名(地點時間)</asp:ListItem>
                    <asp:ListItem Value="5">上傳附件</asp:ListItem>
                    <asp:ListItem Value="3">健康檢查報名(Local)</asp:ListItem>
                    <asp:ListItem Value="4">健康檢查報名(駐在員)</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlQuestionnaireTemplate" runat="server" Style="width: 150px" CssClass="QueryField" OnSelectedIndexChanged="ddlQuestionnaireTemplate_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Selected="True" Value="">- 未指定 -</asp:ListItem>
                    <asp:ListItem Value="1">滿意度(講座)</asp:ListItem>
                    <asp:ListItem Value="2">滿意度(活動)</asp:ListItem>
                    <asp:ListItem Value="3">滿意度(健檢)</asp:ListItem>
                    <asp:ListItem Value="4">滿意度(電腦替換)</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPublish" runat="server" Text="是否上架"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblDuplicated" runat="server" Text="是否允許重覆報名"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="rblPublis" runat="server" RepeatDirection="Horizontal" CssClass="controlCommon">
                    <asp:ListItem Value="Y">是</asp:ListItem>
                    <asp:ListItem Value="" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td colspan="2">
                <asp:RadioButtonList ID="rblDuplicated" runat="server" RepeatDirection="Horizontal" CssClass="controlCommon">
                    <asp:ListItem Value="Y">是</asp:ListItem>
                    <asp:ListItem Value="" Selected="True">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblEventMember" runat="server" Text="活動成員"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <div>
                                <asp:RadioButtonList ID="rblEventMember" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblEventMember_SelectedIndexChanged" AutoPostBack="true" CssClass="controlCommon">
                                    <asp:ListItem Value="A" Selected="True">全體社員</asp:ListItem>
                                    <asp:ListItem Value="C">自訂</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div>
                                <div>
                                    <asp:CheckBoxList ID="cblCustMember" runat="server" Visible="false" CssClass="controlCommon">
                                    </asp:CheckBoxList>
                                </div>
                                <div style="display: flex">
                                    <div style="display: inline-table;">
                                        <asp:TextBox ID="tbCustMember" runat="server" Width="330px" Visible="false" CssClass="QueryField"></asp:TextBox>
                                    </div>
                                    <div style="display: inline-table;">
                                        <asp:Label ID="lblCustMember" runat="server" Text="(格式:工號,工號)" Style="font-size: 17px;" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblEventDescription" runat="server" Text="活動說明"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtEditor"></ajaxToolkit:HtmlEditorExtender>
                <asp:TextBox ID="txtEditor" runat="server" Width="100%" Height="200" />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblThumbnail" runat="server" Text="活動縮圖（限上傳jpg、jpeg、png、gif，尺寸為 360x240）"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblThumbnailName" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Button ID="btnThumbnail" runat="server" Text="修改" Visible="false" OnClick="btnThumbnail_Click" />
                <asp:FileUpload ID="FileUploadThumbnail" runat="server" accept=".jpg,.jpeg,.png,.gif" CssClass="controlCommon" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblPicture" runat="server" Text="活動大圖（限上傳jpg、jpeg、png、gif，尺寸為 600x400）"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblPictureName" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Button ID="btnPicture" runat="server" Text="修改" Visible="false" OnClick="btnPicture_Click" />
                <asp:FileUpload ID="FileUploadPicture" runat="server" accept=".jpg,.jpeg,.png,.gif" CssClass="controlCommon" />
            </td>
        </tr>
        <tr>
            <td style="padding-top:15px">
                <asp:Button ID="btnNextStep" runat="server" Text="下一步" OnClick="btnNextStep_Click" Width="145px" CssClass="Button" />
            </td>
            <td style="padding-top:15px">
                <asp:Button ID="btnCancel" runat="server" Text="取消" Width="145px" OnClick="btnCancel_Click" CssClass="Button" />
            </td>
            <td></td>
        </tr>
    </table>
    <div id="dialogTemplate" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none">
            <asp:Image ID="imgTemplate" runat="server" />
        </asp:Panel>
    </div>

    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。"></asp:Label>
            <asp:Label ID="lblErrMsgTxt" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label><br />
            <asp:Label ID="lblErrMsg" runat="server" Text="" Visible="false"></asp:Label><br />
        </asp:Panel>
    </div>

    <div id="dialogDelete" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none">
            <asp:Label ID="lblDeleteWarning" runat="server" Text="確定刪除該筆資料？"></asp:Label>
        </asp:Panel>
    </div>

    <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="false"></asp:Label>
    <asp:Label ID="lblThumbnail1" runat="server" Text="活動縮圖" Visible="false"></asp:Label>
    <asp:Label ID="lblPicture1" runat="server" Text="活動大圖" Visible="false"></asp:Label>
    <asp:Label ID="lblExtension" runat="server" Text=" 限上傳jpg、jpeg、png、gif圖檔" Visible="false"></asp:Label>
    <asp:Label ID="lblInvalidEmpid" runat="server" Text="無效工號，請重新輸入。" Visible="false"></asp:Label>
    <asp:Label ID="lblCantDelete" runat="server" Text="已有報名資料，無法刪除。" Visible="false"></asp:Label>
    <asp:Label ID="lblUnselect" runat="server" Text="- 未指定 -" Visible="false"></asp:Label>
    <asp:Label ID="lblRegisterDateErr" runat="server" Text="報名開始日期時間不可以晚於、等於報名結束日期時間" Visible="false"></asp:Label>

    <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
    <asp:Button ID="btnGoBackEventPage" runat="server" Text="" OnClick="btnGoBackEventPage_Click" Style="display: none;" />
</asp:Content>

