<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="PastEvent.aspx.cs" Inherits="Event_PastEvent" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource1"%>
<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        $(function () {

            $('#<%= sDate.ClientID%>').datepicker({
                dateFormat: 'yy/mm/dd',
                changeMonth: true,
                changeYear: true,
                onClose: function (selectedDate) {
                    $('#<%= eDate.ClientID%>').datepicker("option", "minDate", selectedDate);
                }
            });

            $('#<%= eDate.ClientID%>').datepicker({
                dateFormat: 'yy/mm/dd',
                onClose: function (selectedDate) {
                    $('#<%= sDate.ClientID%>').datepicker("option", "maxDate", selectedDate);
                    $('#<%= eDate.ClientID%>').val($(this).val());
                }
            });


        });

        function ShowDialogView(id) {
            $(function () {
                $("#dialogView").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    changeMonth: true,
                    changeYear: true,
                    width: "1200px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+100", },
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    }
                });

                $("#dialogView").load('Event_View.aspx?id=' + id);
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon6.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL1" runat="server" CssClass="PageTitle" Text="過去活動" meta:resourcekey="LABEL1Resource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEventName" runat="server" Text="活動名稱" meta:resourcekey="lblEventNameResource1"></asp:Label>
            </td>
            <td >&nbsp;
                <asp:Label ID="lblEventCategory" runat="server" Text="活動分類" meta:resourcekey="lblEventCategoryResource1"></asp:Label>
            </td>
            <td >&nbsp;
                <asp:Label ID="lblDateInterval" runat="server" Text="活動開始日期區間" meta:resourcekey="lblDateIntervalResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbEventName" runat="server" CssClass="QueryField" meta:resourcekey="tbEventNameResource1"></asp:TextBox>
            </td>
            <td >&nbsp;
                <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass="QueryField" meta:resourcekey="ddlEventCategoryResource1">
                </asp:DropDownList>
            </td>
            <td >&nbsp;
                <asp:TextBox ID="sDate" runat="server" Width="160px"  CssClass="QueryField" meta:resourcekey="sDateResource1"></asp:TextBox>
                ~
                <asp:TextBox ID="eDate" runat="server" Width="160px"  CssClass="QueryField" meta:resourcekey="eDateResource1"></asp:TextBox>
            </td>
            <td>&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" CssClass="Button" meta:resourcekey="btnSearchResource1"  />
            </td>
        </tr>
    </table>
    <table style="padding-top:10px" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gridEvents" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                        EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                        PageSize="20" OnPageIndexChanging="gridEvent_PageIndexChanging" OnRowDataBound="gridEvent_RowDataBound" meta:resourcekey="gridEventsResource1" >
                        <Columns>
                            <asp:BoundField HeaderText="活動名稱" DataField="eventname" meta:resourcekey="BoundFieldResource1">
                                <HeaderStyle Width="300px" ></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動分類" DataField="categoryname" meta:resourcekey="BoundFieldResource2">
                                <HeaderStyle Width="120px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動開始日期" DataField="eventstart" meta:resourcekey="BoundFieldResource3">
                                <HeaderStyle Width="105px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動結束日期" DataField="eventend" meta:resourcekey="BoundFieldResource4">
                                <HeaderStyle  Width="105px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="活動資訊" meta:resourcekey="TemplateFieldResource1">
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid") %>' OnClick="btnView_Click" meta:resourcekey="btnViewResource1" />
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
    <div id="dialogView" title="Dialog Title">
        
    </div>

    <asp:Label ID="item_all" runat="server" Text="- 全部 -" style="display:none" meta:resourcekey="item_allResource1"></asp:Label>

    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
</asp:Content>

