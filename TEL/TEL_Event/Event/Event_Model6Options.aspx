<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_Model6Options.aspx.cs" Inherits="Event_Event_Model6Options" StylesheetTheme="Event" Culture="auto" UICulture="auto"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //上傳健檢群組開窗
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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Button ID="btnImportChangeArea" runat="server" CssClass="Button" Text="匯入地點與日期時間" Width="150px" OnClick="btnImportChangeArea_Click" />
            </td>
            <td style="padding-left: 10px;">
                <asp:HyperLink ID="hlnkFileAttachment" runat="server" Text="Excel範例" Style="color: blue;" NavigateUrl="~/Sample/Import_ComputerChange.xlsx" />
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption6" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnRowDataBound="gridRegisterOption6_RowDataBound" OnPageIndexChanging="gridRegisterOption6_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="地點" DataField="area">
                            <HeaderStyle Width="150px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="日期時間" DataField="avaliabledate">
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
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FileUpload" />
            </div>
            <div style="margin-top: 20px;">
                <asp:Button ID="btnImport" runat="server" Text="匯入" CssClass="Button" OnClick="btnImport_Click" />
            </div>
            <div style="margin-top: 5px;">
                <asp:TextBox ID="tbImportMsg" runat="server" TextMode="MultiLine" Height="250px" Width="412px" placeholder="匯入資訊..." ReadOnly="true"></asp:TextBox>
            </div>
        </asp:Panel>
    </div>

    <asp:Label ID="lblImportSuccess" runat="server" Text="匯入成功。" Visible="false"></asp:Label>
    <asp:Label ID="lblImportFailed" runat="server" Text="匯入失敗，請重新匯入。" Visible="false"></asp:Label>
    <asp:Label ID="lblImportFailedMsg" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label>
</asp:Content>

