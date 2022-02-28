<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Event_Register" StylesheetTheme="Event" Culture="auto" UICulture="auto"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function ShowDialogView(page) {
            $(function () {
                $("#dialogView").dialog({
                    title: "",
                    modal: true,
                    width: "1200px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+100", },
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });
                $("#dialogView").load(page);
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
            PageMethods.DeleteRegisterModel(id, Success, Failure);
        }

        //刪除資料events Success callback
        function Success(result) {
            //ShowDialogSuccessReload(result);
            //刪除成功 reload gridview
            <%= btnReloadGridView.ClientID%>.click();
        }

        //刪除資料events Failure callback
        function Failure(error) {
            ShowDialogFailed();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon2.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="管理報名資料"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td id="TD_Category" runat="server" style="text-align: center; width: 120px">
                <asp:Label ID="lblCategory" runat="server" CssClass="ShowCategory"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEventName" runat="server" CssClass="ShowCategory"></asp:Label>
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
                <asp:Label ID="lblEmpName" runat="server" Text="工號或姓名"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtEmpName" runat="server" Width="120px" CssClass="QueryField"></asp:TextBox></td>
            <td>
            <td>
                <asp:Button ID="btnQuery" runat="server" Text="查詢"  OnClick="btnQuery_Click" CssClass="Button" />
            </td>
        </tr>
    </table>
    <table cellspacing="5">
        <tr style="text-align: right">
            <td style="width: 100%; padding-right:20px; padding-bottom:10px;">
                <asp:Image ID="FIELD_People" runat="server" ImageUrl="~/Master/images/people.png" Height="20px" />
                <asp:Label ID="lblCount" runat="server" CssClass="ShowPeopleCount"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnPreCreate" runat="server" Text="新增報名" CssClass="Button" OnClick="btnPreCreate_Click" />
            </td>
            <td>
                <asp:Button ID="btnExportExcel" runat="server" Text="匯出Excel" CssClass="Button" OnClick="btnExportExcel_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gridRegister" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    OnRowDataBound="gridRegister_RowDataBound" OnPageIndexChanging="gridRegister_PageIndexChanging" PageSize="20">
                    <Columns>
                        <asp:BoundField HeaderText="工號" DataField="empid">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="中文姓名" DataField="empnamech">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="英文姓名"  DataField="empnameen">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="報名日期" DataField="registerdate">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="最後修改人員" DataField="modifiedby">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="最後修改日期" DataField="modifieddate">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="報名資料">
                                <HeaderStyle Width="150px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="編輯" CssClass="Button_Gridview" CommandArgument='<%# Eval("registerid") %>' OnClick="btnEdit_Click"/>
                                    <asp:Button ID="btnView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("registerid")%>' OnClick="btnView_Click"/>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        
                        <asp:TemplateField>
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("registerid") %>' OnClick="btnDelete_Click" />
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

    <div id="dialogView" title="Dialog Title"></div>

    <div id="dialogDelete" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none">
            <asp:Label ID="lblDeleteWarning" runat="server" Text="確定刪除該筆資料？"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。"></asp:Label>
            <asp:Label ID="lblErrMsgTxt" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label><br />
            <asp:Label ID="lblErrMsg" runat="server" Text="" Visible="false"></asp:Label><br />
        </asp:Panel>
    </div>

    <asp:Label ID="lblLimit" runat="server" Text="無限制" Visible="false"></asp:Label>
    <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />

    <asp:Button ID="btnReloadGridView" runat="server" OnClick="btnReloadGridView_Click" style="display:none;"/>
</asp:Content>

