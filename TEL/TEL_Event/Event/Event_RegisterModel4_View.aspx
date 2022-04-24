<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_RegisterModel4_View.aspx.cs" Inherits="Event_Event_RegisterModel4_View" culture="auto" meta:resourcekey="PageResource2" uiculture="auto" %>

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
                    <td>
                        <asp:Label ID="lblHealthGroup" runat="server" Text="健檢組別" meta:resourcekey="lblHealthGroupResource1"></asp:Label>
                    </td>
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
                    <td>
                        <asp:TextBox ID="txtHealthGroup" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtHealthGroupResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblIdentity" runat="server" Text="受診者身份別" meta:resourcekey="lblIdentityResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineename" runat="server" Text="受診者姓名" meta:resourcekey="lblExamineenameResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineename2" runat="server" Text="受診者拼音" meta:resourcekey="lblExamineename2Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineeidno" runat="server" Text="受診者居留證字號" meta:resourcekey="lblExamineeidnoResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineebirthday" runat="server" Text="受診者出生年月日" meta:resourcekey="lblExamineebirthdayResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineemobile" runat="server" Text="受診者手機" meta:resourcekey="lblExamineemobileResource1"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtIdentity" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtIdentityResource1"></asp:TextBox>

                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineename" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtExamineenameResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineename2" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtExamineename2Resource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineeidno" runat="server" CssClass="QueryField" MaxLength="10" Enabled="False" meta:resourcekey="txtExamineeidnoResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineebirthday" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtExamineebirthdayResource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineemobile" MaxLength="10" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtExamineemobileResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblHosipital" runat="server" Text="健檢醫院" meta:resourcekey="lblHosipitalResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblArea" runat="server" Text="地區" meta:resourcekey="lblAreaResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSolution" runat="server" Text="費用&方案" meta:resourcekey="lblSolutionResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGender" runat="server" Text="受診者性別" meta:resourcekey="lblGenderResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExpectdate" runat="server" Text="期望受檢日" meta:resourcekey="lblExpectdateResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSeconddate" runat="server" Text="備用受檢日" meta:resourcekey="lblSeconddateResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtHosipital" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtHosipitalResource1"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtArea" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtAreaResource1"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtSolution" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtSolutionResource1"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtGender" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtGenderResource1"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtExpectdate" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtExpectdateResource1"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtSeconddate" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtSeconddateResource1"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblSecondsolution1" runat="server" Text="健檢次方案1" meta:resourcekey="lblSecondsolution1Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSecondsolution2" runat="server" Text="健檢次方案2" meta:resourcekey="lblSecondsolution2Resource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSecondsolution3" runat="server" Text="健檢次方案3" meta:resourcekey="lblSecondsolution3Resource1"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSecondsolution1" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtSecondsolution1Resource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSecondsolution2" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtSecondsolution2Resource1"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSecondsolution3" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtSecondsolution3Resource1"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblAddress" runat="server" Text="健檢包寄送地點" meta:resourcekey="lblAddressResource1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMeal" runat="server" Text="餐點樣式" meta:resourcekey="lblMealResource1"></asp:Label>
                    </td>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtAddressResource1"></asp:TextBox>

                    </td>
                    <td style="vertical-align: top;">
                        <asp:TextBox ID="txtMeal" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtMealResource1"></asp:TextBox>
                    </td>
                    <td colspan="4"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblOptional" runat="server" Text="自費加選項目" meta:resourcekey="lblOptionalResource1"></asp:Label>
                    </td>
                    <td colspan="2"></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtOptional" runat="server" TextMode="MultiLine" Width="100%" Height="100px" CssClass="QueryField" Enabled="False" meta:resourcekey="txtOptionalResource1"></asp:TextBox>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblNeedhotel" runat="server" Text="是否預約飯店" meta:resourcekey="lblNeedhotelResource1"></asp:Label>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblCheckininfo" runat="server" Text="住宿人名單(例：野原廣治 35歲)" meta:resourcekey="lblCheckininfoResource1"></asp:Label>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:TextBox ID="txtNeedhotel" runat="server" CssClass="QueryField" Enabled="False" meta:resourcekey="txtNeedhotelResource1"></asp:TextBox>
                    </td>
                    <td colspan="2" style="vertical-align: top">
                        <asp:TextBox ID="txtCheckininfo" runat="server" TextMode="MultiLine" CssClass="QueryField" Width="100%" Height="50px" Enabled="False" meta:resourcekey="txtCheckininfoResource1"></asp:TextBox>
                    </td>
                    <td colspan="3"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="6">
                        <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋" Enabled="False" meta:resourcekey="lblCommentResource1"></asp:Label>
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
                    <asp:Label ID="lblNoRegisterInfo" runat="server" Text="查無報名資料。" meta:resourceKey="lblNoRegisterInfoResource1"></asp:Label>
                </asp:Panel>
            </div>

            <asp:Button ID="btnGoBackPage" runat="server" Text="" OnClick="btnGoBackPage_Click" Style="display: none;" meta:resourcekey="btnGoBackPageResource1" />

            <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
        </div>
    </form>
</body>
</html>
