<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Register_PreCreate.aspx.cs" Inherits="Event_Register_PreCreate" StylesheetTheme="Event" Culture="auto" UICulture="auto"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        //通用錯誤訊息 通知開窗
        function ShowDialogMsg(msg) {
            $('#<%= lblDialogMsg.ClientID %>').text(msg);

            $(function () {
                $("#dialogMsg").dialog({
                    title: $('#<%=hfWarning.ClientID%>')[0].value,
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel8").style.display = "block";
                    }
                });
            });

        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon3.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL_PageName" runat="server" CssClass="PageTitle" Text="馬上報名"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px"></tr>
    </table>
    <table>
        <tr style="height: 15px">
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEmpID" runat="server" Text="工號"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtEmpID" runat="server" Width="120px" CssClass="QueryField"></asp:TextBox></td>
            <td>
            <td>
                <asp:Button ID="btnNextStep" runat="server" Text="下一步" CssClass="Button" OnClick="btnNextStep_Click" />
            </td>
        </tr>
    </table>

    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel8" runat="server" Style="display: none">
            <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>

    <asp:Label ID="lblNoEmp" runat="server" Text="查無此員工" Visible="false"></asp:Label>
    <asp:Label ID="lblNoDuplicated" runat="server" Text="此員工已報名，無法重複報名。" Visible="false"></asp:Label>

    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
</asp:Content>

