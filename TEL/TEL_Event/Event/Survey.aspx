<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Survey.aspx.cs" Inherits="Event_Survey" Culture="auto" StylesheetTheme="Event" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon2.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL_PageName" runat="server" CssClass="PageTitle" meta:resourcekey="LABEL_PageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px"></tr>
    </table>
    <table>
        <tr>
            <td id="TD_Category" runat="server" style="text-align: center; width: 120px">
                <asp:Label ID="FIELD_Category" runat="server" CssClass="ShowCategory"></asp:Label>
            </td>
            <td>
                <asp:Label ID="FIELD_EventName" runat="server" CssClass="ShowCategory"></asp:Label>
            </td>
        </tr>
    </table>
    <table>
        <tr style="height: 15px">
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:Label ID="QUERY_EmpName" runat="server" meta:resourcekey="QUERY_EmpNameResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="FIELD_EmpName" runat="server" Width="120px" CssClass="QueryField"></asp:TextBox></td>
            <td>
            <td>
                <asp:Button ID="Button_Query" runat="server" Text="Button" meta:resourcekey="Button_QueryResource1" OnClick="Button_Query_Click" CssClass="Button" />
            </td>
        </tr>
    </table>
    <table cellspacing="0">
        <tr style="text-align: right">
            <td style="width: 100%">
                <asp:Image ID="FIELD_People" runat="server" ImageUrl="~/Master/images/people.png" Height="20px" />
                <asp:Label ID="FIELD_Count" runat="server" CssClass="ShowPeopleCount"></asp:Label>
            </td>
            <td>
                <asp:Button ID="Button_ExportExcel" runat="server" Text="Button" meta:resourcekey="Button_ExportExcelResource1" CssClass="Button" OnClick="Button_ExportExcel_Click" /></td>
        </tr>
        <tr style="height: 10px"></tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="FIELD_Result" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                    EmptyDataText="無符合資料" meta:resourcekey="FIELD_ResultResource1" AutoGenerateColumns="False" BorderColor="White"
                    OnRowDataBound="FIELD_Result_RowDataBound" OnPageIndexChanging="FIELD_Result_PageIndexChanging" PageSize="20">
                    <Columns>
                        <asp:BoundField HeaderText="工號" meta:resourcekey="FIELD_Result_Empid" DataField="empid">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="中文姓名" meta:resourcekey="FIELD_Result_EmpNameCH" DataField="empnamech">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="英文姓名" meta:resourcekey="FIELD_Result_EmpNameEN" DataField="empnameen">
                            <HeaderStyle Width="150px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="填寫日期" meta:resourcekey="FIELD_Result_FillinDate" DataField="fillindate">
                            <HeaderStyle Width="100px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="問卷資料" meta:resourcekey="FIELD_Result_Survey">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_SurveyView" runat="server" Text="檢視" CssClass="Button_Gridview" CommandArgument='<%# Eval("surveyid") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_Delete" runat="server" Text="編輯" meta:resourcekey="Button_Delete" CssClass="Button_Gridview" CommandArgument='<%# Eval("surveyinfo") %>' OnClick="Button_Delete_Click" />
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

