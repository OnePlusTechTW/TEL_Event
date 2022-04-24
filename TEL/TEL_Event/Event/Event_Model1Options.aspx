<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_Model1Options.aspx.cs" Inherits="Event_Event_Model1Options" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowDialogRequired() {
            $(function () {
                $("#dialogRequired").dialog({
                    title: document.getElementById('<%=lblWarningText.ClientID%>').innerText,
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
                    title: document.getElementById('<%=lblWarningText.ClientID%>').innerText,
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
        function ShowDialogDelete(id) {
            $(function () {
                $("#dialogDelete").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    buttons: [
                        {
                            text: "確定",
                            click: function () {
                                onDelete(id);
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
                    title: document.getElementById('<%=lblWarningText.ClientID%>').innerText,
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
        function onDelete(id) {
            PageMethods.DeleteRegisterOption1(id, Success, Failure);
        }

        //刪除資料events Success callback
        function Success(result) {
            //ShowDialogSuccessReload(result);
            //刪除成功 reload gridview
            $('#<%=btnReloadGridView.ClientID%>')[0].click();
        }

        //刪除資料events Failure callback
        function Failure(error) {
            ShowDialogFailed();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Image ID="lblPageImage" runat="server" ImageUrl="~/Master/images/Link_CreateEvents.png" Height="40px" meta:resourcekey="lblPageImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="lblPageName" runat="server" CssClass="PageTitle" Text="建立活動" meta:resourcekey="lblPageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblContent" runat="server" Text="欲參加的內容" meta:resourcekey="lblContentResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLimit" runat="server" Text="人數上限" meta:resourcekey="lblLimitResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtContent" runat="server" Width="250px" CssClass="QueryField" meta:resourcekey="txtContentResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtLimit" runat="server" Width="150px" CssClass="QueryField" onkeypress="if(event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" meta:resourcekey="txtLimitResource1"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="Button" OnClick="btnAdd_Click" meta:resourcekey="btnAddResource1" />
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridModel1Options" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnPageIndexChanging="gridModel1Options_PageIndexChanging" OnRowDataBound="gridModel1Options_RowDataBound" meta:resourcekey="gridModel1OptionsResource1" >
                    <Columns>
                        <asp:BoundField HeaderText="欲參加的內容" DataField="description" meta:resourcekey="BoundFieldResource1">
                            <HeaderStyle Width="200px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="人數上限" DataField="limit" meta:resourcekey="BoundFieldResource2">
                            <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="" meta:resourcekey="TemplateFieldResource1">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_DeleteManager" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>'
                                    OnClientClick='<%# "ShowDialogDelete(\""+ Eval("id") + "\");return false;" %>' meta:resourcekey="Button_DeleteManagerResource1" />
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
                <asp:Button ID="btnfinish" runat="server" Text="完成" OnClick="btnfinish_Click" CssClass="Button" meta:resourcekey="btnfinishResource1" />
            </td>
        </tr>
    </table>

    <div id="dialogRequired" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none" meta:resourcekey="ContentPanel1Resource1">
            <asp:Label ID="lblRequiredMsg" runat="server" meta:resourcekey="lblRequiredMsgResource1"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none" meta:resourcekey="ContentPanel3Resource1">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。" meta:resourcekey="lblFailedResource1"></asp:Label>
            <asp:Label ID="lblErrMsgTxt" runat="server" Text="錯誤訊息：" Visible="False" meta:resourcekey="lblErrMsgTxtResource1"></asp:Label><br />
            <asp:Label ID="lblErrMsg" runat="server" Visible="False" meta:resourcekey="lblErrMsgResource1"></asp:Label><br />
        </asp:Panel>
    </div>

    <div id="dialogDelete" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none" meta:resourcekey="ContentPanel6Resource1">
            <asp:Label ID="lblDeleteWarning" runat="server" Text="確定刪除該筆資料？" meta:resourcekey="lblDeleteWarningResource1"></asp:Label>
        </asp:Panel>
    </div>

    <%--dialog Msg--%>
    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel8" runat="server" Style="display: none" meta:resourcekey="ContentPanel8Resource1">
            <asp:Label ID="lblDialogMsg" runat="server" meta:resourcekey="lblDialogMsgResource1"></asp:Label>
        </asp:Panel>
    </div>

    <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="False" meta:resourcekey="lblRequiredResource1"></asp:Label>
    <asp:Button ID="btnReloadGridView" runat="server" Text="" OnClick="btnReloadGridView_Click" style="display:none;" meta:resourcekey="btnReloadGridViewResource1"/>

    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;" ></asp:Label>
    <asp:Label ID="lblWarningText" runat="server" Text="警告" meta:resourcekey="lblWarningTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblOptionsNoAdd" runat="server" Text="請確認自訂選項是否新增。" Style="display: none;" meta:resourcekey="lblOptionsNoAddResource1"></asp:Label>
</asp:Content>

