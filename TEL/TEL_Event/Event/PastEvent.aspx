﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="PastEvent.aspx.cs" Inherits="Event_PastEvent" StylesheetTheme="Event" Culture="auto" UICulture="auto"%>
<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        $(function () {

            $('#<%= sDate.ClientID%>').prop("readonly", true).datepicker({
                dateFormat: 'yy/mm/dd',
                onClose: function (selectedDate) {
                    $('#<%= eDate.ClientID%>').datepicker("option", "minDate", selectedDate);
                }
            });

            $('#<%= eDate.ClientID%>').prop("readonly", true).datepicker({
                dateFormat: 'yy/mm/dd',
                onClose: function (selectedDate) {
                    $('#<%= sDate.ClientID%>').datepicker("option", "maxDate", selectedDate);
                    $('#<%= eDate.ClientID%>').val($(this).val());
                }
            });


        });

        function ShowDialogView() {
            $(function () {
                //$("#dialogView").load('Event_Create.aspx?id=af3bb0d8-3850-4e29-b469-400f372d3868');

                $("#dialogView").dialog({
                    title: "",
                    modal: true,
                    width: "645px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+100", },
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

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon6.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL1" runat="server" CssClass="PageTitle" Text="過去活動"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblEventName" runat="server" Text="活動名稱"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblEventCategory" runat="server" Text="活動分類"></asp:Label>
            </td>
            <td >
                <asp:Label ID="lblDateInterval" runat="server" Text="活動開始日期區間"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="tbEventName" runat="server" CssClass="QueryField"></asp:TextBox>
            </td>
            <td >
                <asp:DropDownList ID="ddlEventCategory" runat="server" CssClass="QueryField">
                </asp:DropDownList>
            </td>
            <td >
                <asp:TextBox ID="sDate" runat="server" Width="160px"  CssClass="QueryField"></asp:TextBox>
                ~
                <asp:TextBox ID="eDate" runat="server" Width="160px"  CssClass="QueryField"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="查詢" OnClick="btnSearch_Click" CssClass="Button"  />
            </td>
        </tr>
    </table>
    <table style="padding-top:10px" cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gridEvents" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                        EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                        PageSize="20" OnPageIndexChanging="gridEvent_PageIndexChanging" OnRowDataBound="gridEvent_RowDataBound" >
                        <Columns>
                            <asp:BoundField HeaderText="活動名稱" DataField="eventname">
                                <HeaderStyle Width="200px" ></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動分類" DataField="categoryname">
                                <HeaderStyle Width="95px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動開始日期" DataField="eventstart">
                                <HeaderStyle Width="105px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="活動結束日期" DataField="eventend">
                                <HeaderStyle  Width="105px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            
                            <asp:TemplateField HeaderText="活動資訊">
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("eventnid")%>' OnClick="btnView_Click" />
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
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
                <uc1:UC_EventDescription runat="server" id="UC_EventDescription" />
        </asp:Panel>
    </div>

    <asp:Label ID="item_all" runat="server" Text="- 全部 -" style="display:none"></asp:Label>
</asp:Content>

