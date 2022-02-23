<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_RegisterModel2_View.aspx.cs" Inherits="Event_Event_RegisterModel2_View" %>
<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script>
        function ShowNoRegisterInfo() {
            $(function () {
                $("#dialogNoRegisterInfo").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    width: "700px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", },
                    buttons: {
                        Close: function () {
                            <%= btnGoBackPage.ClientID%>.click();
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel4").style.display = "block";
                    }
                });
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
            <table style="width: 950px;" >
                <tr>
                    <td>
                        <asp:Label ID="lblEmpid" runat="server" Text="工號"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCName" runat="server" Text="中文姓名"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEName" runat="server" Text="英文姓名"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDepartment" runat="server" Text="部門"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblStation" runat="server" Text="勤務地"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtEmpid" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCName" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEName" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDepartment" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStation" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblID" runat="server" Text="身份證字號"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBDay" runat="server" Text="出生年月日"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGender" runat="server" Text="性別"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtID" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBDay" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGender" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="6">
                        <asp:Label ID="lblAttendContent" runat="server" Text="欲參加的內容" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtAttendContent" runat="server"  Enabled="false" CssClass="QueryField" Width="100%"></asp:TextBox>

                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                 <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblPhone" runat="server" Text="手機"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTransportation" runat="server" Text="交通車"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMeal" runat="server" Text="餐點內容"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="QueryField" MaxLength="10"  Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransportation" runat="server" CssClass="QueryField"  Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMeal" runat="server" CssClass="QueryField"  Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr >
                    <td colspan="5" style="padding-top:15px" >
                        <asp:GridView ID="gridRegisterModel2family" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True" Width="100%"
                            EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                            PageSize="20" OnPageIndexChanging="gridRegisterModel2family_PageIndexChanging" OnRowDataBound="gridRegisterModel2family_RowDataBound" >
                            <Columns>
                                <asp:BoundField DataField="name" HeaderText="家屬姓名" />
                                <asp:BoundField DataField="idno" HeaderText="家屬身分證字號" />
                                <asp:BoundField DataField="birthday" HeaderText="家屬生日年月日" />
                                <asp:BoundField DataField="gender" HeaderText="家屬性別" />
                                <asp:BoundField DataField="meal" HeaderText="餐點內容" />
                            </Columns>
                            <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                            <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                        </asp:GridView>
                    </td>
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="6">
                        <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="135px" CssClass="QueryField"  Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>

            <div id="dialogNoRegisterInfo" title="Dialog Title">
                <asp:Panel ID="ContentPanel4" runat="server" Style="display: none">
                    <asp:Label ID="lblNoRegisterInfo" runat="server" Text="查無報名資料。" ></asp:Label>
                </asp:Panel>
            </div>

            <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="false"></asp:Label>
            <asp:Label ID="lblUnselect" runat="server" Text="- 未指定 -" Visible="false"></asp:Label>
            <asp:Label ID="lblLimitReached" runat="server" Text="此方案報名人數已達上限，請重新選擇其他方案" Visible="false"></asp:Label>
            <asp:Label ID="lblSendMailFailed" runat="server" Text="但報名成功通知mail寄送失敗。" Visible="false"></asp:Label>
            <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
            <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
            <asp:Button ID="btnGoBackPage" runat="server" Text="Button" OnClick="btnGoBackPage_Click" style="display:none;" />
        </div>
    </form>
</body>
</html>
