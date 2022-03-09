<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_EventDescription.ascx.cs" Inherits="Event_UserControl_UC_EventDescription" %>

<table style="width: 800px; border-spacing: 0px">
    <tr>
        <td runat="server" id="category" style="text-align: center; width: 150px">
            <asp:Label ID="lblCategoryName" runat="server" Text="" Font-Size="18px" Font-Bold="true" ForeColor="Black"></asp:Label>
        </td>
        <td style="text-align: center">
            <asp:Label ID="lblEventName" runat="server" Text="Label" Font-Size="25px" Font-Bold="true" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Image ID="Image1" runat="server" Width="600px" />
        </td>
    </tr>
    <tr>
        <td style="width: 47%">
            <asp:Label ID="lblEventDate" runat="server" Text="活動日期" Font-Bold="true" ForeColor="Black"></asp:Label>
            <asp:Label ID="lblEventDateValue" runat="server" Text="" Font-Bold="true" ForeColor="Black"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblSignupDate" runat="server" Text="報名日期" Font-Bold="true" ForeColor="Black"></asp:Label>
            <asp:Label ID="lblSignupDateValue" runat="server" Text="" Font-Bold="true" ForeColor="Black"></asp:Label>
        </td>
    </tr>
    <tr>
        <td runat="server" id="content" colspan="2">
            <asp:Label ID="Label1" runat="server" Text="Label" Font-Size="25px" Font-Bold="true" ForeColor="Black"></asp:Label>

        </td>
    </tr>
</table>
