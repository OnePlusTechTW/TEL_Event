<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_Edit.aspx.cs" Inherits="Event_Event_Edit" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource2"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .ajax__html_editor_extender_popupDiv {
            display: none;
        }
    </style>
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

        function ShowDialogTemplate(title) {
            $(function () {
                $("#dialogTemplate").dialog({
                    title: title,
                    modal: true,
                    width: "1200px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", },
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

        //上傳副件開窗
        function ShowDialogFileUpload(event, id) {
            $(function () {
                var dialog = $("#dialogFileUpload").dialog({
                    title: "",
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel7").style.display = "block";
                    },
                    width: "450px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", }
                });
                dialog.parent().appendTo(jQuery("form:first"));
            });

        };

        //是否刪除 訊息開窗
        function ShowDialogDelete(id) {
            $(function () {
                $("#dialogDelete").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
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
            $('#<%=btnGoBackEventPage.ClientID%>')[0].click();

        }

        //刪除資料events Failure callback
        function Failure(error) {
            ShowDialogFailed();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/icon2.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="編輯活動" meta:resourcekey="lblPageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblEventName" runat="server" Text="活動名稱" meta:resourcekey="lblEventNameResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="tbEventName" runat="server" CssClass="QueryField" Width="304px" MaxLength="64" meta:resourcekey="tbEventNameResource1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblEventCategory" runat="server" Text="活動分類" Width="150px" meta:resourcekey="lblEventCategoryResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEventSDate" runat="server" Text="活動開始日期" Width="150px" meta:resourcekey="lblEventSDateResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEventEDate" runat="server" Text="活動結束日期" Width="150px" meta:resourcekey="lblEventEDateResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass="QueryField" Width="150px" meta:resourcekey="ddlEventCategoryResource1">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="tbEventSDate" runat="server" Width="150px" CssClass="QueryField" meta:resourcekey="tbEventSDateResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="tbEventEDate" runat="server" Width="150px" CssClass="QueryField" meta:resourcekey="tbEventEDateResource1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPeopleLimit" runat="server" Text="人數限制" meta:resourcekey="lblPeopleLimitResource1"></asp:Label>
                <asp:Label ID="lblPeopleLimit2" runat="server" Text="(無上限勿填)" meta:resourcekey="lblPeopleLimit2Resource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSignupSDate" runat="server" Text="報名開始日期時間" meta:resourcekey="lblSignupSDateResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblSignupEDate" runat="server" Text="報名結束日期時間" meta:resourcekey="lblSignupEDateResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbPeopleLimit" runat="server" Width="150px" CssClass="QueryField" onkeypress="if(event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" meta:resourcekey="tbPeopleLimitResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="tbSignupSDate" runat="server" Width="150px" CssClass="QueryField" meta:resourcekey="tbSignupSDateResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="tbSignupEDate" runat="server" Width="150px" CssClass="QueryField" meta:resourcekey="tbSignupEDateResource1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblOrtherEventManager" runat="server" Text="其它活動管理者" meta:resourcekey="lblOrtherEventManagerResource1"></asp:Label>
                <asp:Label ID="lblOrtherEventManager2" runat="server" Text="(格式:工號,工號)" meta:resourcekey="lblOrtherEventManager2Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="tbOrtherEventManager" runat="server" Width="304px" CssClass="QueryField" meta:resourcekey="tbOrtherEventManagerResource1"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblSignupTemplate" runat="server" Text="報名表模板" meta:resourcekey="lblSignupTemplateResource1"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblQuestionnaireTemplate" runat="server" Text="問卷模板" meta:resourcekey="lblQuestionnaireTemplateResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="ddlSignupTemplate" runat="server" Style="width: 150px" CssClass="QueryField" OnSelectedIndexChanged="ddlSignupTemplate_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlSignupTemplateResource1">
                    <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource1">- 未指定 -</asp:ListItem>
                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource2">活動報名(簡易)</asp:ListItem>
                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource3">活動報名(詳細)</asp:ListItem>
                    <asp:ListItem Value="6" meta:resourcekey="ListItemResource4">活動報名(地點時間)</asp:ListItem>
                    <asp:ListItem Value="5" meta:resourcekey="ListItemResource5">上傳附件</asp:ListItem>
                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource6">健康檢查報名(Local)</asp:ListItem>
                    <asp:ListItem Value="4" meta:resourcekey="ListItemResource7">健康檢查報名(駐在員)</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlQuestionnaireTemplate" runat="server" Style="width: 150px" CssClass="QueryField" OnSelectedIndexChanged="ddlQuestionnaireTemplate_SelectedIndexChanged" AutoPostBack="True" meta:resourcekey="ddlQuestionnaireTemplateResource1">
                    <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource8">- 未指定 -</asp:ListItem>
                    <asp:ListItem Value="1" meta:resourcekey="ListItemResource9">滿意度(講座)</asp:ListItem>
                    <asp:ListItem Value="2" meta:resourcekey="ListItemResource10">滿意度(活動)</asp:ListItem>
                    <asp:ListItem Value="3" meta:resourcekey="ListItemResource11">滿意度(健檢)</asp:ListItem>
                    <asp:ListItem Value="4" meta:resourcekey="ListItemResource12">滿意度(電腦替換)</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPublish" runat="server" Text="是否上架" meta:resourcekey="lblPublishResource1"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblDuplicated" runat="server" Text="是否允許重覆報名" meta:resourcekey="lblDuplicatedResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButtonList ID="rblPublis" runat="server" RepeatDirection="Horizontal" CssClass="controlCommon" meta:resourcekey="rblPublisResource1">
                    <asp:ListItem Value="Y" meta:resourcekey="ListItemResource13">是</asp:ListItem>
                    <asp:ListItem Value="" Selected="True" meta:resourcekey="ListItemResource14">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td colspan="2">
                <asp:RadioButtonList ID="rblDuplicated" runat="server" RepeatDirection="Horizontal" CssClass="controlCommon" meta:resourcekey="rblDuplicatedResource1">
                    <asp:ListItem Value="Y" meta:resourcekey="ListItemResource15">是</asp:ListItem>
                    <asp:ListItem Value="" Selected="True" meta:resourcekey="ListItemResource16">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblEventMember" runat="server" Text="活動成員" meta:resourcekey="lblEventMemberResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div>
                            <div>
                                <asp:RadioButtonList ID="rblEventMember" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblEventMember_SelectedIndexChanged" AutoPostBack="True" CssClass="controlCommon" meta:resourcekey="rblEventMemberResource1">
                                    <asp:ListItem Value="A" Selected="True" meta:resourcekey="ListItemResource17">全體社員</asp:ListItem>
                                    <asp:ListItem Value="C" meta:resourcekey="ListItemResource18">自訂</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div>
                                <div>
                                    <asp:CheckBoxList ID="cblCustMember" runat="server" Visible="False" CssClass="controlCommon" meta:resourcekey="cblCustMemberResource1">
                                    </asp:CheckBoxList>
                                </div>
                                <div style="display: flex">
                                    <div style="display: inline-table;">
                                        <asp:TextBox ID="tbCustMember" runat="server" Width="330px" Visible="False" CssClass="QueryField" meta:resourcekey="tbCustMemberResource1"></asp:TextBox>
                                    </div>
                                    <div style="display: inline-table;">
                                        <asp:Label ID="lblCustMember" runat="server" Text="(格式:工號,工號)" Style="font-size: 17px;" Visible="False" meta:resourcekey="lblCustMemberResource1"></asp:Label>
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
                <asp:Label ID="lblEventDescription" runat="server" Text="活動說明" meta:resourcekey="lblEventDescriptionResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <ajaxToolkit:HtmlEditorExtender ID="HtmlEditorExtender1" runat="server" TargetControlID="txtEditor" BehaviorID="HtmlEditorExtender1"></ajaxToolkit:HtmlEditorExtender>
                <asp:TextBox ID="txtEditor" runat="server" Width="100%" Height="200px" meta:resourcekey="txtEditorResource1" />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblThumbnail" runat="server" Text="活動縮圖（限上傳jpg、jpeg、png、gif，尺寸為 320x180）" meta:resourcekey="lblThumbnailResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Image ID="imgThumbnail" runat="server" Width="160px" Height="90px" />
                <asp:Label ID="lblThumbnailName" runat="server" meta:resourcekey="lblThumbnailNameResource1" ></asp:Label>
                <asp:Button ID="btnUploadThumbnail" runat="server" Text="上傳檔案" CssClass="Button" OnClick="btnUploadThumbnail_Click" meta:resourcekey="btnUploadThumbnailResource1" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblPicture" runat="server" Text="活動大圖（限上傳jpg、jpeg、png、gif，尺寸為 640x360）" meta:resourcekey="lblPictureResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Image ID="imgPicture" runat="server" Width="160px" Height="90px" />
                <asp:Label ID="lblPictureName" runat="server" meta:resourcekey="lblPictureNameResource1" ></asp:Label>
                <asp:Button ID="btnUploadPicture" runat="server" Text="上傳檔案" CssClass="Button" OnClick="btnUploadPicture_Click" meta:resourcekey="btnUploadPictureResource1"  />
            </td>
        </tr>
        <tr>
            <td style="padding-top:15px">
                <asp:Button ID="btnNextStep" runat="server" Text="下一步" OnClick="btnNextStep_Click" Width="145px" CssClass="Button" meta:resourcekey="btnNextStepResource1" />
            </td>
            <td style="padding-top:15px">
                <asp:Button ID="btnDelete" runat="server" Text="刪除" Width="145px" CssClass="Button" OnClick="btnDelete_Click" meta:resourcekey="btnDeleteResource1"/>
            </td>
            <td style="padding-top:15px">
                <asp:Button ID="btnCancel" runat="server" Text="取消" Width="145px" OnClick="btnCancel_Click" CssClass="Button" meta:resourcekey="btnCancelResource1" />
            </td>
        </tr>
    </table>
    <div id="dialogTemplate" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none" meta:resourcekey="ContentPanel2Resource1">
            <asp:Image ID="imgTemplate" runat="server" meta:resourcekey="imgTemplateResource1" />
        </asp:Panel>
    </div>

    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none" meta:resourcekey="ContentPanel1Resource1">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none" meta:resourcekey="ContentPanel3Resource1">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。" meta:resourcekey="lblFailedResource1"></asp:Label>
            <asp:Label ID="lblErrMsgTxt" runat="server" Text="錯誤訊息：" Visible="False" meta:resourcekey="lblErrMsgTxtResource1"></asp:Label><br />
            <asp:Label ID="lblErrMsg" runat="server" Visible="False" meta:resourcekey="lblErrMsgResource1"></asp:Label><br />
        </asp:Panel>
    </div>

    <div id="dialogDelete" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none" meta:resourcekey="ContentPanel6Resource1">
            <asp:Label ID="lblDeleteWarning" runat="server" Text="確定刪除該筆資料？" meta:resourcekey="lblDeleteWarningResource1"></asp:Label>
        </asp:Panel>
    </div>

     <div id="dialogFileUpload" title="Dialog Title">
        <asp:Panel ID="ContentPanel7" runat="server" Style="display: none" meta:resourcekey="ContentPanel7Resource1" >
            <div>
                <asp:FileUpload ID="FileUploadThumbnail" runat="server" accept=".jpg,.jpeg,.png,.gif" CssClass="controlCommon" meta:resourcekey="FileUploadThumbnailResource1" />
            </div>
            <div style="margin-top: 20px;">
                <asp:Button ID="btnImport" runat="server" Text="確定" CssClass="Button" OnClick="btnImport_Click" meta:resourcekey="btnImportResource1" />
            </div>
        </asp:Panel>
    </div>

    <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="False" meta:resourcekey="lblRequiredResource1"></asp:Label>
    <asp:Label ID="lblThumbnail1" runat="server" Text="活動縮圖" Visible="False" meta:resourcekey="lblThumbnail1Resource1"></asp:Label>
    <asp:Label ID="lblPicture1" runat="server" Text="活動大圖" Visible="False" meta:resourcekey="lblPicture1Resource1"></asp:Label>
    <asp:Label ID="lblExtension" runat="server" Text=" 限上傳jpg、jpeg、png、gif圖檔" Visible="False" meta:resourcekey="lblExtensionResource1"></asp:Label>
    <asp:Label ID="lblInvalidEmpid" runat="server" Text="無效工號，請重新輸入。" Visible="False" meta:resourcekey="lblInvalidEmpidResource1"></asp:Label>
    <asp:Label ID="lblCantDelete" runat="server" Text="已有報名資料，無法刪除。" Visible="False" meta:resourcekey="lblCantDeleteResource1"></asp:Label>
    <asp:Label ID="lblUnselect" runat="server" Text="- 未指定 -" Visible="False" meta:resourcekey="lblUnselectResource1"></asp:Label>
    <asp:Label ID="lblRegisterDateErr" runat="server" Text="報名開始日期時間不可以晚於、等於報名結束日期時間" Visible="False" meta:resourcekey="lblRegisterDateErrResource1"></asp:Label>

    <asp:HiddenField ID="hfUploadType" runat="server" />
    <asp:Button ID="btnGoBackEventPage" runat="server" OnClick="btnGoBackEventPage_Click" Style="display: none;" meta:resourcekey="btnGoBackEventPageResource1" />

    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblWarningText" runat="server" Text="警告" meta:resourcekey="lblWarningTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblComplete" runat="server" Text="完成" Style="display: none;" meta:resourcekey="lblCompleteResource1"></asp:Label>

    <asp:Label ID="lblSignupTemplatePreview" runat="server" Text="報名表模板預覽" Style="display: none;"  meta:resourcekey="lblSignupTemplatePreviewResource1" ></asp:Label>
    <asp:Label ID="lblQuestionnaireTemplatePreview" runat="server" Text="問卷模板預覽" Style="display: none;" meta:resourcekey="lblQuestionnaireTemplatePreviewResource1" ></asp:Label>

</asp:Content>

