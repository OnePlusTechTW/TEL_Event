<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Event_Event" StylesheetTheme="Event" Culture="auto" UICulture="auto" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    		
    <script>
        $(function () {

            $('#<%= sDate.ClientID%>').prop("readonly", true).datepicker({
                dateFormat: 'yy/mm/dd',
                onClose: function (selectedDate) {
                    $('#<%= eDate.ClientID%>').datepicker("option", "minDate", selectedDate);
                }
            });

            $('#<%= eDate.ClientID%>').prop("readonly", true).datepicker({
                dateFormat: 'yy/mm/dd',
                onClose: function (selectedDate) {
                    $('#<%= sDate.ClientID%>').datepicker("option", "maxDate", selectedDate);
                    $('#<%= eDate.ClientID%>').val($(this).val());
                }
            });


        });

        function ShowDialogView(id) {
            $(function () {
                $("#dialogView").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
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

                $("#dialogView").load('Event_View.aspx?id=' + id);
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon2.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="管理活動"></asp:Label>
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
            <td >
                <asp:Label ID="lblEventCategory" runat="server" Text="活動分類"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblDateInterval" runat="server" Text="活動開始日期區間"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblEventStatus" runat="server" Text="活動狀態"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbEventName" runat="server" CssClass="QueryField"></asp:TextBox>
            </td>
            <td >
                <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass="QueryField">
                </asp:DropDownList>
            </td>
            <td >
                <asp:TextBox ID="sDate" runat="server" Width="160px"  CssClass="QueryField"></asp:TextBox>
                ~
                <asp:TextBox ID="eDate" runat="server" Width="160px"  CssClass="QueryField"></asp:TextBox>
            </td>
            <td  >
                <asp:DropDownList ID="ddlEventStatus" runat="server" CssClass="QueryField">
                    <asp:ListItem Text="- 全部 -" Value="" ></asp:ListItem>
                    <asp:ListItem Text="尚未開始" Value="N" ></asp:ListItem>
                    <asp:ListItem Text="進行中" Value="O" ></asp:ListItem>
                    <asp:ListItem Text="已結束" Value="F" ></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td >
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" CssClass="Button"  />
            </td>
        </tr>
    </table>
    <table style="width:100%; text-align:right">
        <tr>
            <td>
                <asp:Button ID="tbAddEvent" runat="server" Text="新增" OnClick="tbAddEvent_Click" CssClass="Button" />
            </td>
        </tr>
    </table>
    <table style="padding-top:10px" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gridEvents" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                        EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                        PageSize="20" OnPageIndexChanging="gridEvent_PageIndexChanging" OnRowDataBound="gridEvent_RowDataBound" >
                        <Columns>
                            <asp:BoundField HeaderText="活動名稱" DataField="eventname">
                                <HeaderStyle Width="200px" ></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動分類" DataField="categoryname">
                                <HeaderStyle Width="95px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                             <asp:TemplateField HeaderText="報名開始日期時間">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRegisterstart" runat="server" Text="" CssClass=" "></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="報名結束日期時間">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRegisterend" runat="server" Text="" CssClass=" "></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="活動開始日期" DataField="eventstart">
                                <HeaderStyle Width="105px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動結束日期" DataField="eventend">
                                <HeaderStyle  Width="105px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="活動狀態">
                                <HeaderStyle  Width="95px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" CssClass=" " runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="上架">
                                <HeaderStyle  Width="50px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblEnable" CssClass=" " runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="活動資訊">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnMaintain" runat="server" Text="編輯" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnMaintain_Click" />
                                    <asp:Button ID="btnView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid")%>' OnClick="btnView_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="報名資料">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnRegisterManage" runat="server" Text="管理" CssClass="Button_Gridview"  CommandArgument='<%# Eval("eventnid") %>' OnClick="btnRegisterManage_Click"/>
                                    <asp:Button ID="btnRegisterExport" runat="server" Text="匯出" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnRegisterExport_Click"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="問卷資料">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnSurveyManage" runat="server" Text="管理" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnSurveyManage_Click"/>
                                    <asp:Button ID="btnSurveyExport" runat="server" Text="匯出" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnSurveyExport_Click"/>
                                    <asp:Button ID="btnSurveyPublish" runat="server" Text="發送問券" CssClass="Button_Gridview" OnClick="btnSurveyPublish_Click" CommandArgument='<%# Eval("eventnid") +","+ Eval("eventname")+","+ Eval("registermodel")+","+ Eval("surveymodel")%>'/>
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
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>

    <asp:Label ID="lblSurveyFailed" runat="server" Text="問券發送失敗。" Visible="false"></asp:Label>
    <asp:Label ID="lblNoRegister" runat="server" Text="尚無人員報名。" Visible="false"></asp:Label>
    <asp:Label ID="item_all" runat="server" Text="- 全部 -" style="display:none"></asp:Label>
    <asp:Label ID="lblNYStart" runat="server" Text="尚未開始" Visible="false"></asp:Label>
    <asp:Label ID="lblInProgress" runat="server" Text="進行中" Visible="false"></asp:Label>
    <asp:Label ID="lblEnd" runat="server" Text="已結束" Visible="false"></asp:Label>
    <asp:Label ID="lblEnableYes" runat="server" Text="是" Visible="false"></asp:Label>
    <asp:Label ID="lblEnableNo" runat="server" Text="否" Visible="false"></asp:Label>
    <asp:HiddenField ID="hfIsManager" runat="server" />
    <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
</asp:Content>

