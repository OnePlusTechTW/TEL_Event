<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_RegisterModel5_View.aspx.cs" Inherits="Event_Event_RegisterModel5_View" Culture="auto" meta:resourcekey="PageResource2" UICulture="auto" %>

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
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">
                        <asp:Label ID="lblFileUpload1" runat="server" Text="上傳附件1" meta:resourcekey="lblFileUpload1Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:HyperLink ID="hlnkFileUpload1" runat="server" Style="color: blue;" Visible="False" meta:resourcekey="hlnkFileUpload1Resource1">[hlnkFileUpload1]</asp:HyperLink>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">
                        <asp:Label ID="lblDescription1" runat="server" Text="上傳附件1之說明" meta:resourcekey="lblDescription1Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtDescription1" runat="server" CssClass="QueryField" TextMode="MultiLine" Width="100%" Height="60px" Enabled="False" meta:resourcekey="txtDescription1Resource1"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">
                        <asp:Label ID="lblFileUpload2" runat="server" Text="上傳附件2" meta:resourcekey="lblFileUpload2Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:HyperLink ID="hlnkFileUpload2" runat="server" Style="color: blue;" Visible="False" meta:resourcekey="hlnkFileUpload2Resource1">[hlnkFileUpload2]</asp:HyperLink>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">
                        <asp:Label ID="lblDescription2" runat="server" Text="上傳附件2之說明" meta:resourcekey="lblDescription2Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtDescription2" runat="server" CssClass="QueryField" TextMode="MultiLine" Width="100%" Height="60px" Enabled="False" meta:resourcekey="txtDescription2Resource1"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">

                        <asp:Label ID="lblFileUpload3" runat="server" Text="上傳附件3" meta:resourcekey="lblFileUpload3Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:HyperLink ID="hlnkFileUpload3" runat="server" Style="color: blue;" Visible="False" meta:resourcekey="hlnkFileUpload3Resource1">[hlnkFileUpload3]</asp:HyperLink>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">
                        <asp:Label ID="lblDescription3" runat="server" Text="上傳附件3之說明" meta:resourcekey="lblDescription3Resource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtDescription3" runat="server" CssClass="QueryField" TextMode="MultiLine" Width="100%" Height="60px" Enabled="False" meta:resourcekey="txtDescription3Resource1"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">
                        <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋" meta:resourcekey="lblCommentResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="100px" CssClass="QueryField" Enabled="False" meta:resourcekey="txtCommentResource1"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
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
