<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_Model3Options.aspx.cs" Inherits="Event_Event_Model3Options" StylesheetTheme="Event" Culture="auto" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //上傳健檢群組開窗
        function ShowDialogFileUpload(event, id) {
            $(function () {
                var dialog = $("#dialogFileUpload").dialog({
                    title: "",
                    modal: true,
                    buttons: [
                        {
                            text: "關閉",
                            click: function () {
                                $(this).dialog("close");
                            }
                        }
                    ],
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

        function ShowDialogRequired(fieldName, errField) {
            $('#<%= lblFiledName.ClientID %>').text(errField);

            $(function () {
                $("#dialogRequired").dialog({
                    title: $('#<%=hfWarning.ClientID%>')[0].value,
                    modal: true,
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

        };

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
            PageMethods.DeleteRegisterOption5(id, Success, Failure);
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnImportHealthSolutions" runat="server" CssClass="Button" Text="匯入健檢方案" OnClick="btnImportHealthSolutions_Click" Width="120px"  />
            </td>
            <td style="padding-left: 10px;">
                <asp:HyperLink ID="hlnkFileAttachment" runat="server" Text="Excel範例" Style="color: blue;" NavigateUrl="~/Sample/Import_HealthSolutions.xlsx" />
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption4" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnRowDataBound="gridRegisterOption4_RowDataBound" OnPageIndexChanging="gridRegisterOption4_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="醫院" DataField="hosipital">
                            <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="地區" DataField="area">
                            <HeaderStyle Width="150px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="性別" DataField="gender">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="費用&方案" DataField="description">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="次方案1" DataField="secondoption1">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="次方案2" DataField="secondoption2">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="次方案3" DataField="secondoption3">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="日期" DataField="avaliabledate">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="人數上限" DataField="limit">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                    <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table style="margin-top:35px">
        <tr>
            <td>
                <asp:Label ID="lblSendArea" runat="server" Text="健檢包寄送地點"></asp:Label>
            </td>
            
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtSendArea" runat="server" Width="200px" CssClass="QueryField"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="Button" OnClick="btnAdd_Click"/>
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption5" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnPageIndexChanging="gridRegisterOption5_PageIndexChanging" OnRowDataBound="gridRegisterOption5_RowDataBound" >
                    <Columns>
                        <asp:BoundField HeaderText="健檢包寄送地點" DataField="description">
                            <HeaderStyle Width="120px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_DeleteManager" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>'
                                    OnClientClick='<%# "ShowDialogDelete(\""+ Eval("id") + "\");return false;" %>' />
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
    <table style="padding-top: 35px">
        <tr>
            <td>
                <asp:Button ID="btnfinish" runat="server" Text="完成" OnClick="btnfinish_Click" CssClass="Button" />
            </td>
        </tr>
    </table>


    <div id="dialogFileUpload" title="Dialog Title">
        <asp:Panel ID="ContentPanel7" runat="server" Style="display: none">
            <div>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div style="margin-top: 20px;">
                <asp:Button ID="btnImport" runat="server" Text="匯入" OnClick="btnImport_Click" />
            </div>
            <div style="margin-top: 5px;">
                <asp:TextBox ID="tbImportMsg" runat="server" TextMode="MultiLine" Height="250px" Width="412px" placeholder="匯入資訊..." ReadOnly="true" ></asp:TextBox>
            </div>
        </asp:Panel>
    </div>

    <div id="dialogRequired" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <asp:Label ID="lblFiledName" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblRequired" runat="server" Text="為必填欄位。"></asp:Label>
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

    <asp:Label ID="lblImportSuccess" runat="server" Text="匯入成功。" Visible="false"></asp:Label>
    <asp:Label ID="lblImportFailed" runat="server" Text="匯入失敗，請重新匯入。" Visible="false"></asp:Label>
    <asp:Label ID="lblImportFailedMsg" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label>
    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
    <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
    <asp:Button ID="btnReloadGridView" runat="server" Text="Button" OnClick="btnReloadGridView_Click" style="display:none;"/>
</asp:Content>

