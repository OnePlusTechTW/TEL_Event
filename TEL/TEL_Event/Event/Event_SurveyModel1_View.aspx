<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event_SurveyModel1_View.aspx.cs" Inherits="Event_Event_SurveyModel1_View" StylesheetTheme="Event" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon4.png" Height="40px"></asp:Image>
                </td>
                <td style="width: 5px"></td>
                <td style="border-bottom: 1.5px solid #19b1e5;">
                    <asp:Label ID="LABEL_PageName" runat="server" CssClass="PageTitle" meta:resourcekey="LABEL_PageNameResource1"></asp:Label>
                </td>
            </tr>
            <tr style="height: 10px"></tr>
        </table>
        <table>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Label ID="LABEL_Station" runat="server" meta:resourcekey="LABEL_StationResource1"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LABEL_Empid" runat="server" meta:resourcekey="LABEL_EmpidResource1"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LABEL_EmpNameCH" runat="server" meta:resourcekey="LABEL_EmpNameCHResource1"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LABEL_EmpNameEN" runat="server" meta:resourcekey="LABEL_EmpNameENResource1"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LABEL_UnitName" runat="server" meta:resourcekey="LABEL_UnitNameResource1"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="FIELD_Station" runat="server" CssClass="FillField" Width="80px" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="FIELD_Empid" runat="server" CssClass="FillField" Width="80px" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="FIELD_EmpNameCH" runat="server" CssClass="FillField" Width="120px" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="FIELD_EmpNameEN" runat="server" CssClass="FillField" Width="200px" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="FIELD_UnitName" runat="server" CssClass="FillField" Width="200px" Enabled="false"></asp:TextBox>
                </td>

            </tr>
        </table>
        <table>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q1" runat="server" meta:resourcekey="LABEL_Q1Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    <asp:CheckBox ID="FIELD_Q1_1" runat="server" Text="公告" Value="公告" CssClass="Normal" Enabled="false"></asp:CheckBox>
                </td>
                <td style="width: 120px">
                    <asp:CheckBox ID="FIELD_Q1_2" runat="server" Text="信件宣傳" Value="信件宣傳" CssClass="Normal" Enabled="false"></asp:CheckBox>
                </td>
                <td style="width: 120px">
                    <asp:CheckBox ID="FIELD_Q1_3" runat="server" Text="海報" Value="海報" CssClass="Normal" Enabled="false"></asp:CheckBox>
                </td>
                <td style="width: 120px">
                    <asp:CheckBox ID="FIELD_Q1_4" runat="server" Text="同事分享" Value="同事分享" CssClass="Normal" Enabled="false"></asp:CheckBox>
                </td>
                <td style="width: 120px">
                    <asp:CheckBox ID="FIELD_Q1_5" runat="server" Text="螢幕鎖定畫面" Value="螢幕鎖定畫面" CssClass="Normal" Enabled="false"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:CheckBox ID="FIELD_Q1_6" runat="server" Text="其他(自填)" Value="其他(自填)" CssClass="Normal" Enabled="false" meta:resourcekey="FIELD_Q1_6Resource1"></asp:CheckBox>
                    <asp:TextBox ID="FIELD_Q1Other" runat="server" CssClass="FillField" Width="150px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q2" runat="server" meta:resourcekey="LABEL_Q2Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="FIELD_Q2_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q2" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q2_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q2" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q2_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q2" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q2_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q2" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q2_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q2" Enabled="false"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q3" runat="server" meta:resourcekey="LABEL_Q3Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="FIELD_Q3_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q3" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q3_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q3" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q3_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q3" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q3_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q3" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q3_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q3" Enabled="false"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q4" runat="server" meta:resourcekey="LABEL_Q4Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="FIELD_Q4_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q4" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q4_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q4" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q4_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q4" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q4_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q4" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q4_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q4" Enabled="false"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q5" runat="server" meta:resourcekey="LABEL_Q5Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="FIELD_Q5_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q5" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q5_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q5" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q5_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q5" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q5_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q5" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q5_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q5" Enabled="false"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q6" runat="server" meta:resourcekey="LABEL_Q6Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="FIELD_Q6_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q6" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q6_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q6" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q6_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q6" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q6_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q6" Enabled="false"></asp:RadioButton>
                </td>
                <td>
                    <asp:RadioButton ID="FIELD_Q6_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q6" Enabled="false"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q7" runat="server" meta:resourcekey="LABEL_Q7Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="FIELD_Q7_1" runat="server" Text="願意" Value="願意" CssClass="Normal" GroupName="FIELD_Q7" Enabled="false"></asp:RadioButton>
                </td>
                <td colspan="4">
                    <asp:RadioButton ID="FIELD_Q7_2" runat="server" Text="不願意" Value="不願意" CssClass="Normal" GroupName="FIELD_Q7" Enabled="false"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q7Reason" runat="server" meta:resourcekey="LABEL_Q7ReasonResource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:TextBox ID="FIELD_Q7Reason" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q8" runat="server" meta:resourcekey="LABEL_Q8Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:TextBox ID="FIELD_Q8" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q9" runat="server" meta:resourcekey="LABEL_Q9Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:TextBox ID="FIELD_Q9" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="LABEL_Q10" runat="server" meta:resourcekey="LABEL_Q10Resource1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:TextBox ID="FIELD_Q10" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
