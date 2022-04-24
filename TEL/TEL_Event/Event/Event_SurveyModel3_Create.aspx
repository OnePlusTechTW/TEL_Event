<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_SurveyModel3_Create.aspx.cs" Inherits="Event_SurveyModel3_Create" Culture="auto" StylesheetTheme="Event" meta:resourcekey="PageResource1" UICulture="auto" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript"> 
        function fk(id, isWrite) {

            document.getElementById(id).disabled = isWrite;
        }

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
                    width: "500px",
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
            <td colspan="5">
                <asp:DropDownList ID="FIELD_Q1" runat="server" Width="120px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q2" runat="server" meta:resourcekey="LABEL_Q2Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q2_1" runat="server" Text="是" Value="是" CssClass="Normal" GroupName="FIELD_Q2" onclick="fk('ContentPlaceHolder1_FIELD_Q2Reason',false)"></asp:RadioButton>
            </td>
            <td colspan="4">
                <asp:RadioButton ID="FIELD_Q2_2" runat="server" Text="否" Value="否" CssClass="Normal" GroupName="FIELD_Q2" onclick="fk('ContentPlaceHolder1_FIELD_Q2Reason',true)"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="LABEL_Q2Reason" runat="server" meta:resourcekey="LABEL_Q2ReasonResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:TextBox ID="FIELD_Q2Reason" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q3" runat="server" meta:resourcekey="LABEL_Q3Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 120px">
                <asp:RadioButton ID="FIELD_Q3_1" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q3" onclick="fk('ContentPlaceHolder1_FIELD_Q3Reason',true)"></asp:RadioButton>
            </td>
            <td style="width: 120px">
                <asp:RadioButton ID="FIELD_Q3_2" runat="server" Text="尚可" Value="尚可" CssClass="Normal" GroupName="FIELD_Q3" onclick="fk('ContentPlaceHolder1_FIELD_Q3Reason',true)"></asp:RadioButton>
            </td>
            <td style="width: 120px" colspan="3">
                <asp:RadioButton ID="FIELD_Q3_3" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q3" onclick="fk('ContentPlaceHolder1_FIELD_Q3Reason',false)"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="LABEL_Q3Reason" runat="server" meta:resourcekey="LABEL_Q3ReasonResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:TextBox ID="FIELD_Q3Reason" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q4" runat="server" meta:resourcekey="LABEL_Q4Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q4_1" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q4" onclick="fk('ContentPlaceHolder1_FIELD_Q4Reason',true)"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q4_2" runat="server" Text="尚可" Value="尚可" CssClass="Normal" GroupName="FIELD_Q4" onclick="fk('ContentPlaceHolder1_FIELD_Q4Reason',true)"></asp:RadioButton>
            </td>
            <td colspan="3">
                <asp:RadioButton ID="FIELD_Q4_3" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q4" onclick="fk('ContentPlaceHolder1_FIELD_Q4Reason',false)"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="LABEL_Q4Reason" runat="server" meta:resourcekey="LABEL_Q4ReasonResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:TextBox ID="FIELD_Q4Reason" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q5" runat="server" meta:resourcekey="LABEL_Q5Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q5_1" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q5" onclick="fk('ContentPlaceHolder1_FIELD_Q5Reason',true)"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q5_2" runat="server" Text="尚可" Value="尚可" CssClass="Normal" GroupName="FIELD_Q5" onclick="fk('ContentPlaceHolder1_FIELD_Q5Reason',true)"></asp:RadioButton>
            </td>
            <td colspan="3">
                <asp:RadioButton ID="FIELD_Q5_3" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q5" onclick="fk('ContentPlaceHolder1_FIELD_Q5Reason',false)"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="LABEL_Q5Reason" runat="server" meta:resourcekey="LABEL_Q5ReasonResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:TextBox ID="FIELD_Q5Reason" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q6" runat="server" meta:resourcekey="LABEL_Q6Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q6_1" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q6" onclick="fk('ContentPlaceHolder1_FIELD_Q6Reason',true)"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q6_2" runat="server" Text="尚可" Value="尚可" CssClass="Normal" GroupName="FIELD_Q6" onclick="fk('ContentPlaceHolder1_FIELD_Q6Reason',true)"></asp:RadioButton>
            </td>
            <td colspan="3">
                <asp:RadioButton ID="FIELD_Q6_3" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q6" onclick="fk('ContentPlaceHolder1_FIELD_Q6Reason',false)"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="LABEL_Q6Reason" runat="server" meta:resourcekey="LABEL_Q6ReasonResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:TextBox ID="FIELD_Q6Reason" runat="server" Height="60px" TextMode="MultiLine" Width="360px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q7" runat="server" meta:resourcekey="LABEL_Q7Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q7_1" runat="server" Text="滿意" Value="滿意" CssClass="Normal" GroupName="FIELD_Q7" onclick="fk('ContentPlaceHolder1_FIELD_Q7Reason',true)"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="FIELD_Q7_2" runat="server" Text="尚可" Value="尚可" CssClass="Normal" GroupName="FIELD_Q7" onclick="fk('ContentPlaceHolder1_FIELD_Q7Reason',true)"></asp:RadioButton>
            </td>
            <td colspan="3">
                <asp:RadioButton ID="FIELD_Q7_3" runat="server" Text="不滿意" Value="不滿意" CssClass="Normal" GroupName="FIELD_Q7" onclick="fk('ContentPlaceHolder1_FIELD_Q7Reason',false)"></asp:RadioButton>
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
        <tr class="FormTRStyle">
            <td colspan="5">
                <asp:Label ID="LABEL_Q9" runat="server" meta:resourcekey="LABEL_Q9Resource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:RadioButton ID="FIELD_Q9_1" runat="server" Text="需要" Value="需要" CssClass="Normal" GroupName="FIELD_Q9"></asp:RadioButton>
            </td>
            <td colspan="4">
                <asp:RadioButton ID="FIELD_Q9_2" runat="server" Text="不需要" Value="不需要" CssClass="Normal" GroupName="FIELD_Q9"></asp:RadioButton>
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
            <asp:Label ID="lblDeleteWarning" runat="server" Text="問卷送出後即不可修正，您確定要送出嗎？"></asp:Label>
        </asp:Panel>
    </div>

    <asp:Button ID="Button_AddConfirm" runat="server" Text="Button" meta:resourcekey="Button_SubmitResource1" CssClass="Button" Style="display: none" OnClick="Button_AddConfirm_Click" />
    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
    <asp:HiddenField ID="hfmsg" runat="server" Value="確認" />

</asp:Content>

