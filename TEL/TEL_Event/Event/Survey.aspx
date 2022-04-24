<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Survey.aspx.cs" Inherits="Event_Survey" Culture="auto" StylesheetTheme="Event" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //刪除資料events
        function onDelete(id) {
            PageMethods.DeleteSurveyData(id, Success, Failure);
        }

        //刪除資料events Success callback
        function Success(result) {
            //ShowDialogSuccessReload(result);
            //刪除成功 reload gridview
            <%= btnReloadSurveyData.ClientID%>.click();
        }

        //刪除資料events Failure callback
        function Failure(error) {
            ShowDialogFailed();
        }

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
                        open: function () {
                            //打開dialog時，顯示panel
                            document.getElementById("ContentPlaceHolder1_ContentPanel6").style.display = "block";
                        }
                    });
                });

        };

        function ShowDialogSuccessReload(event) {
            $(function () {
                $("#dialogSuccess").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    buttons: {
                        Close: function () {
                            <%= btnReloadSurveyData.ClientID%>.click();

                            $(this).dialog('close');
                }
                    },
                open: function (event, ui) {
                    //打開dialog時，顯示panel
                    document.getElementById("ContentPlaceHolder1_ContentPanel2").style.display = "block";
                }
                });
            });

        };

        function ShowDialogLoadPage(url, width, height) {
            $(function () {

                $("#dialogLoadPage").dialog({
                    title: "",
                    modal: true,
                    width: "1100px",
                    Height: "650px",
                    position: { my: "center center", at: "center top+0", },
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });

                $("#dialogLoadPage").load(url);

            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon2.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL_PageName" runat="server" CssClass="PageTitle" meta:resourcekey="LABEL_PageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px"></tr>
    </table>
    <table>
        <tr>
            <td runat="server" id="category" style="max-width: 350px; padding-right: 13px; word-break: break-word;">
                <asp:Label ID="FIELD_Category" runat="server" Font-Size="25px" Font-Bold="True" meta:resourcekey="lblCategoryNameResource1"></asp:Label>
            </td>
            <td style="border-left: 1px solid black; padding-left: 15px; min-width: 600px; max-width: 800px;">
                <asp:Label ID="FIELD_EventName" runat="server" Text="" Font-Size="25px" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblEventNameResource1"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr style="height: 15px">
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="QUERY_EmpName" runat="server" meta:resourcekey="QUERY_EmpNameResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="FIELD_EmpName" runat="server" Width="120px" CssClass="QueryField"></asp:TextBox></td>
            <td>
            <td>
                &nbsp;<asp:Button ID="Button_Query" runat="server" Text="Button" meta:resourcekey="Button_QueryResource1" OnClick="Button_Query_Click" CssClass="Button" />
            </td>
        </tr>
    </table>
    <table cellspacing="0">
        <tr style="text-align: right">
            <td style="width: 100%; padding-right:10px;">
                <asp:Image ID="FIELD_People" runat="server" ImageUrl="~/Master/images/people.png" Height="20px" />
                <asp:Label ID="FIELD_Count" runat="server" CssClass="ShowPeopleCount"></asp:Label>
            </td>   
            <td>
                <asp:Button ID="Button_ExportExcel" runat="server" Text="Button" meta:resourcekey="Button_ExportExcelResource1" CssClass="Button" OnClick="Button_ExportExcel_Click" /></td>
        </tr>
        <tr style="height: 10px"></tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="FIELD_Result" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" meta:resourcekey="FIELD_ResultResource1" AutoGenerateColumns="False" BorderColor="White"
                    OnRowDataBound="FIELD_Result_RowDataBound" OnPageIndexChanging="FIELD_Result_PageIndexChanging" PageSize="20">
                    <Columns>
                        <asp:BoundField HeaderText="工號" meta:resourcekey="FIELD_Result_Empid" DataField="empid">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="中文姓名" meta:resourcekey="FIELD_Result_EmpNameCH" DataField="empnamech">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="英文姓名" meta:resourcekey="FIELD_Result_EmpNameEN" DataField="empnameen">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="填寫日期" meta:resourcekey="FIELD_Result_FillinDate" DataField="fillindate">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="問卷資料" meta:resourcekey="FIELD_Result_Survey">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_SurveyView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("surveyinfo") %>' OnClick="Button_SurveyView_Click"/>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_Delete" runat="server" Text="刪除" meta:resourcekey="Button_Delete" CssClass="Button_Gridview" CommandArgument='<%# Eval("surveyinfo") %>' 
                                    OnClientClick='<%# "ShowDialogDelete(\""+ Eval("surveyinfo") + "\");return false;" %>' />
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

    <div id="dialogDelete" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none">
            <asp:Label ID="lblDeleteWarning" runat="server" Text="您確定要刪除此筆資料？"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。"></asp:Label>
            <asp:Label ID="lblErrMsgTxt" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label><br />
            <asp:Label ID="lblErrMsg" runat="server" Text="" Visible="false"></asp:Label><br />
        </asp:Panel>
    </div>

    <div id="dialogSuccess" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none">
            <asp:Label ID="lblSuccess" runat="server" Text="成功。"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogLoadPage" title="Dialog Title"></div>

    <asp:Button ID="btnReloadSurveyData" runat="server" Text="Button" OnClick="btnReloadSurveyData_Click" Style="display: none;" />
    <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
    
</asp:Content>

