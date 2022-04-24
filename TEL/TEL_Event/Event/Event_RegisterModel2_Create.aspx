<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_RegisterModel2_Create.aspx.cs" Inherits="Event_Event_RegisterModel2_Create" StylesheetTheme="Event" Culture="auto" UICulture="auto" meta:resourcekey="PageResource2" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>
<%@ Register Src="~/Event/UserControl/PageLoader.ascx" TagPrefix="uc1" TagName="PageLoader" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            //家屬生日年月日
            $('#<%= txtFBDay.ClientID%>').prop("readonly", true).datepicker({
                dateFormat: 'yy/mm/dd',
                changeMonth: true,
                changeYear: true,
                showMonthAfterYear: true,
                yearRange: 'c-80:c'
            });
        });

        function ShowDialogMsg() {
            $(function () {
                $("#dialogMsg").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    width: "700px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", },
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

        function ShowRegisterSccessDialog() {
            $(function () {
                $("#dialogRegisterSccess").dialog({
                    title: document.getElementById('<%=lblMsgText.ClientID%>').innerText,
                    modal: true,
                    width: "700px",
                    Height: "500px",
                    position: { my: "center center", at: "center top+175", },
                    buttons: {
                        Close: function () {
                            $('#<%=btnGoBackPage.ClientID%>')[0].click();
                        $(this).dialog('close');
                    }
                },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel2").style.display = "block";
                    }
                });
        });

        }

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PageLoader runat="server" ID="PageLoader" />
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon3.png" Height="40px" meta:resourcekey="ImageResource1"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL_PageName" runat="server" CssClass="PageTitle" Text="馬上報名" meta:resourcekey="LABEL_PageNameResource1"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px"></tr>
    </table>
    <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblStation" runat="server" Text="勤務地" meta:resourcekey="lblStationResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEmpid" runat="server" Text="工號" meta:resourcekey="lblEmpidResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblCName" runat="server" Text="中文姓名" meta:resourcekey="lblCNameResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblEName" runat="server" Text="英文姓名" meta:resourcekey="lblENameResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblDepartment" runat="server" Text="部門" meta:resourcekey="lblDepartmentResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtStation" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtStationResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtEmpid" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtEmpidResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtCName" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtCNameResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtEName" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtENameResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtDepartment" runat="server" CssClass="FillField" Enabled="false" meta:resourcekey="txtDepartmentResource1"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr class="FormTRStyle">
            <td>
                <asp:Label ID="lblID" runat="server" Text="身份證字號" meta:resourcekey="lblIDResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblBDay" runat="server" Text="出生年月日" meta:resourcekey="lblBDayResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblGender" runat="server" Text="性別" meta:resourcekey="lblGenderResource1"></asp:Label>
            </td>
            <td colspan="2">
                <asp:Label ID="lblEmail" runat="server" Text="Email" meta:resourcekey="lblEmailResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtID" runat="server" ReadOnly="True" CssClass="QueryField" meta:resourcekey="txtIDResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtBDay" runat="server" ReadOnly="True" CssClass="QueryField" meta:resourcekey="txtBDayResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtGender" runat="server" ReadOnly="True" CssClass="QueryField" meta:resourcekey="txtGenderResource1"></asp:TextBox>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True" CssClass="QueryField" Width="316px" meta:resourcekey="txtEmailResource1"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="6">
                <asp:Label ID="lblAttendContent" runat="server" Text="欲參加的內容" meta:resourcekey="lblAttendContentResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:DropDownList ID="ddlAttendContent" runat="server" CssClass="QueryField" Width="100%" meta:resourcekey="ddlAttendContentResource1">
                    <asp:ListItem Selected="True" Value="" Text="- 未指定 -" meta:resourcekey="ListItemResource1"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td colspan="3"></td>
        </tr>
        <tr class="FormTRStyle">
            <td>
                <asp:Label ID="lblPhone" runat="server" Text="手機" meta:resourcekey="lblPhoneResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTransportation" runat="server" Text="交通車" meta:resourcekey="lblTransportationResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblMeal" runat="server" Text="餐點內容" meta:resourcekey="lblMealResource1"></asp:Label>
            </td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" CssClass="QueryField" MaxLength="10" onkeypress="if(event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" meta:resourcekey="txtPhoneResource1"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ddlTransportation" runat="server" CssClass="QueryField" Width="100%" meta:resourcekey="ddlTransportationResource1">
                    <asp:ListItem Selected="True" Value="" Text="- 未指定 -" meta:resourcekey="ListItemResource2"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlMeal" runat="server" CssClass="QueryField" Width="100%" meta:resourcekey="ddlMealResource1">
                    <asp:ListItem Selected="True" Value="" Text="- 未指定 -" meta:resourcekey="ListItemResource3"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr class="FormTRStyle">
            <td>
                <asp:Label ID="lblFName" runat="server" Text="家屬姓名" meta:resourcekey="lblFNameResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFID" runat="server" Text="家屬身分證字號" meta:resourcekey="lblFIDResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFBDay" runat="server" Text="家屬生日年月日" meta:resourcekey="lblFBDayResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFGender" runat="server" Text="家屬性別" meta:resourcekey="lblFGenderResource1"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblFMeal" runat="server" Text="餐點內容" meta:resourcekey="lblFMealResource1"></asp:Label>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtFName" runat="server" CssClass="QueryField" meta:resourcekey="txtFNameResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtFID" runat="server" MaxLength="10" CssClass="QueryField" meta:resourcekey="txtFIDResource1"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtFBDay" runat="server" CssClass="QueryField" meta:resourcekey="txtFBDayResource1"></asp:TextBox>
            </td>
            <td>
                <asp:DropDownList ID="ddlGender" runat="server" CssClass="QueryField" Width="100%" meta:resourcekey="ddlGenderResource1">
                    <asp:ListItem Selected="True" Value="" Text="- 未指定 -" meta:resourcekey="ListItemResource4"></asp:ListItem>
                    <asp:ListItem Value="男" Text="男" meta:resourcekey="ListItemResource5"></asp:ListItem>
                    <asp:ListItem Value="女" Text="女" meta:resourcekey="ListItemResource6"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlFMeal" runat="server" CssClass="QueryField" Width="100%" meta:resourcekey="ddlFMealResource1">
                    <asp:ListItem Selected="True" Value="" Text="- 未指定 -" meta:resourcekey="ListItemResource7"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnFAdd" runat="server" Text="新增" OnClick="btnFAdd_Click" CssClass="Button" Width="80px" meta:resourcekey="btnFAddResource1" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="gridRegisterModel2family" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True" Width="100%"
                    EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                    PageSize="20" OnPageIndexChanging="gridRegisterModel2family_PageIndexChanging" OnRowDataBound="gridRegisterModel2family_RowDataBound" meta:resourcekey="gridRegisterModel2familyResource1">
                    <Columns>
                        <asp:BoundField DataField="name" HeaderText="家屬姓名" meta:resourcekey="BoundFieldResource1">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="idno" HeaderText="家屬身分證字號" meta:resourcekey="BoundFieldResource2">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="birthday" HeaderText="家屬生日年月日" meta:resourcekey="BoundFieldResource3">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="gender" HeaderText="家屬性別" meta:resourcekey="BoundFieldResource4">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="meal" HeaderText="餐點內容" meta:resourcekey="BoundFieldResource5">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="" meta:resourcekey="TemplateFieldResource1">
                            <HeaderStyle Width="80px"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Button ID="Button_Delete" runat="server" Text="刪除" CssClass="Button_Gridview" OnClick="Button_Delete_Click" meta:resourcekey="Button_DeleteResource1" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                    <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                </asp:GridView>
            </td>
            <td></td>
        </tr>
        <tr class="FormTRStyle">
            <td colspan="6">
                <asp:Label ID="lblComment" runat="server" Text="意見/問題回饋" meta:resourcekey="lblCommentResource1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" Width="100%" Height="100px" CssClass="QueryField" meta:resourcekey="txtCommentResource1"></asp:TextBox>
            </td>
            <td colspan="3"></td>
        </tr>
        <tr>
            <td colspan="6">
                <asp:Button ID="btnSummit" runat="server" Text="送出" CssClass="Button" OnClick="btnSummit_Click" OnClientClick="ShowProgressBar();" meta:resourcekey="btnSummitResource1" />
                <asp:Button ID="btnCannel" runat="server" Text="取消" CssClass="Button" OnClick="btnCannel_Click" meta:resourcekey="btnCannelResource1" />
            </td>
        </tr>
    </table>

    <div id="dialogMsg" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none" meta:resourcekey="ContentPanel1Resource1">
            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogRegisterSccess" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none" meta:resourcekey="ContentPanel2Resource1">
            <asp:Label ID="lblRegisterSccess" runat="server" Text="報名成功" meta:resourcekey="lblRegisterSccessResource1"></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none" meta:resourcekey="ContentPanel3Resource1">
            <asp:Label ID="lblFailed" runat="server" Text="失敗。" meta:resourcekey="lblFailedResource1"></asp:Label>
            <asp:Label ID="lblErrMsg" runat="server" meta:resourcekey="lblErrMsgResource1"></asp:Label><br />
        </asp:Panel>
    </div>

    <asp:Label ID="lblRequired" runat="server" Text="欄位 {0} 為必填欄位。" Visible="False" meta:resourcekey="lblRequiredResource1"></asp:Label>
    <asp:Label ID="lblFormatError" runat="server" Text="欄位 {0} 格式錯誤。" Visible="False" meta:resourcekey="lblFormatErrorResource1"></asp:Label>
    <asp:Label ID="lblUnselect" runat="server" Text="- 未指定 -" Visible="False" meta:resourcekey="lblUnselectResource1"></asp:Label>
    <asp:Label ID="lblLimitReached" runat="server" Text="此方案報名人數已達上限，請重新選擇其他方案" Visible="False" meta:resourcekey="lblLimitReachedResource1"></asp:Label>
    <asp:Label ID="lblSendMailFailed" runat="server" Text="但報名成功通知mail寄送失敗。" Visible="False" meta:resourcekey="lblSendMailFailedResource1"></asp:Label>
    <asp:Button ID="btnGoBackPage" runat="server" Text="" OnClick="btnGoBackPage_Click" Style="display: none;" meta:resourcekey="btnGoBackPageResource1" />
    <asp:Label ID="lblRegisterErrMsg" runat="server" Text="報名資料新增發生錯誤。" Visible="False" meta:resourcekey="lblRegisterErrMsgResource1"></asp:Label>
    <asp:Label ID="lblIDFormatErr" runat="server" Text="家屬身份證字號格式錯誤。" Visible="False" meta:resourcekey="lblIDFormatErrResource1"></asp:Label>

    <asp:Label ID="lblEmailSubject" runat="server" Text="【通知】活動填寫完成_{0}" Visible="False" meta:resourcekey="lblEmailSubjectResource1"></asp:Label>
    <asp:Label ID="lblEmailContent1" runat="server" Text="您好:" Visible="False" meta:resourcekey="lblEmailContent1Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent2" runat="server" Text="此封信件為通知您參與了『<a href='{0}'>{1}</a>』，並完成報名。" Visible="False" meta:resourcekey="lblEmailContent2Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent3" runat="server" Text="相關報名資訊，可以至網站『<a href='{0}'>我的活動</a>』頁面中查看！" Visible="False" meta:resourcekey="lblEmailContent3Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent4" runat="server" Text="如果有任何問題請聯絡活動單位負責人，謝謝。" Visible="False" meta:resourcekey="lblEmailContent4Resource1"></asp:Label>
    <asp:Label ID="lblEmailContent5" runat="server" Text="※此信件為系統發送通知使用，請勿直接回覆。" Visible="False" meta:resourcekey="lblEmailContent5Resource1"></asp:Label>

    <asp:Label ID="lblMsgText" runat="server" Text="訊息" meta:resourcekey="lblMsgTextResource1" Style="display: none;"></asp:Label>
    <asp:Label ID="lblWarningText" runat="server" Text="警告" meta:resourcekey="lblWarningTextResource1" Style="display: none;"></asp:Label>
</asp:Content>

