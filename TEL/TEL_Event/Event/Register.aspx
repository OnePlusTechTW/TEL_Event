<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Event_Register" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            ShowDialogFailed(document.getElementById('<%=lblDeleteErrMsg.ClientID%>').innerText);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon2.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="管理報名資料" meta:resourcekey="lblPageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td runat="server" id="category" style="max-width: 350px; padding-right: 13px; word-break: break-word;">
                <asp:Label ID="lblCategory" runat="server" Font-Size="25px" Font-Bold="True" meta:resourcekey="lblCategoryNameResource1"></asp:Label>
            </td>
            <td style="border-left: 1px solid black; padding-left: 15px; min-width: 600px; max-width: 800px;">
                <asp:Label ID="lblEventName" runat="server" Text="" Font-Size="25px" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblEventNameResource1"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEmpName" runat="server" Text="工號或姓名" meta:resourcekey="lblEmpNameResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtEmpName" runat="server" Width="120px" CssClass="QueryField" meta:resourcekey="txtEmpNameResource1"></asp:TextBox></td>
            <td>
            <td>&nbsp;
                <asp:Button ID="btnQuery" runat="server" Text="查詢" OnClick="btnQuery_Click" CssClass="Button" meta:resourcekey="btnQueryResource1" />
            </td>
        </tr>
    </table>
    <table cellspacing="5">
        <tr style="text-align: right">
            <td style="width: 100%; padding-right: 20px; padding-bottom: 10px;">
                <asp:Image ID="FIELD_People" runat="server" ImageUrl="~/Master/images/people.png" Height="20px" meta:resourcekey="FIELD_PeopleResource1" />
                <asp:Label ID="lblCount" runat="server" CssClass="ShowPeopleCount" meta:resourcekey="lblCountResource1"></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnPreCreate" runat="server" Text="新增報名" CssClass="Button" OnClick="btnPreCreate_Click" meta:resourcekey="btnPreCreateResource1" />
            </td>
            <td>
                <asp:Button ID="btnExportExcel" runat="server" Text="匯出Excel" CssClass="Button" OnClick="btnExportExcel_Click" meta:resourcekey="btnExportExcelResource1" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gridRegister" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    OnRowDataBound="gridRegister_RowDataBound" OnPageIndexChanging="gridRegister_PageIndexChanging" PageSize="20" meta:resourcekey="gridRegisterResource1">
                    <Columns>
                        <asp:BoundField HeaderText="工號" DataField="empid" meta:resourcekey="BoundFieldResource1">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="中文姓名" DataField="empnamech" meta:resourcekey="BoundFieldResource2">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="英文姓名" DataField="empnameen" meta:resourcekey="BoundFieldResource3">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="報名日期時間" DataField="registerdate" meta:resourcekey="BoundFieldResource4">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="最後修改人員" DataField="modifiedby" meta:resourcekey="BoundFieldResource5">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="最後修改日期" DataField="modifieddate" meta:resourcekey="BoundFieldResource6">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="報名資料" meta:resourcekey="TemplateFieldResource1">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="btnEdit" runat="server" Text="編輯" CssClass="Button_Gridview" CommandArgument='<%# Eval("registerid") %>' OnClick="btnEdit_Click" meta:resourcekey="btnEditResource1" />
                                <asp:Button ID="btnView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("registerid") %>' OnClick="btnView_Click" meta:resourcekey="btnViewResource1" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField meta:resourcekey="TemplateFieldResource2">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("registerid") %>' OnClick="btnDelete_Click" meta:resourcekey="btnDeleteResource1" />
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
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none" meta:resourcekey="ContentPanel6Resource1">
            <asp:Label ID="lblDeleteWarning" runat="server" Text="確定刪除該筆資料？" meta:resourcekey="lblDeleteWarningResource1"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none" meta:resourcekey="ContentPanel3Resource1">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。" meta:resourcekey="lblFailedResource1"></asp:Label>
            <asp:Label ID="lblErrMsg" runat="server" meta:resourcekey="lblErrMsgResource1"></asp:Label><br />
        </asp:Panel>
    </div>

    <asp:Label ID="lblLimit" runat="server" Text="Unlimited" Visible="False" meta:resourcekey="lblLimitResource1"></asp:Label>
    <asp:Button ID="btnReloadGridView" runat="server" OnClick="btnReloadGridView_Click" Style="display: none;" meta:resourcekey="btnReloadGridViewResource1" />

    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblWarningText" runat="server" Text="警告" meta:resourcekey="lblWarningTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblDeleteErrMsg" runat="server" Text="報名資料刪除發生錯誤" meta:resourcekey="lblDeleteErrMsgResource1" Style="display: none;"></asp:Label>
</asp:Content>

