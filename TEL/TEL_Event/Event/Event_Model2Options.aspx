<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_Model2Options.aspx.cs" Inherits="Event_Event_Model2Options"  StylesheetTheme="Event" Culture="auto" UICulture="auto"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function ShowDialogRequired() {
            $(function () {
                $("#dialogRequired").dialog({
                    title: $('#<%=hfWarning.ClientID%>')[0].value,
                    modal: true,
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

        };

        //失敗通知 訊息開窗
        function ShowDialogFailed(ErrMsg) {
            $(function () {
                $("#dialogFailed").dialog({
                    title: $('#<%=hfWarning.ClientID%>')[0].value,
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel3").style.display = "block";
                        $('#<%= lblErrMsg.ClientID %>').text(ErrMsg);

                    }
                });
            });

        };

        //是否刪除 訊息開窗
        function ShowDialogDelete(event, id) {
            $(function () {
                $("#dialogDelete").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    buttons: [
                        {
                            text: "確定",
                            click: function () {
                                onDelete(event, id);
                                $(this).dialog("close");
                            }
                        },
                        {
                            text: "取消",
                            click: function () {
                                $(this).dialog("close");
                            }
                        }
                    ],
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel6").style.display = "block";
                    }
                });
            });

        };

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

        //刪除資料events
        function onDelete(event, id) {
            switch (event) {
                case 'Option1':
                    PageMethods.DeleteRegisterOption1(id, Success, Failure);
                    break;
                case 'Option2':
                    PageMethods.DeleteRegisterOption2(id, Success, Failure);
                    break;
                case 'Option3':
                    PageMethods.DeleteRegisterOption3(id, Success, Failure);
                    break;
                default:
            }
        }

        //刪除資料events Success callback
        function Success(result) {
            //ShowDialogSuccessReload(result);
            //刪除成功 reload gridview
            <%= btnReloadGridView.ClientID%>.click();
        }

        //刪除資料events Failure callback
        function Failure(error) {
            ShowDialogFailed();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
     <table>
        <tr>
            <td>
                <asp:Image ID="lblPageImage" runat="server" ImageUrl="~/Master/images/Link_CreateEvents.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="建立活動"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblContent" runat="server" Text="欲參加的內容"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLimit" runat="server" Text="人數上限"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtContent" runat="server" Width="250px" CssClass="QueryField"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtLimit" runat="server" Width="150px" CssClass="QueryField" onkeypress="if(event.keyCode < 48 || event.keyCode >57) event.returnValue = false;"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="Button" OnClick="btnAdd_Click" />
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption1" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnPageIndexChanging="gridRegisterOption1_PageIndexChanging" OnRowDataBound="gridRegisterOption1_RowDataBound" >
                    <Columns>
                        <asp:BoundField HeaderText="欲參加的內容" DataField="description">
                            <HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="人數上限" DataField="limit">
                            <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_DeleteManager" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>'
                                    OnClientClick='<%# "ShowDialogDelete(\"Option1\",\""+ Eval("id") + "\");return false;" %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                    <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table style="margin-top:35px">
        <tr>
            <td>
                <asp:Label ID="lblTransportation" runat="server" Text="交通車"></asp:Label>
            </td>
            
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtTransportation" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAddTransportation" runat="server" Text="新增" CssClass="Button" OnClick="btnAddTransportation_Click"/>
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption2" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnPageIndexChanging="gridRegisterOption2_PageIndexChanging" OnRowDataBound="gridRegisterOption2_RowDataBound" >
                    <Columns>
                        <asp:BoundField HeaderText="交通車" DataField="description">
                            <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_DeleteManager" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>'
                                    OnClientClick='<%# "ShowDialogDelete(\"Option2\",\""+ Eval("id") + "\");return false;" %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                    <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table style="margin-top:35px">
        <tr>
            <td>
                <asp:Label ID="lblMeal" runat="server" Text="餐點內容"></asp:Label>
            </td>
            
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtMeal" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAddMeal" runat="server" Text="新增" CssClass="Button" OnClick="btnAddMeal_Click"/>
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption3" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnPageIndexChanging="gridRegisterOption3_PageIndexChanging" OnRowDataBound="gridRegisterOption3_RowDataBound" >
                    <Columns>
                        <asp:BoundField HeaderText="餐點內容" DataField="description">
                            <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_DeleteManager" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>'
                                    OnClientClick='<%# "ShowDialogDelete(\"Option3\",\""+ Eval("id") + "\");return false;" %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                    <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table style="padding-top: 35px">
        <tr>
            <td>
                <asp:Button ID="btnfinish" runat="server" Text="完成" OnClick="btnfinish_Click" CssClass="Button" />
            </td>
        </tr>
    </table>

    <div id="dialogRequired" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <asp:Label ID="lblRequiredMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。"></asp:Label>
            <asp:Label ID="lblErrMsgTxt" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label><br />
            <asp:Label ID="lblErrMsg" runat="server" Text="" Visible="false"></asp:Label><br />
        </asp:Panel>
    </div>

    <div id="dialogDelete" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none">
            <asp:Label ID="lblDeleteWarning" runat="server" Text="確定刪除該筆資料？"></asp:Label>
        </asp:Panel>
    </div>

    <%--dialog Msg--%>
    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel8" runat="server" Style="display: none">
            <asp:Label ID="lblDialogMsg" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>

    <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="false"></asp:Label>
    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
    <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
    <asp:Button ID="btnReloadGridView" runat="server" Text="Button" OnClick="btnReloadGridView_Click" style="display:none;"/>

</asp:Content>

