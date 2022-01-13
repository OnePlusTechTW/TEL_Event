<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="MyEvent.aspx.cs" StylesheetTheme="Event" Inherits="Event_MyEvent" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon1.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL_PageName" runat="server" meta:resourcekey="LABEL_PageNameResource1" CssClass="PageTitle"></asp:Label>
            </td>

    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="QUERY_EventName" runat="server" meta:resourcekey="QUERY_EventNameResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="QUERY_EventCategory" runat="server" meta:resourcekey="QUERY_EventCategoryResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="QUERY_EventStatus" runat="server" meta:resourcekey="QUERY_EventStatusResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="FIELD_EventName" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox></td>
            <td>
                <asp:DropDownList ID="FIELD_EventCategory" runat="server" Width="120px" CssClass="QueryField"></asp:DropDownList></td>
            <td>
                <asp:DropDownList ID="FIELD_EventStatus" runat="server" Width="100px" CssClass="QueryField">
                    <asp:ListItem Text="- 全部 -" Value="" meta:resourcekey="FIELD_EventStatusitem_all"></asp:ListItem>
                    <asp:ListItem Text="尚未開始" Value="N" meta:resourcekey="FIELD_EventStatusitem_1"></asp:ListItem>
                    <asp:ListItem Text="進行中" Value="O" meta:resourcekey="FIELD_EventStatusitem_2"></asp:ListItem>
                    <asp:ListItem Text="已結束" Value="F" meta:resourcekey="FIELD_EventStatusitem_3"></asp:ListItem>
                </asp:DropDownList></td>
            <td>
                <asp:Button ID="Button_Query" runat="server" Text="Button" meta:resourcekey="Button_QueryResource1" OnClick="Button_Query_Click" CssClass="Button" /></td>
        </tr>
    </table>
    <table cellspacing="0">
        <tr>
            <td>
                <asp:GridView ID="FIELD_Result" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" meta:resourcekey="FIELD_ResultResource1" AutoGenerateColumns="False" BorderColor="White"
                    OnRowDataBound="FIELD_Result_RowDataBound" OnPageIndexChanging="FIELD_Result_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="活動名稱" meta:resourcekey="FIELD_Result_Name" DataField="eventname">
                            <HeaderStyle Width="250px"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="活動分類" meta:resourcekey="FIELD_Result_Category" DataField="categoryname">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="報名開始日期時間" meta:resourcekey="FIELD_Result_Regstart" DataField="registerstart">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="報名結束日期時間" meta:resourcekey="FIELD_Result_Regend" DataField="registerend">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="活動開始日期" meta:resourcekey="FIELD_Result_Eventstart" DataField="eventstart">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="活動結束日期" meta:resourcekey="FIELD_Result_Eventend" DataField="eventend">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="活動狀態" meta:resourcekey="FIELD_Result_Status" DataField="status">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="報名資料" meta:resourcekey="FIELD_Result_Event">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_RegisterEdit" runat="server" Text="編輯" CssClass="Button_Gridview" CommandArgument='<%# Eval("registerinfo") %>' OnClick="Button_RegisterEdit_Click" />
                                <asp:Button ID="Button_RegisterView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("registerinfo") %>'  />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="問卷資料" meta:resourcekey="FIELD_Result_Survey">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_SurveyCreate" runat="server" Text="填寫" CssClass="Button_Gridview" CommandArgument='<%# Eval("surveyinfo") %>' OnClick="Button_SurveyCreate_Click" />
                                <asp:Button ID="Button_SurveyView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("surveyinfo") %>' />
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
</asp:Content>

