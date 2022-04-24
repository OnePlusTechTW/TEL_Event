<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_RegisterModel2_View.aspx.cs" Inherits="Event_Event_RegisterModel2_View" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        function ShowNoRegisterInfo() {
            $(function () {
                $("#dialogNoRegisterInfo").dialog({
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
            <table>
                <tr>
                    <td>
                        <asp:Image runat="server" ImageUrl="~/Master/images/icon3.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
                    </td>
                    <td style="width: 5px"></td>
                    <td style="border-bottom: 1.5px solid #19b1e5;">
                        <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="檢視報名" meta:resourcekey="lblPageNameResource1"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 10px">
                    <td></td>
                </tr>
            </table>
            <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
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
                    <td></td>
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
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblID" runat="server" Text="身份證字號" meta:resourcekey="lblIDResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBDay" runat="server" Text="出生年月日" meta:resourcekey="lblBDayResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGender" runat="server" Text="性別" meta:resourcekey="lblGenderResource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtID" runat="server" Enabled="False" CssClass="QueryField" meta:resourcekey="txtIDResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBDay" runat="server" Enabled="False" CssClass="QueryField" meta:resourcekey="txtBDayResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGender" runat="server" Enabled="False" CssClass="QueryField" meta:resourcekey="txtGenderResource1"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtEmail" runat="server" Enabled="False" CssClass="QueryField" Width="316px" meta:resourcekey="txtEmailResource1"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="6">
                        <asp:Label ID="lblAttendContent" runat="server" Text="欲參加的內容" meta:resourcekey="lblAttendContentResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtAttendContent" runat="server" Enabled="False" CssClass="QueryField" Width="100%" meta:resourcekey="txtAttendContentResource1"></asp:TextBox>

                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblPhone" runat="server" Text="手機" meta:resourcekey="lblPhoneResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTransportation" runat="server" Text="交通車" meta:resourcekey="lblTransportationResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMeal" runat="server" Text="餐點內容" meta:resourcekey="lblMealResource1"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="QueryField" MaxLength="10" Enabled="False" meta:resourcekey="txtPhoneResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTransportation" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtTransportationResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMeal" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtMealResource1"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="5" style="padding-top: 15px">
                        <asp:GridView ID="gridRegisterModel2family" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True" Width="100%"
                            EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                            PageSize="20" OnPageIndexChanging="gridRegisterModel2family_PageIndexChanging" OnRowDataBound="gridRegisterModel2family_RowDataBound" meta:resourcekey="gridRegisterModel2familyResource1">
                            <Columns>
                                <asp:BoundField DataField="name" HeaderText="家屬姓名" meta:resourcekey="BoundFieldResource1" />
                                <asp:BoundField DataField="idno" HeaderText="家屬身分證字號" meta:resourcekey="BoundFieldResource2" />
                                <asp:BoundField DataField="birthday" HeaderText="家屬生日年月日" meta:resourcekey="BoundFieldResource3" />
                                <asp:BoundField DataField="gender" HeaderText="家屬性別" meta:resourcekey="BoundFieldResource4" />
                                <asp:BoundField DataField="meal" HeaderText="餐點內容" meta:resourcekey="BoundFieldResource5" />
                            </Columns>
                            <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                            <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                        </asp:GridView>
                    </td>
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="6">
                        <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋" meta:resourcekey="lblCommentResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="100px" CssClass="QueryField" Enabled="False" meta:resourcekey="txtCommentResource1"></asp:TextBox>
                    </td>
                    <td colspan="3"></td>
                </tr>
            </table>

            <div id="dialogNoRegisterInfo" title="Dialog Title">
                <asp:Panel ID="ContentPanel4" runat="server" Style="display: none" meta:resourcekey="ContentPanel4Resource1">
                    <asp:Label ID="lblNoRegisterInfo" runat="server" Text="查無報名資料。" meta:resourcekey="lblNoRegisterInfoResource1"></asp:Label>
                </asp:Panel>
            </div>

            <asp:Button ID="btnGoBackPage" runat="server" Text="" OnClick="btnGoBackPage_Click" Style="display: none;" meta:resourcekey="btnGoBackPageResource1" />

            <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
        </div>
    </form>
</body>
</html>
