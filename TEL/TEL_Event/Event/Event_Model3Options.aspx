﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_Model3Options.aspx.cs" Inherits="Event_Event_Model3Options" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        //上傳健檢群組開窗
        function ShowDialogFileUpload(event, id) {
            $(function () {
                var dialog = $("#dialogFileUpload").dialog({
                    title: "",
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel7").style.display = "block";
                    },
                    width: "450px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", }
                });
                dialog.parent().appendTo(jQuery("form:first"));
            });

        };

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
            PageMethods.DeleteRegisterOption5(id, Success, Failure);
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
                <asp:Button ID="btnImportHealthSolutions" runat="server" CssClass="Button" Text="匯入健檢方案" OnClick="btnImportHealthSolutions_Click" Width="120px" meta:resourcekey="btnImportHealthSolutionsResource1" />
            </td>
            <td style="padding-left: 10px;">
                <asp:HyperLink ID="hlnkFileAttachment" runat="server" Text="Excel範例" Style="color: blue;" NavigateUrl="~/Sample/Import_HealthSolutions.xlsx" meta:resourcekey="hlnkFileAttachmentResource1" />
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption4" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnRowDataBound="gridRegisterOption4_RowDataBound" OnPageIndexChanging="gridRegisterOption4_PageIndexChanging" meta:resourcekey="gridRegisterOption4Resource1">
                    <Columns>
                        <asp:BoundField HeaderText="醫院" DataField="hosipital" meta:resourcekey="BoundFieldResource1">
                            <HeaderStyle Width="100px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="地區" DataField="area" meta:resourcekey="BoundFieldResource2">
                            <HeaderStyle Width="150px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="費用方案" DataField="description" meta:resourcekey="BoundFieldResource3">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="性別" DataField="gender" meta:resourcekey="BoundFieldResource4">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="次方案1" DataField="secondoption1" meta:resourcekey="BoundFieldResource5">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="次方案2" DataField="secondoption2" meta:resourcekey="BoundFieldResource6">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="次方案3" DataField="secondoption3" meta:resourcekey="BoundFieldResource7">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="日期" DataField="avaliabledate" meta:resourcekey="BoundFieldResource8">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="人數上限" DataField="limit" meta:resourcekey="BoundFieldResource9">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                    <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table style="margin-top: 35px">
        <tr>
            <td>
                <asp:Label ID="lblSendArea" runat="server" Text="健檢包寄送地點" meta:resourcekey="lblSendAreaResource1"></asp:Label>
            </td>

            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtSendArea" runat="server" Width="200px" CssClass="QueryField" meta:resourcekey="txtSendAreaResource1"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnAdd" runat="server" Text="新增" CssClass="Button" OnClick="btnAdd_Click" meta:resourcekey="btnAddResource1" />
            </td>
        </tr>
    </table>
    <table style="padding-top: 10px" cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="gridRegisterOption5" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnPageIndexChanging="gridRegisterOption5_PageIndexChanging" OnRowDataBound="gridRegisterOption5_RowDataBound" meta:resourcekey="gridRegisterOption5Resource1">
                    <Columns>
                        <asp:BoundField HeaderText="健檢包寄送地點" DataField="description" meta:resourcekey="BoundFieldResource10">
                            <HeaderStyle Width="120px"></HeaderStyle>
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


    <div id="dialogFileUpload" title="Dialog Title">
        <asp:Panel ID="ContentPanel7" runat="server" Style="display: none" meta:resourcekey="ContentPanel7Resource1">
            <div>
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="FileUpload" meta:resourcekey="FileUpload1Resource1" />
            </div>
            <div style="margin-top: 20px;">
                <asp:Button ID="btnImport" runat="server" Text="匯入" OnClick="btnImport_Click" CssClass="Button" meta:resourcekey="btnImportResource1" />
            </div>
            <div style="margin-top: 5px;">
                <asp:TextBox ID="tbImportMsg" runat="server" TextMode="MultiLine" Height="250px" Width="412px" placeholder="匯入資訊..." ReadOnly="True" meta:resourcekey="tbImportMsgResource1"></asp:TextBox>
            </div>
        </asp:Panel>
    </div>

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

    <asp:Label ID="lblDuplicate" runat="server" Text="資料列 {0} 為重複資料。" Visible="False" meta:resourcekey="lblDuplicateResource1"></asp:Label>
    <asp:Label ID="lblLoseData" runat="server" Text="資料列 {0} 的必填欄位必須輸入。" Visible="False" meta:resourcekey="lblLoseDataResource1"></asp:Label>
    <asp:Label ID="lblReimport" runat="server" Text="請重新匯入。" Visible="False" meta:resourcekey="lblReimportResource1"></asp:Label>
    <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="False" meta:resourcekey="lblRequiredResource1"></asp:Label>
    <asp:Label ID="lblImportSuccess" runat="server" Text="匯入成功。" Visible="False" meta:resourcekey="lblImportSuccessResource1"></asp:Label>
    <asp:Label ID="lblImportFailed" runat="server" Text="匯入失敗，請重新匯入。" Visible="False" meta:resourcekey="lblImportFailedResource1"></asp:Label>
    <asp:Label ID="lblImportFailedMsg" runat="server" Text="錯誤訊息：" Visible="False" meta:resourcekey="lblImportFailedMsgResource1"></asp:Label>
    <asp:Button ID="btnReloadGridView" runat="server" Text="" OnClick="btnReloadGridView_Click" Style="display: none;" meta:resourcekey="btnReloadGridViewResource1" />

    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblWarningText" runat="server" Text="警告" meta:resourcekey="lblWarningTextResource1" Style="display: none;"></asp:Label>

    <asp:Label ID="lblOptionsNoAdd" runat="server" Text="請確認自訂選項是否新增。" Style="display: none;" meta:resourcekey="lblOptionsNoAddResource1"></asp:Label>

</asp:Content>

