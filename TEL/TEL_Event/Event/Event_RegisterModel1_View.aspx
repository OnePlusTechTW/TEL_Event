<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_RegisterModel1_View.aspx.cs" Inherits="Event_Event_RegisterModel1_View" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

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
                    <asp:Label ID="lblNoRegisterInfo" runat="server" Text="查無報名資料。" meta:resourcekey="lblNoRegisterInfoResource1"></asp:Label>
                </asp:Panel>
            </div>

            <asp:Button ID="btnGoBackPage" runat="server" Text="" OnClick="btnGoBackPage_Click" Style="display: none;" />
            <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
        </div>
    </form>
</body>
</html>
