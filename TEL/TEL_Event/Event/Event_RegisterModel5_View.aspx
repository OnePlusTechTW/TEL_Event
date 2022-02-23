<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_RegisterModel5_View.aspx.cs" Inherits="Event_Event_RegisterModel5_View" %>

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
            <table>
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
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtEmpid" runat="server" ReadOnly="true" CssClass="QueryField" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCName" runat="server" ReadOnly="true" CssClass="QueryField" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEName" runat="server" ReadOnly="true" CssClass="QueryField" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td>
                        <asp:Label ID="lblDepartment" runat="server" Text="部門"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblStation" runat="server" Text="勤務地"></asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDepartment" runat="server" ReadOnly="true" CssClass="QueryField" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStation" runat="server" ReadOnly="true" CssClass="QueryField" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblFileUpload1" runat="server" Text="上傳附件1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:HyperLink ID="hlnkFileUpload1" runat="server" Style="color: blue;" Visible="false" />
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblDescription1" runat="server" Text="上傳附件1之說明"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtDescription1" runat="server" CssClass="QueryField" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">

                        <asp:Label ID="lblFileUpload2" runat="server" Text="上傳附件2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:HyperLink ID="hlnkFileUpload2" runat="server" Style="color: blue;" Visible="false" />
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblDescription2" runat="server" Text="上傳附件2之說明"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtDescription2" runat="server" CssClass="QueryField" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">

                        <asp:Label ID="lblFileUpload3" runat="server" Text="上傳附件3"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:HyperLink ID="hlnkFileUpload3" runat="server" Style="color: blue;" Visible="false" />
                    </td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblDescription3" runat="server" Text="上傳附件3之說明"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtDescription3" runat="server" CssClass="QueryField" Width="100%" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr  class="FormTRStyle">
                    <td colspan="3">
                        <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="135px" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>

            <div id="dialogNoRegisterInfo" title="Dialog Title">
                <asp:Panel ID="ContentPanel4" runat="server" Style="display: none">
                    <asp:Label ID="lblNoRegisterInfo" runat="server" Text="查無報名資料。"></asp:Label>
                </asp:Panel>
            </div>
            <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
            <asp:Button ID="btnGoBackPage" runat="server" Text="Button" OnClick="btnGoBackPage_Click" Style="display: none;" />
        </div>
    </form>
</body>
</html>
