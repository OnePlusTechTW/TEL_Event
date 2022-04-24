<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_RegisterModel4_Create.aspx.cs" Inherits="Event_Event_RegisterModel4_Create" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource2" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>
<%@ Register Src="~/Event/UserControl/PageLoader.ascx" TagPrefix="uc1" TagName="PageLoader" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(
            function () {
                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
                    //受診者出生年月日
                    $('#<%= txtExamineebirthday.ClientID%>').prop("readonly", true).datepicker({
                        dateFormat: 'yy/mm/dd',
                        changeMonth: true,
                        changeYear: true,
                        showMonthAfterYear: true,
                        yearRange: 'c-80:c'
                    });
                });
            }
        );

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
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
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
                    <td>
                        <asp:Label ID="lblHealthGroup" runat="server" Text="健檢組別" meta:resourcekey="lblHealthGroupResource1"></asp:Label>
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
                    <td>
                        <asp:TextBox ID="txtHealthGroup" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtHealthGroupResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblIdentity" runat="server" Text="受診者身份別" meta:resourcekey="lblIdentityResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineename" runat="server" Text="受診者姓名" meta:resourcekey="lblExamineenameResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineename2" runat="server" Text="受診者拼音" meta:resourcekey="lblExamineename2Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineeidno" runat="server" Text="受診者居留證字號" meta:resourcekey="lblExamineeidnoResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineebirthday" runat="server" Text="受診者出生年月日" meta:resourcekey="lblExamineebirthdayResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineemobile" runat="server" Text="受診者手機" meta:resourcekey="lblExamineemobileResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlIdentity" runat="server" CssClass="QueryField" Width="100%" meta:resourcekey="ddlIdentityResource1">
                             <asp:ListItem Value="" meta:resourcekey="ListItemResource1">- 未指定 -</asp:ListItem>
                            <asp:ListItem Value="社員" meta:resourcekey="ListItemResource2">社員</asp:ListItem>
                            <asp:ListItem Value="家屬" meta:resourcekey="ListItemResource3">家屬</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineename" runat="server" CssClass="QueryField" meta:resourcekey="txtExamineenameResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineename2" runat="server" CssClass="QueryField" meta:resourcekey="txtExamineename2Resource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineeidno" runat="server" CssClass="QueryField" MaxLength="10" meta:resourcekey="txtExamineeidnoResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineebirthday" MaxLength="10" runat="server" CssClass="QueryField" meta:resourcekey="txtExamineebirthdayResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineemobile" MaxLength="10" runat="server" CssClass="QueryField" onkeypress="if(event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" meta:resourcekey="txtExamineemobileResource1"></asp:TextBox>
                    </td>

                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblHosipital" runat="server" Text="健檢醫院" meta:resourcekey="lblHosipitalResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblArea" runat="server" Text="地區" meta:resourcekey="lblAreaResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSolution" runat="server" Text="費用&方案" meta:resourcekey="lblSolutionResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGender" runat="server" Text="受診者性別" meta:resourcekey="lblGenderResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExpectdate" runat="server" Text="期望受檢日" meta:resourcekey="lblExpectdateResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSeconddate" runat="server" Text="備用受檢日" meta:resourcekey="lblSeconddateResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlHosipital" runat="server" CssClass="QueryField" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlHosipital_SelectedIndexChanged" meta:resourcekey="ddlHosipitalResource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource4">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="QueryField" Width="100%" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" meta:resourcekey="ddlAreaResource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource5">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSolution" runat="server" CssClass="QueryField" Width="100%" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlSolution_SelectedIndexChanged" meta:resourcekey="ddlSolutionResource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource6">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="QueryField" Width="100%" Enabled="False" AutoPostBack="True" OnSelectedIndexChanged="ddlGender_SelectedIndexChanged" meta:resourcekey="ddlGenderResource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource7">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlExpectdate" runat="server" CssClass="QueryField" Width="100%" Enabled="False" meta:resourcekey="ddlExpectdateResource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource8">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSeconddate" runat="server" CssClass="QueryField" Width="100%" Enabled="False" meta:resourcekey="ddlSeconddateResource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource9">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblSecondsolution1" runat="server" Text="健檢次方案1" meta:resourcekey="lblSecondsolution1Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSecondsolution2" runat="server" Text="健檢次方案2" meta:resourcekey="lblSecondsolution2Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSecondsolution3" runat="server" Text="健檢次方案3" meta:resourcekey="lblSecondsolution3Resource1"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlSecondsolution1" runat="server" CssClass="QueryField" Width="100%" Enabled="False" meta:resourcekey="ddlSecondsolution1Resource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource10">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSecondsolution2" runat="server" CssClass="QueryField" Width="100%" Enabled="False" meta:resourcekey="ddlSecondsolution2Resource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource11">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSecondsolution3" runat="server" CssClass="QueryField" Width="100%" Enabled="False" meta:resourcekey="ddlSecondsolution3Resource1">
                            <asp:ListItem Selected="True" Value="" meta:resourcekey="ListItemResource12">- 未指定 -</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblOptional" runat="server" Text="自費加選項目" meta:resourcekey="lblOptionalResource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblAddress" runat="server" Text="健檢包寄送地點" meta:resourcekey="lblAddressResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMeal" runat="server" Text="餐點樣式" meta:resourcekey="lblMealResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtOptional" runat="server" TextMode="MultiLine" Width="100%" Height="100px" CssClass="QueryField" meta:resourcekey="txtOptionalResource1"></asp:TextBox>
                    </td>
                    <td colspan="2" style="vertical-align: top;">
                        <asp:RadioButtonList ID="rblAddress" runat="server" CssClass="controlCommon" RepeatColumns="2" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblAddress_SelectedIndexChanged" meta:resourcekey="rblAddressResource1">
                        </asp:RadioButtonList>
                        <asp:RadioButton ID="rbtnOrther" runat="server" Text="其它" CssClass="controlCommon" AutoPostBack="True" OnCheckedChanged="rbtnOrther_CheckedChanged" meta:resourcekey="rbtnOrtherResource1" />
                        <asp:TextBox ID="txtOrther" runat="server" CssClass="QueryField" meta:resourcekey="txtOrtherResource1"></asp:TextBox>
                    </td>
                    <td style="vertical-align: top;">
                        <asp:DropDownList ID="ddlMeal" runat="server" CssClass="QueryField" Width="100%" meta:resourcekey="ddlMealResource1">
                            <asp:ListItem Value="" meta:resourcekey="ListItemResource13">- 未指定 -</asp:ListItem>
                            <asp:ListItem Value="日式" meta:resourcekey="ListItemResource14">日式</asp:ListItem>
                            <asp:ListItem Value="西式" meta:resourcekey="ListItemResource15">西式</asp:ListItem>
                            <asp:ListItem Value="中式" meta:resourcekey="ListItemResource16">中式</asp:ListItem>
                            <asp:ListItem Value="素食" meta:resourcekey="ListItemResource17">素食</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblNeedhotel" runat="server" Text="是否預約飯店" meta:resourcekey="lblNeedhotelResource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblCheckininfo" runat="server" Text="住宿人名單" meta:resourcekey="lblCheckininfoResource1"></asp:Label>
                        <asp:Label ID="lblCheckininfo2" runat="server" Text="(例：野原廣治 35歲)" meta:resourcekey="lblCheckininfo2Resource1"></asp:Label>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rblNeedhotel" runat="server" CssClass="controlCommon" AutoPostBack="True" OnSelectedIndexChanged="rblNeedhotel_SelectedIndexChanged" meta:resourcekey="rblNeedhotelResource1">
                            <asp:ListItem Value="是" Text="是" meta:resourcekey="ListItemResource18"></asp:ListItem>
                            <asp:ListItem Value="否" Text="否" Selected="True" meta:resourcekey="ListItemResource19"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td colspan="2" style="vertical-align: top">
                        <asp:TextBox ID="txtCheckininfo" runat="server" TextMode="MultiLine" CssClass="QueryField" Width="100%" Height="100%" Enabled="False" meta:resourcekey="txtCheckininfoResource1"></asp:TextBox>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋" meta:resourcekey="lblCommentResource1"></asp:Label>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="100px" CssClass="QueryField" meta:resourcekey="txtCommentResource1"></asp:TextBox>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Button ID="btnSummit" runat="server" Text="送出" OnClientClick="ShowProgressBar();" CssClass="Button" OnClick="btnSummit_Click" meta:resourcekey="btnSummitResource1" />
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
            <asp:Label ID="lblFormatError" runat="server" Text="欄位 {0} 格式錯誤。" Visible="False" meta:resourcekey="lblFormatErrorResource1"></asp:Label>
            <asp:Label ID="lblUnselect" runat="server" Text="- 未指定 -" Visible="False" meta:resourcekey="lblUnselectResource1"></asp:Label>
            <asp:Label ID="lblLimitReached" runat="server" Text="此方案報名人數已達上限，請重新選擇其他方案" Visible="False" meta:resourcekey="lblLimitReachedResource1"></asp:Label>
            <asp:Label ID="lblSendMailFailed" runat="server" Text="但報名成功通知mail寄送失敗。" Visible="False" meta:resourcekey="lblSendMailFailedResource1"></asp:Label>
            <asp:Button ID="btnGoBackPage" runat="server" Text="" OnClick="btnGoBackPage_Click" Style="display: none;" meta:resourcekey="btnGoBackPageResource1" />
            <asp:Label ID="lblRegisterErrMsg" runat="server" Text="報名資料新增發生錯誤。" Visible="False" meta:resourcekey="lblRegisterErrMsgResource1"></asp:Label>
            <asp:Label ID="lblIDFormatErr" runat="server" Text="居留證字號格式錯誤。" Visible="False" meta:resourcekey="lblIDFormatErrResource1"></asp:Label>

            <asp:Label ID="lblEmailSubject" runat="server" Text="【通知】活動填寫完成_{0}" Visible="False" meta:resourcekey="lblEmailSubjectResource1"></asp:Label>
            <asp:Label ID="lblEmailContent1" runat="server" Text="您好:" Visible="False" meta:resourcekey="lblEmailContent1Resource1"></asp:Label>
            <asp:Label ID="lblEmailContent2" runat="server" Text="此封信件為通知您參與了『<a href='{0}'>{1}</a>』，並完成報名。" Visible="False" meta:resourcekey="lblEmailContent2Resource1"></asp:Label>
            <asp:Label ID="lblEmailContent3" runat="server" Text="相關報名資訊，可以至網站『<a href='{0}'>我的活動</a>』頁面中查看！" Visible="False" meta:resourcekey="lblEmailContent3Resource1"></asp:Label>
            <asp:Label ID="lblEmailContent4" runat="server" Text="如果有任何問題請聯絡活動單位負責人，謝謝。" Visible="False" meta:resourcekey="lblEmailContent4Resource1"></asp:Label>
            <asp:Label ID="lblEmailContent5" runat="server" Text="※此信件為系統發送通知使用，請勿直接回覆。" Visible="False" meta:resourcekey="lblEmailContent5Resource1"></asp:Label>

            <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
            <asp:Label ID="lblWarningText" runat="server" Text="警告" meta:resourcekey="lblWarningTextResource1" Style="display: none;"></asp:Label>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSummit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

