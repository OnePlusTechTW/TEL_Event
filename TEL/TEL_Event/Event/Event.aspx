<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Event_Event" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<%@ Register Src="~/Event/UserControl/PageLoader.ascx" TagPrefix="uc1" TagName="PageLoader" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    		
    <script>
        $(function () {

            $('#<%= sDate.ClientID%>').datepicker({
                dateFormat: 'yy/mm/dd',
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $('#<%= eDate.ClientID%>').datepicker("option", "minDate", selectedDate);
                }
            });

            $('#<%= eDate.ClientID%>').datepicker({
                dateFormat: 'yy/mm/dd',
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $('#<%= sDate.ClientID%>').datepicker("option", "maxDate", selectedDate);
                    $('#<%= eDate.ClientID%>').val($(this).val());
                }
            });


        });

        function ShowDialogView(id) {
            $(function () {
                $("#dialogView").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    width: "1200px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+0", },
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });

                $("#dialogView").load('Event_View.aspx?id=' + id);
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

        function ShowDialogSurveyPublish() {
            $(function () {
                $("#dialogSurveyPublish").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    buttons: [
                        {
                            text: "確定",
                            click: function () {
                                $('#<%=btnSurveyPublishEvent.ClientID%>')[0].click();
                                ShowProgressBar();
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageLoader runat="server" ID="PageLoader" />
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon2.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="管理活動" meta:resourcekey="lblPageNameResource1"></asp:Label>
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
            <td >&nbsp;
                <asp:Label ID="lblDateInterval" runat="server" Text="活動開始日期區間" meta:resourcekey="lblDateIntervalResource1"></asp:Label>
            </td>
            <td >&nbsp;
                <asp:Label ID="lblEventStatus" runat="server" Text="活動狀態" meta:resourcekey="lblEventStatusResource1"></asp:Label>
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
                <asp:TextBox ID="sDate" runat="server" Width="160px"  CssClass="QueryField" meta:resourcekey="sDateResource1"></asp:TextBox>
                ~
                <asp:TextBox ID="eDate" runat="server" Width="160px"  CssClass="QueryField" meta:resourcekey="eDateResource1"></asp:TextBox>
            </td>
            <td  >&nbsp;
                <asp:DropDownList ID="ddlEventStatus" runat="server" CssClass="QueryField" meta:resourcekey="ddlEventStatusResource1">
                    <asp:ListItem Text="- 全部 -" Value="" meta:resourcekey="ListItemResource1" ></asp:ListItem>
                    <asp:ListItem Text="尚未開始" Value="N" meta:resourcekey="ListItemResource2" ></asp:ListItem>
                    <asp:ListItem Text="進行中" Value="O" meta:resourcekey="ListItemResource3" ></asp:ListItem>
                    <asp:ListItem Text="已結束" Value="F" meta:resourcekey="ListItemResource4" ></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td >&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" CssClass="Button" meta:resourcekey="btnSearchResource1"  />
            </td>
        </tr>
    </table>
    <table style="width:100%; text-align:right">
        <tr>
            <td>
                <asp:Button ID="tbAddEvent" runat="server" Text="新增" OnClick="tbAddEvent_Click" CssClass="Button" meta:resourcekey="tbAddEventResource1" />
            </td>
        </tr>
    </table>
    <table style="padding-top:10px" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gridEvents" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True" PageSize="20"
                        EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White" OnPageIndexChanging="gridEvent_PageIndexChanging" OnRowDataBound="gridEvent_RowDataBound" meta:resourcekey="gridEventsResource1" >
                        <Columns>
                            <asp:BoundField HeaderText="活動名稱" DataField="eventname" meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle Width="200px" ></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動分類" DataField="categoryname" meta:resourcekey="BoundFieldResource2">
                                <HeaderStyle Width="95px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:TemplateField HeaderText="報名開始日期時間" meta:resourcekey="TemplateFieldResource1">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRegisterstart" runat="server" CssClass=" " meta:resourcekey="lblRegisterstartResource1"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="報名結束日期時間" meta:resourcekey="TemplateFieldResource2">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRegisterend" runat="server" CssClass=" " meta:resourcekey="lblRegisterendResource1"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="活動開始日期" DataField="eventstart" meta:resourcekey="BoundFieldResource3">
                                <HeaderStyle Width="105px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動結束日期" DataField="eventend" meta:resourcekey="BoundFieldResource4">
                                <HeaderStyle  Width="105px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="活動狀態" meta:resourcekey="TemplateFieldResource3">
                                <HeaderStyle  Width="95px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" CssClass=" " runat="server" meta:resourcekey="lblStatusResource1"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="上架" meta:resourcekey="TemplateFieldResource4">
                                <HeaderStyle  Width="50px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblEnable" CssClass=" " runat="server" meta:resourcekey="lblEnableResource1"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="活動資訊" meta:resourcekey="TemplateFieldResource5">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnMaintain" runat="server" Text="編輯" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnMaintain_Click" meta:resourcekey="btnMaintainResource1" />
                                    <asp:Button ID="btnView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnView_Click" meta:resourcekey="btnViewResource1" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="報名資料" meta:resourcekey="TemplateFieldResource6">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnRegisterManage" runat="server" Text="管理" CssClass="Button_Gridview"  CommandArgument='<%# Eval("eventnid") %>' OnClick="btnRegisterManage_Click" meta:resourcekey="btnRegisterManageResource1"/>
                                    <asp:Button ID="btnRegisterExport" runat="server" Text="匯出" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnRegisterExport_Click" meta:resourcekey="btnRegisterExportResource1"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="問卷資料" meta:resourcekey="TemplateFieldResource7">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnSurveyManage" runat="server" Text="管理" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnSurveyManage_Click" meta:resourcekey="btnSurveyManageResource1"/>
                                    <asp:Button ID="btnSurveyExport" runat="server" Text="匯出" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnSurveyExport_Click" meta:resourcekey="btnSurveyExportResource1"/>
                                    <asp:Button ID="btnSurveyPublish" runat="server" Text="發送問券" CssClass="Button_Gridview" OnClick="btnSurveyPublish_Click" CommandArgument='<%# Eval("eventnid") +","+ Eval("eventname")+","+ Eval("registermodel")+","+ Eval("surveymodel") %>' meta:resourcekey="btnSurveyPublishResource1"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                        <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    <div id="dialogView" title="Dialog Title">
        
    </div>

    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none" meta:resourcekey="ContentPanel1Resource1">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogSurveyPublish" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none" meta:resourcekey="ContentPanel6Resource1">
            <asp:Label ID="lblSurveyWarning" runat="server" Text="是否發送問卷？" meta:resourcekey="lblSurveyWarningResource1"></asp:Label>
        </asp:Panel>
    </div>

    <asp:Label ID="lblSurveyFailed" runat="server" Text="問券發送失敗。" Visible="False" meta:resourcekey="lblSurveyFailedResource1"></asp:Label>
    <asp:Label ID="lblNoRegister" runat="server" Text="尚無人員報名。" Visible="False" meta:resourcekey="lblNoRegisterResource1"></asp:Label>
    <asp:Label ID="item_all" runat="server" Text="- 全部 -" style="display:none" meta:resourcekey="item_allResource1"></asp:Label>
    <asp:Label ID="lblNYStart" runat="server" Text="尚未開始" Visible="False" meta:resourcekey="lblNYStartResource1"></asp:Label>
    <asp:Label ID="lblInProgress" runat="server" Text="進行中" Visible="False" meta:resourcekey="lblInProgressResource1"></asp:Label>
    <asp:Label ID="lblEnd" runat="server" Text="已結束" Visible="False" meta:resourcekey="lblEndResource1"></asp:Label>
    <asp:Label ID="lblEnableYes" runat="server" Text="是" Visible="False" meta:resourcekey="lblEnableYesResource1"></asp:Label>
    <asp:Label ID="lblEnableNo" runat="server" Text="否" Visible="False" meta:resourcekey="lblEnableNoResource1"></asp:Label>
    <asp:HiddenField ID="hfIsManager" runat="server" />

    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblEmailSubject" runat="server" Text="【通知】滿意度問卷填寫通知_{0}" Visible="False" meta:resourcekey="lblEmailSubjectResource1"></asp:Label>
    <asp:Label ID="lblEmailContent1" runat="server" Text="您好:" Visible="False" meta:resourcekey="lblEmailContent1Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent2" runat="server" Text="此封信件為通知您參與了『<a href='{0}'>{1}（超連結）</a>』，請點選連結進行滿意度問卷填寫。" Visible="False" meta:resourcekey="lblEmailContent2Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent3" runat="server" Text="相關報名資訊，可以至網站『<a href='{0}'>我的活動（超連結）</a>』頁面中查看！" Visible="False" meta:resourcekey="lblEmailContent3Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent4" runat="server" Text="如果有任何問題請聯絡活動單位負責人，謝謝。" Visible="False" meta:resourcekey="lblEmailContent4Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent5" runat="server" Text="※此信件為系統發送通知使用，請勿直接回覆。" Visible="False" meta:resourcekey="lblEmailContent5Resource1"></asp:Label>

    <asp:Button ID="btnSurveyPublishEvent" runat="server" Text="Button" OnClick="btnSurveyPublishEvent_Click" Style="display: none;" />
    <asp:HiddenField ID="hfSurveyPublisParam" runat="server" />
</asp:Content>

