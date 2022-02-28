<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_RegisterModel1_View.aspx.cs" Inherits="Event_Event_RegisterModel1_View" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblStation" runat="server" Text="勤務地"></asp:Label>
                    </td>
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
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtStation" runat="server" Enabled="false" CssClass="QueryField"></asp:TextBox>
                    </td>
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
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">
                        <asp:Label ID="lblAttendContent" runat="server" Text="欲參加的內容"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtAttendContent" runat="server" Enabled="false" CssClass="QueryField" Width="100%"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr class="FormTRStyle">
                    <td colspan="5">
                        <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="100px" CssClass="QueryField" Enabled="false"></asp:TextBox>
                    </td>
                    <td colspan="2"></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
