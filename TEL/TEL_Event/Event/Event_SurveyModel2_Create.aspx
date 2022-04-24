<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_SurveyModel2_Create.aspx.cs" Inherits="Event_SurveyModel2_Create" Culture="auto" StylesheetTheme="Event" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript"> 
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
                    },
                    width: "450px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", }
                });
            });

        };

        function ShowDialogConfirm() {
            $(function () {
                $("#dialogSubmit").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    buttons: [
                        {
                            text: "確定",
                            click: function () {
                               <%= Button_AddConfirm.ClientID%>.click();
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
                    },
                    width: "350px",
                    Height: "300px",
                });
        });

        };

    </script>
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon3.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL_PageName" runat="server" CssClass="PageTitle" meta:resourcekey="LABEL_PageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px"></tr>
    </table>
   <table style="width: 650px">
        <tr>
            <td>
                <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
            </td>
        </tr>
        <tr style="height: 10px"></tr>
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
                <asp:TextBox ID="FIELD_Station" runat="server" CssClass="FillField" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="FIELD_Empid" runat="server" CssClass="FillField" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="FIELD_EmpNameCH" runat="server" CssClass="FillField" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="FIELD_EmpNameEN" runat="server" CssClass="FillField" Enabled="false"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="FIELD_UnitName" runat="server" CssClass="FillField" Enabled="false"></asp:TextBox>
            </td>

        </tr>
    </table>
    <table>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q1" runat="server" meta:resourcekey="LABEL_Q1Resource1"></asp:Label>
            </td>
        </tr>
       <tr>
            <td style="width: 120px">
                <asp:CheckBox ID="FIELD_Q1_1" runat="server" Text="公告" Value="公告" CssClass="Normal" meta:resourcekey="FIELD_Q1_1Resource1"></asp:CheckBox>
            </td>
            <td style="width: 120px">
                <asp:CheckBox ID="FIELD_Q1_2" runat="server" Text="信件宣傳" Value="信件宣傳" CssClass="Normal" meta:resourcekey="FIELD_Q1_2Resource1"></asp:CheckBox>
            </td>
            <td style="width: 120px">
                <asp:CheckBox ID="FIELD_Q1_3" runat="server" Text="海報" Value="海報" CssClass="Normal" meta:resourcekey="FIELD_Q3_1Resource1"></asp:CheckBox>
            </td>
            <td style="width: 120px">
                <asp:CheckBox ID="FIELD_Q1_4" runat="server" Text="同事分享" Value="同事分享" CssClass="Normal" meta:resourcekey="FIELD_Q4_1Resource1"></asp:CheckBox>
            </td>
            <td style="width: 120px">
                <asp:CheckBox ID="FIELD_Q1_5" runat="server" Text="螢幕鎖定畫面" Value="螢幕鎖定畫面" CssClass="Normal" meta:resourcekey="FIELD_Q1_5Resource1"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:CheckBox ID="FIELD_Q1_6" runat="server" Text="其他(自填)" Value="其他(自填)" CssClass="Normal" meta:resourcekey="FIELD_Q1_6Resource1"></asp:CheckBox>
                <asp:TextBox ID="FIELD_Q1Other" runat="server" CssClass="FillField" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q2" runat="server" meta:resourcekey="LABEL_Q2Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q2_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q2" meta:resourcekey="FIELD_Q2_1Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q2_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q2" meta:resourcekey="FIELD_Q2_2Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q2_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q2" meta:resourcekey="FIELD_Q2_3Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q2_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q2" meta:resourcekey="FIELD_Q2_4Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q2_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q2" meta:resourcekey="FIELD_Q2_5Resource1"></asp:RadioButton>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q3" runat="server" meta:resourcekey="LABEL_Q3Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q3_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q3" meta:resourcekey="FIELD_Q2_1Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q3_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q3" meta:resourcekey="FIELD_Q2_2Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q3_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q3" meta:resourcekey="FIELD_Q2_3Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q3_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q3" meta:resourcekey="FIELD_Q2_4Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q3_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q3" meta:resourcekey="FIELD_Q2_5Resource1"></asp:RadioButton>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q4" runat="server" meta:resourcekey="LABEL_Q4Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q4_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q4" meta:resourcekey="FIELD_Q2_1Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q4_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q4" meta:resourcekey="FIELD_Q2_2Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q4_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q4" meta:resourcekey="FIELD_Q2_3Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q4_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q4" meta:resourcekey="FIELD_Q2_4Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q4_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q4" meta:resourcekey="FIELD_Q2_5Resource1"></asp:RadioButton>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q5" runat="server" meta:resourcekey="LABEL_Q5Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q5_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q5" meta:resourcekey="FIELD_Q2_1Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q5_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q5" meta:resourcekey="FIELD_Q2_2Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q5_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q5" meta:resourcekey="FIELD_Q2_3Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q5_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q5" meta:resourcekey="FIELD_Q2_4Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q5_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q5" meta:resourcekey="FIELD_Q2_5Resource1"></asp:RadioButton>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q6" runat="server" meta:resourcekey="LABEL_Q6Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q6_1" runat="server" Text="非常滿意" Value="非常滿意" CssClass="Normal" GroupName="FIELD_Q6" meta:resourcekey="FIELD_Q2_1Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q6_2" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q6" meta:resourcekey="FIELD_Q2_2Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q6_3" runat="server" Text="普通" Value="普通" CssClass="Normal" GroupName="FIELD_Q6" meta:resourcekey="FIELD_Q2_3Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q6_4" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q6" meta:resourcekey="FIELD_Q2_4Resource1"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q6_5" runat="server" Text="非常不滿意" Value="非常不滿意" CssClass="Normal" GroupName="FIELD_Q6" meta:resourcekey="FIELD_Q2_5Resource1"></asp:RadioButton>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q7" runat="server" meta:resourcekey="LABEL_Q7Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:TextBox ID="FIELD_Q7" runat="server" Height="60px" TextMode="MultiLine" Width="360px"></asp:TextBox>
            </td>
        </tr>
       <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q8" runat="server" meta:resourcekey="LABEL_Q8Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:TextBox ID="FIELD_Q8" runat="server" Height="60px" TextMode="MultiLine" Width="360px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Button ID="Button_Submit" runat="server" Text="Button" meta:resourcekey="Button_SubmitResource1" CssClass="Button" OnClick="Button_Submit_Click" />
                <asp:Button ID="Button_Cancel" runat="server" Text="Button" meta:resourcekey="Button_CancelResource1" CssClass="Button" OnClick="Button_Cancel_Click" />
            </td>
        </tr>
    </table>

    <div id="dialogRequired" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <asp:Label ID="lblFiledName" runat="server" Text=""></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogSubmit" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none">
            <asp:Label ID="lblDeleteWarning" runat="server" Text="問卷送出後即不可修正，您確定要送出嗎？" meta:resourcekey="lblDeleteWarningResource1"></asp:Label>
        </asp:Panel>
    </div>

    <asp:Button ID="Button_AddConfirm" runat="server" Text="Button" meta:resourcekey="Button_SubmitResource1" CssClass="Button" Style="display: none" OnClick="Button_AddConfirm_Click" />
    <asp:HiddenField ID="hfWarning" runat="server" Value="警告"  meta:resourcekey="hfWarningResource1"/>
    <asp:HiddenField ID="hfmsg" runat="server" Value="確認"  meta:resourcekey="hfmsgResource1"/>

</asp:Content>

