<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_RegisterModel3_View.aspx.cs" Inherits="Event_Event_RegisterModel3_View" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        
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
            <table style="width: 1150px;">
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
                        <asp:Label ID="lblHealthGroup" runat="server" Text="健檢組別"></asp:Label>
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
                        <asp:TextBox ID="txtHealthGroup" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblIdentity" runat="server" Text="受診者身份別"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineename" runat="server" Text="受診者中文姓名"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineeidno" runat="server" Text="受診者身分證字號"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineebirthday" runat="server" Text="受診者出生年月日"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExamineemobile" runat="server" Text="受診者手機"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtIdentity" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineename" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineeidno" runat="server" CssClass="QueryField" MaxLength="10" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineebirthday" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtExamineemobile" MaxLength="10" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblHosipital" runat="server" Text="健檢醫院"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblArea" runat="server" Text="地區"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSolution" runat="server" Text="費用&方案"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblGender" runat="server" Text="受診者性別"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblExpectdate" runat="server" Text="期望受檢日"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSeconddate" runat="server" Text="備用受檢日"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtHosipital" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtArea" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtSolution" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtGender" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtExpectdate" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtSeconddate" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblSecondsolution1" runat="server" Text="健檢次方案1"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSecondsolution2" runat="server" Text="健檢次方案2"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSecondsolution3" runat="server" Text="健檢次方案2"></asp:Label>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtSecondsolution1" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSecondsolution2" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSecondsolution3" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblAddress" runat="server" Text="健檢包寄送地點"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblMeal" runat="server" Text="餐點樣式"></asp:Label>
                    </td>
                     <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>

                    </td>
                    <td style="vertical-align: top;">
                        <asp:TextBox ID="txtMeal" runat="server" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td colspan="4">
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblOptional" runat="server" Text="自費加選項目"></asp:Label>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtOptional" runat="server" TextMode="MultiLine" Width="100%" Height="135px" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="6">
                        <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋" Enabled="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="135px" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                
            </table>

            <div id="dialogMsg" title="Dialog Title">
                <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </div>
            

            <div id="dialogNoRegisterInfo" title="Dialog Title">
                <asp:Panel ID="ContentPanel4" runat="server" Style="display: none">
                    <asp:Label ID="lblNoRegisterInfo" runat="server" Text="查無報名資料。"></asp:Label>
                </asp:Panel>
            </div>

            <asp:Label ID="lblUnselect" runat="server" Text="- 未指定 -" Visible="false"></asp:Label>
            <asp:Label ID="lblLimitReached" runat="server" Text="此方案報名人數已達上限，請重新選擇其他方案" Visible="false"></asp:Label>
            <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
            <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
            <asp:Button ID="btnGoBackPage" runat="server" Text="Button" OnClick="btnGoBackPage_Click" style="display:none;" />

        </div>
    </form>
</body>
</html>
