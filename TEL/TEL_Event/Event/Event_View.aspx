<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_View.aspx.cs" Inherits="Event_Event_View" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

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
            <table>
                <tr>
                    <td>
                        <asp:Image runat="server" ImageUrl="~/Master/images/icon3.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
                    </td>
                    <td style="width: 5px"></td>
                    <td style="border-bottom: 1.5px solid #19b1e5;">
                        <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="檢視活動" meta:resourcekey="lblPageNameResource2"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 10px">
                    <td></td>
                </tr>
            </table>
            <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
        </div>
    </form>
</body>
</html>
