<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_Model6Options.aspx.cs" Inherits="Event_Event_Model6Options" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

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

        //通用錯誤訊息 通知開窗
        function ShowDialogMsg(msg) {
            $('#<%= lblDialogMsg.ClientID %>').text(msg);

            $(function () {
                $("#dialogMsg").dialog({
                    title: document.getElementById('<%=lblWarningText.ClientID%>').innerText,
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel8").style.display = "block";
                    }
                });
            });

        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Image ID="lblPageImage" runat="server" ImageUrl="~/Master/images/Link_CreateEvents.png" Height="40px" meta:resourcekey="lblPageImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="建立活動" meta:resourcekey="lblPageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Button ID="btnImportChangeArea" runat="server" CssClass="Button" Text="匯入地點與日期時間" Width="150px" OnClick="btnImportChangeArea_Click" meta:resourcekey="btnImportChangeAreaResource1" />
            </td>
            <td style="padding-left: 10px;">
                <asp:HyperLink ID="hlnkFileAttachment" runat="server" Text="Excel範例" Style="color: blue;" NavigateUrl="~/Sample/Import_ComputerChange.xlsx" meta:resourcekey="hlnkFileAttachmentResource1" />
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption6" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnRowDataBound="gridRegisterOption6_RowDataBound" OnPageIndexChanging="gridRegisterOption6_PageIndexChanging" meta:resourcekey="gridRegisterOption6Resource1">
                    <Columns>
                        <asp:BoundField HeaderText="地點" DataField="area" meta:resourcekey="BoundFieldResource1">
                            <HeaderStyle Width="150px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="日期時間" DataField="avaliabledate" meta:resourcekey="BoundFieldResource2">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="人數上限" DataField="limit" meta:resourcekey="BoundFieldResource3">
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
                <asp:Button ID="btnfinish" runat="server" Text="完成" OnClick="btnfinish_Click" CssClass="Button" meta:resourcekey="btnfinishResource1" />
            </td>
        </tr>
    </table>

    <div id="dialogFileUpload" title="Dialog Title">
        <asp:Panel ID="ContentPanel7" runat="server" Style="display: none" meta:resourcekey="ContentPanel7Resource1">
            <div>
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FileUpload" meta:resourcekey="FileUpload1Resource1" />
            </div>
            <div style="margin-top: 20px;">
                <asp:Button ID="btnImport" runat="server" Text="匯入" CssClass="Button" OnClick="btnImport_Click" meta:resourcekey="btnImportResource1" />
            </div>
            <div style="margin-top: 5px;">
                <asp:TextBox ID="tbImportMsg" runat="server" TextMode="MultiLine" Height="250px" Width="412px" placeholder="匯入資訊..." ReadOnly="True" meta:resourcekey="tbImportMsgResource1"></asp:TextBox>
            </div>
        </asp:Panel>
    </div>

    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel8" runat="server" Style="display: none" meta:resourcekey="ContentPanel8Resource1">
            <asp:Label ID="lblDialogMsg" runat="server" meta:resourcekey="lblDialogMsgResource1"></asp:Label>
        </asp:Panel>
    </div>

    <asp:Label ID="lblImportSuccess" runat="server" Text="匯入成功。" Visible="False" meta:resourcekey="lblImportSuccessResource1"></asp:Label>
    <asp:Label ID="lblImportFailed" runat="server" Text="匯入失敗，請重新匯入。" Visible="False" meta:resourcekey="lblImportFailedResource1"></asp:Label>
    <asp:Label ID="lblImportFailedMsg" runat="server" Text="錯誤訊息：" Visible="False" meta:resourcekey="lblImportFailedMsgResource1"></asp:Label>
    <asp:Label ID="lblDuplicate" runat="server" Text="資料列 {0} 為重複資料。" Visible="False" meta:resourcekey="lblDuplicateResource1"></asp:Label>
    <asp:Label ID="lblLoseData" runat="server" Text="資料列 {0} 的必填欄位必須輸入。" Visible="False" meta:resourcekey="lblLoseDataResource1"></asp:Label>

    <asp:Label ID="lblOptionsNoAdd" runat="server" Text="請確認自訂選項是否新增。" Style="display: none;" meta:resourcekey="lblOptionsNoAddResource1"></asp:Label>
    <asp:Label ID="lblWarningText" runat="server" Text="警告" meta:resourcekey="lblWarningTextResource1" Style="display: none;"></asp:Label>

</asp:Content>

