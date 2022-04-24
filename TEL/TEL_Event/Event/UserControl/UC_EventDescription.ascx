<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_EventDescription.ascx.cs" Inherits="Event_UserControl_UC_EventDescription" %>

<style>
    div.scrollable {
        width: 850px;
        height: 250px;
        margin: 0;
        border-radius: 10px;
        background-color: #E7E6E6;
        padding: 11px;
        word-break: break-word;
        overflow-x: auto;
    }
</style>

<table style="width: 850px; border-spacing: 0px; border-bottom: 1px solid silver; margin-bottom: 10px; padding-bottom: 10px;">
    <tr>
        <td>
            <table style="border-spacing: 0px">
                <tr>
                    <td runat="server" id="category" style="max-width: 350px; padding-right: 13px; word-break: break-word;">
                        <asp:Label ID="lblCategoryName" runat="server" Font-Size="25px" Font-Bold="True" meta:resourcekey="lblCategoryNameResource1"></asp:Label>
                    </td>
                    <td style="border-left: 1px solid black; padding-left: 15px; min-width: 500px; max-width: 800px;">
                        <asp:Label ID="lblEventName" runat="server" Text="" Font-Size="25px" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblEventNameResource1"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table style="border-top: 1px solid silver; width: 850px; border-spacing: 0px; margin-top: 13px; padding-top: 8px;">
                <tr>
                    <td style="vertical-align: top;" colspan="2">
                        <asp:Image ID="Image1" runat="server" Width="850px" meta:resourcekey="Image1Resource1" />
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 5px;">
                        <asp:Label ID="lblEventDate" runat="server" Text="活動日期" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblEventDateResource1"></asp:Label>
                        <asp:Label ID="lblEventDateValue" runat="server" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblEventDateValueResource1"></asp:Label>
                    </td>
                    <td style="padding-bottom: 10px; text-align: right">
                        <asp:Label ID="lblSignupDate" runat="server" Text="報名日期" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblSignupDateResource1"></asp:Label>
                        <asp:Label ID="lblSignupDateValue" runat="server" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblSignupDateValueResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="padding-bottom: 5px;" colspan="2">
                        <asp:Label ID="lblDescription" runat="server" Text="活動說明" meta:resourcekey="lblDescriptionResource1"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="2">
                        <div runat="server" id="content" class="scrollable" colspan="2" >
                        </div>
                    </td>
                </tr>
            </table>
        </td>

    </tr>

</table>
