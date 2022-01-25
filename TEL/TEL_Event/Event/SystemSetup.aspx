<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="SystemSetup.aspx.cs" StylesheetTheme="Event" Inherits="Event_SystemSetup" Culture="auto" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .ddlColor1 {
            background-color: #00A9E0 !important;
        }

        .ddlColor2 {
            background-color: #71C5E8 !important;
        }

        .ddlColor3 {
            background-color: #00629B !important;
        }

        .ddlColor4 {
            background-color: #78BE20 !important;
        }

        .ddlColor5 {
            background-color: #B7DD79 !important;
        }

        .ddlColor6 {
            background-color: #658D1B !important;
        }

        .ddlColor7 {
            background-color: #DA1884 !important;
        }

        .ddlColor8 {
            background-color: #F395C7 !important;
        }

        .ddlColor9 {
            background-color: #A50050 !important;
        }

        .ddlColor10 {
            background-color: #00B2A9 !important;
        }

        .ddlColor11 {
            background-color: #9CDBD9 !important;
        }

        .ddlColor12 {
            background-color: #007367 !important;
        }

        .ddlColor13 {
            background-color: #8031A7 !important;
        }

        .ddlColor14 {
            background-color: #CAA2DD !important;
        }

        .ddlColor15 {
            background-color: #572C5F !important;
        }

        .ddlColor16 {
            background-color: #EEDC00 !important;
        }

        .ddlColor17 {
            background-color: #F0EC74 !important;
        }

        .ddlColor18 {
            background-color: #BBA600 !important;
        }

        .ddlColor19 {
            background-color: #FF6A13 !important;
        }

        .ddlColor20 {
            background-color: #FAAA8D !important;
        }

        .ddlColor21 {
            background-color: #A65523 !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            var preMenuID = $('#<%=hfPreMenu.ClientID%>')[0].value;//上一個點選的menu
            if (preMenuID == "") {
                $("#category").click();
            }
        });


        function Menu(menuid, ispostback) {
            var preMenuID = $('#<%=hfPreMenu.ClientID%>')[0].value;//上一個點選的menu
            if (menuid == preMenuID) return;

            var menu = $("#" + menuid);
            var preMenu;

            if (preMenuID != "") {
                preMenu= $("#" + preMenuID);
            }

            if (ispostback == true) {
                if (preMenuID !="") {
                    //preMenu.style.background = "rgba(0, 0, 0, 0)";
                    preMenu[0].style.color = "#8031A7";

                    $("#" + preMenu[0].id + "Content").attr("style", "display:block;");
                }
            }
            else {


                var c = menu.css("background-color");

                //var c = window.getComputedStyle(a).backgroundColor;
                if (c === "rgba(0, 0, 0, 0)") {
                    //a.style.background = "#8031A7";
                    menu[0].style.color = "#8031A7";
                    if (preMenuID != "") {
                        //preMenu.style.background = "rgba(0, 0, 0, 0)";
                        preMenu[0].style.color = "#000000";
                    }

                    switch (menu[0].id) {
                        case 'category':
                            $("#" + menu[0].id + "Content").attr("style", "display:block;");
                            break;
                        case 'eventManager':
                            $("#" + menu[0].id + "Content").attr("style", "display:block;");
                            break;
                        case 'mailGroup':
                            $("#" + menu[0].id + "Content").attr("style", "display:block;");
                            break;
                        case 'checkup':
                            $("#" + menu[0].id + "Content").attr("style", "display:block;");
                            break;
                        default:
                    }

                    if (preMenuID != "") {
                        switch (preMenu[0].id) {
                            case 'category':
                                $("#" + preMenu[0].id + "Content").attr("style", "display:none;");
                                break;
                            case 'eventManager':
                                $("#" + preMenu[0].id + "Content").attr("style", "display:none;");
                                break;
                            case 'mailGroup':
                                $("#" + preMenu[0].id + "Content").attr("style", "display:none;");
                                break;
                            case 'checkup':
                                $("#" + preMenu[0].id + "Content").attr("style", "display:none;");
                                break;
                            default:
                        }
                    }

                    $('#<%=hfPreMenu.ClientID%>').val(menuid);
                }
            }

        }

        var preColor;
        function ddlCategoryColorOnChange(e) {
            var color = $("#ContentPlaceHolder1_ddlCategoryColor")[0].value;
            switch (preColor) {
                case '00A9E0':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor1');
                    break;
                case '71C5E8':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor2');
                    break;
                case '00629B':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor3');
                    break;
                case '78BE20':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor4');
                    break;
                case 'B7DD79':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor5');
                    break;
                case '658D1B':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor6');
                    break;
                case 'DA1884':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor7');
                    break;
                case 'F395C7':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor8');
                    break;
                case 'A50050':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor9');
                    break;
                case '00B2A9':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor10');
                    break;
                case '9CDBD9':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor11');
                    break;
                case '007367':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor12');
                    break;
                case '8031A7':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor13');
                    break;
                case 'CAA2DD':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor14');
                    break;
                case '572C5F':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor15');
                    break;
                case 'EEDC00':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor16');
                    break;
                case 'F0EC74':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor17');
                    break
                case 'BBA600':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor18');
                    break;
                case 'FF6A13':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor19');
                    break;
                case 'FAAA8D':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor20');
                    break;
                case 'A65523':
                    $("#ContentPlaceHolder1_ddlCategoryColor").removeClass('ddlColor21');
                    break;
                default:
            }

            switch (color) {
                case '00A9E0':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor1');
                    break;
                case '71C5E8':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor2');
                    break;
                case '00629B':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor3');
                    break;
                case '78BE20':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor4');
                    break;
                case 'B7DD79':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor5');
                    break;
                case '658D1B':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor6');
                    break;
                case 'DA1884':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor7');
                    break;
                case 'F395C7':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor8');
                    break;
                case 'A50050':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor9');
                    break;
                case '00B2A9':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor10');
                    break;
                case '9CDBD9':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor11');
                    break;
                case '007367':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor12');
                    break;
                case '8031A7':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor13');
                    break;
                case 'CAA2DD':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor14');
                    break;
                case '572C5F':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor15');
                    break
                case 'EEDC00':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor16');
                    break
                case 'F0EC74':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor17');
                    break
                case 'BBA600':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor18');
                    break
                case 'FF6A13':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor19');
                    break
                case 'FAAA8D':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor20');
                    break
                case 'A65523':
                    $("#ContentPlaceHolder1_ddlCategoryColor").addClass('ddlColor21');
                    break
                default:
            }

            preColor = color;
            return false;
        }

        function ShowDialogRequired(fieldName) {
            switch (fieldName) {
                case 'CategoryName':
                    $('#<%= lblFiledName.ClientID %>').text($('#<%=lblCategoryName.ClientID%>')[0].textContent);
                    break;
                case 'Empid':
                    $('#<%= lblFiledName.ClientID %>').text($('#<%=lblEmpid.ClientID%>')[0].textContent);
                    break;
                case 'MailGroup':
                    $('#<%= lblFiledName.ClientID %>').text($('#<%=lblMailGroup.ClientID%>')[0].textContent);
                    break;
                default:
            }

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
                    }
                });
            });

        };

        function ShowDialogSuccess() {
            $(function () {
                $("#dialogSuccess").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel2").style.display = "block";
                    }
                });
            });

        };

        function ShowDialogSuccessReload(event) {
            $(function () {
                $("#dialogSuccess").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    buttons: {
                        Close: function () {
                            switch (event) {
                                case 'SuccessCategory':
                                    <%= btnReloadCategoryGrid.ClientID%>.click();

                                    break;
                                case 'SuccessManager':
                                    <%= btnReloadManagerGrid.ClientID%>.click();
                                    break;
                                case 'SuccessMailGroup':
                                    <%= btnReloadMailGroupGrid.ClientID%>.click();
                                    break;
                                default:
                            }

                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel2").style.display = "block";
                    }
                });
            });

        };

        function ShowDialogFailed(ErrMsg) {
            $(function () {
                $("#dialogFailed").dialog({
                    title: $('#<%=hfWarning.ClientID%>')[0].value,
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
        function ShowDialogEmpidErr() {
            $(function () {
                $("#dialogEmpidErr").dialog({
                    title: $('#<%=hfWarning.ClientID%>')[0].value,
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel4").style.display = "block";
                    }
                });
            });

        };

        function ShowDialogExist(fieldName) {
            switch (fieldName) {
                case 'CategoryName':
                    $('#<%= lblExistFiled.ClientID %>').text($('#<%=hfEventCategory.ClientID%>')[0].value);
                    break;
                case 'Empid':
                    $('#<%= lblExistFiled.ClientID %>').text($('#<%=hfEventAdmin.ClientID%>')[0].value);
                    break;
                case 'MailGroup':
                    $('#<%= lblExistFiled.ClientID %>').text($('#<%=hfMailGroup.ClientID%>')[0].value);
                    break;
                default:
            }

            $(function () {
                $("#dialogExist").dialog({
                    title: $('#<%=hfWarning.ClientID%>')[0].value,
                    modal: true,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    open: function (event, ui) {
                        //打開dialog時，顯示panel
                        document.getElementById("ContentPlaceHolder1_ContentPanel5").style.display = "block";
                    }
                });
            });

        };

        function ShowDialogDelete(event, id) {
            $(function () {
                $("#dialogDelete").dialog({
                    title: $('#<%=hfmsg.ClientID%>')[0].value,
                    modal: true,
                    buttons: [
                        {
                            text: "確定",
                            click: function () {
                                onDelete(event, id);
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

        function ShowDialogFileUpload(event, id) {
            $(function () {
                var dialog = $("#dialogFileUpload").dialog({
                    title:"",
                    modal: true,
                    buttons: [
                        {
                            text: "關閉",
                            click: function () {
                                $(this).dialog("close");
                            }
                        }
                    ],
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

        function onDelete(event, id) {
            switch (event) {
                case 'Category':
                    PageMethods.DeleteCategory(id, Success, Failure);
                    break;
                case 'Manager':
                    PageMethods.DeleteManager(id, Success, Failure);
                    break;
                case 'MailGroup':
                    PageMethods.DeleteMailGroup(id, Success, Failure);
                    break;
                default:
            }
        }

        function Success(result) {
            ShowDialogSuccessReload(result);
        }

        function Failure(error) {
            ShowDialogFailed();
        } 
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <table>
        <tr>
            <td>
                <asp:Image runat="server" ImageUrl="~/Master/images/Page_icon5.png" Height="40px"></asp:Image>
            </td>
            <td style="width: 5px"></td>
            <td style="border-bottom: 1.5px solid #19b1e5;">
                <asp:Label ID="LABEL1" runat="server" CssClass="PageTitle" Text="管理網站"></asp:Label>
            </td>
        </tr>
        <tr style="height: 10px">
            <td></td>
        </tr>
    </table>
    <div style="text-align: center; height: 45px; margin-bottom: 10px;">
        <div id="category" style="float: left; padding: 0px 5px; border-right: 2px solid lightgray; min-height: 45px; min-width: 100px; padding: 10px 0px;" onclick="Menu('category')">
            <asp:Label ID="lblCategory" runat="server" CssClass="NormalBoldNoColor" Text="活動分類"></asp:Label>
        </div>
        <div id="eventManager" style="float: left; padding: 0px 5px; border-right: 2px solid lightgray; min-height: 45px; min-width: 100px" onclick="Menu('eventManager')">
            <div>
                <asp:Label ID="lblEventManager1" runat="server" CssClass="NormalBoldNoColor" Text="常態活動"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblEventManager2" runat="server" CssClass="NormalBoldNoColor" Text="管理者"></asp:Label>
            </div>
        </div>
        <div id="mailGroup" style="float: left; padding: 0px 5px; border-right: 2px solid lightgray; min-height: 45px; min-width: 100px; padding: 10px 0px;" onclick="Menu('mailGroup')">
            <asp:Label ID="lblMenuMailGroup" runat="server" CssClass="NormalBoldNoColor" Text="郵件群組"></asp:Label>
        </div>
        <div id="checkup" style="float: left; padding: 0px 5px; min-height: 45px; min-width: 100px" onclick="Menu('checkup')">
            <div>
                <asp:Label ID="lblCheckup1" runat="server" CssClass="NormalBoldNoColor" Text="員工健檢"></asp:Label>
            </div>
            <div>
                <asp:Label ID="lblCheckup2" runat="server" CssClass="NormalBoldNoColor" Text="報名組別"></asp:Label>
            </div>
        </div>
    </div>

    <div id="categoryContent" style="display: none;">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCategoryName" runat="server" Text="分類名稱"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblCategoryColor" runat="server" Text="分類顏色"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblIsEnableCategory" runat="server" Text="是否啟用"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbCategoryName" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategoryColor" runat="server" Width="100px" CssClass="QueryField" onchange="ddlCategoryColorOnChange(this);">
                        <asp:ListItem Selected="True" Text="" Value="" style="display: none;"></asp:ListItem>

                        <asp:ListItem Text="" Value="00A9E0" style="background-color: #00A9E0"></asp:ListItem>
                        <asp:ListItem Text="" Value="71C5E8" style="background-color: #71C5E8"></asp:ListItem>
                        <asp:ListItem Text="" Value="00629B" style="background-color: #00629B"></asp:ListItem>

                        <asp:ListItem Text="" Value="78BE20" style="background-color: #78BE20"></asp:ListItem>
                        <asp:ListItem Text="" Value="B7DD79" style="background-color: #B7DD79"></asp:ListItem>
                        <asp:ListItem Text="" Value="658D1B" style="background-color: #658D1B"></asp:ListItem>

                        <asp:ListItem Text="" Value="DA1884" style="background-color: #DA1884"></asp:ListItem>
                        <asp:ListItem Text="" Value="F395C7" style="background-color: #F395C7"></asp:ListItem>
                        <asp:ListItem Text="" Value="A50050" style="background-color: #A50050"></asp:ListItem>

                        <asp:ListItem Text="" Value="00B2A9" style="background-color: #00B2A9"></asp:ListItem>
                        <asp:ListItem Text="" Value="9CDBD9" style="background-color: #9CDBD9"></asp:ListItem>
                        <asp:ListItem Text="" Value="007367" style="background-color: #007367"></asp:ListItem>

                        <asp:ListItem Text="" Value="8031A7" style="background-color: #8031A7"></asp:ListItem>
                        <asp:ListItem Text="" Value="CAA2DD" style="background-color: #CAA2DD"></asp:ListItem>
                        <asp:ListItem Text="" Value="572C5F" style="background-color: #572C5F"></asp:ListItem>

                        <asp:ListItem Text="" Value="EEDC00" style="background-color: #EEDC00"></asp:ListItem>
                        <asp:ListItem Text="" Value="F0EC74" style="background-color: #F0EC74"></asp:ListItem>
                        <asp:ListItem Text="" Value="BBA600" style="background-color: #BBA600"></asp:ListItem>

                        <asp:ListItem Text="" Value="FF6A13" style="background-color: #FF6A13"></asp:ListItem>
                        <asp:ListItem Text="" Value="FAAA8D" style="background-color: #FAAA8D"></asp:ListItem>
                        <asp:ListItem Text="" Value="A65523" style="background-color: #A65523"></asp:ListItem>

                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ddlIsEnableCategory" runat="server" Width="100px" CssClass="QueryField">
                        <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    <asp:Button ID="Button_AddCategory" runat="server" Text="新增分類" CssClass="Button" OnClick="Button_AddCategory_Click" /></td>
            </tr>
        </table>
        <table cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gridEventCategory" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                        EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                        PageSize="20" OnRowDataBound="gridEventCategory_RowDataBound" OnPageIndexChanging="gridEventCategory_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="活動分類" DataField="name">
                                <HeaderStyle Width="250px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="活動顏色">
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:DropDownList ID="gridDdlCategoryColor" runat="server" Width="100px" CssClass="QueryField" AutoPostBack="true" OnSelectedIndexChanged="gridDdlCategoryColor_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Text="" Value="" style="display: none;"></asp:ListItem>

                                        <asp:ListItem Text="" Value="00A9E0" style="background-color: #00A9E0"></asp:ListItem>
                                        <asp:ListItem Text="" Value="71C5E8" style="background-color: #71C5E8"></asp:ListItem>
                                        <asp:ListItem Text="" Value="00629B" style="background-color: #00629B"></asp:ListItem>

                                        <asp:ListItem Text="" Value="78BE20" style="background-color: #78BE20"></asp:ListItem>
                                        <asp:ListItem Text="" Value="B7DD79" style="background-color: #B7DD79"></asp:ListItem>
                                        <asp:ListItem Text="" Value="658D1B" style="background-color: #658D1B"></asp:ListItem>

                                        <asp:ListItem Text="" Value="DA1884" style="background-color: #DA1884"></asp:ListItem>
                                        <asp:ListItem Text="" Value="F395C7" style="background-color: #F395C7"></asp:ListItem>
                                        <asp:ListItem Text="" Value="A50050" style="background-color: #A50050"></asp:ListItem>

                                        <asp:ListItem Text="" Value="00B2A9" style="background-color: #00B2A9"></asp:ListItem>
                                        <asp:ListItem Text="" Value="9CDBD9" style="background-color: #9CDBD9"></asp:ListItem>
                                        <asp:ListItem Text="" Value="007367" style="background-color: #007367"></asp:ListItem>

                                        <asp:ListItem Text="" Value="8031A7" style="background-color: #8031A7"></asp:ListItem>
                                        <asp:ListItem Text="" Value="CAA2DD" style="background-color: #CAA2DD"></asp:ListItem>
                                        <asp:ListItem Text="" Value="572C5F" style="background-color: #572C5F"></asp:ListItem>

                                        <asp:ListItem Text="" Value="EEDC00" style="background-color: #EEDC00"></asp:ListItem>
                                        <asp:ListItem Text="" Value="F0EC74" style="background-color: #F0EC74"></asp:ListItem>
                                        <asp:ListItem Text="" Value="BBA600" style="background-color: #BBA600"></asp:ListItem>

                                        <asp:ListItem Text="" Value="FF6A13" style="background-color: #FF6A13"></asp:ListItem>
                                        <asp:ListItem Text="" Value="FAAA8D" style="background-color: #FAAA8D"></asp:ListItem>
                                        <asp:ListItem Text="" Value="A65523" style="background-color: #A65523"></asp:ListItem>

                                    </asp:DropDownList>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否啟用">
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:DropDownList ID="gridDdlIsEnableCategory" runat="server" Width="100px" CssClass="QueryField">
                                        <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                    </asp:DropDownList></td>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="Button_SaveCategory" runat="server" Text="儲存" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>' OnClick="Button_SaveCategory_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="Button_DeleteCategory" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>' 
                                        OnClientClick='<%# "ShowDialogDelete(\"Category\",\""+ Eval("id") + "\");return false;" %>' />
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
    </div>
    <div id="eventManagerContent" style="display: none;">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblEmpid" runat="server" Text="工號"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbEmpid" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnAddManager" runat="server" Text="新增管理者" CssClass="Button" OnClick="btnAddManager_Click" />
                </td>
            </tr>
        </table>
        <table cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gridEventManager" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                        EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                        PageSize="20" OnRowDataBound="gridEventManager_RowDataBound" OnPageIndexChanging="gridEventManager_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="工號" DataField="empid">
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="姓名" DataField="name">
                                <HeaderStyle Width="100px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="">
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="Button_DeleteManager" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("empid") %>' 
                                        OnClientClick='<%# "ShowDialogDelete(\"Manager\",\""+ Eval("empid") + "\");return false;" %>' />
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
    </div>
    <div id="mailGroupContent" style="display: none;">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblMailGroup" runat="server" Text="郵件群組"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblIsEnableMailGroup" runat="server" Text="是否啟用"></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbMailGroup" runat="server" Width="150px" CssClass="QueryField"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlIsEnableMailGroup" runat="server" Width="100px" CssClass="QueryField">
                        <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    <asp:Button ID="btnAddMailGroup" runat="server" Text="新增郵件群組" CssClass="Button" Width="120px" OnClick="Button_AddMailGroup_Click" /></td>
            </tr>
        </table>
        <table cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gridMailGroup" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                        EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                        PageSize="20" OnRowDataBound="gridMailGroup_RowDataBound" OnPageIndexChanging="gridMailGroup_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="郵件群組" DataField="name">
                                <HeaderStyle Width="250px"></HeaderStyle>
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="是否啟用">
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:DropDownList ID="gridDdlIsEnableMailGroup" runat="server" Width="100px" CssClass="QueryField">
                                        <asp:ListItem Text="是" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0"></asp:ListItem>
                                    </asp:DropDownList></td>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="Button_SaveMailGroup" runat="server" Text="儲存" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>' OnClick="Button_SaveMailGroup_Click" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <HeaderStyle Width="80px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Button ID="Button_DeleteMailGroup" runat="server" Text="刪除" CssClass="Button_Gridview" CommandArgument='<%# Eval("id") %>' 
                                         OnClientClick='<%# "ShowDialogDelete(\"MailGroup\",\""+ Eval("id") + "\");return false;" %>'
                                        />
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
    </div>
    <div id="checkupContent" style="display: none;">
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnImportHealthGroup" runat="server"  CssClass="Button" Text="匯入組別" OnClick="btnImportHealthGroup_Click" />
                </td>
                <td>
                    <asp:HyperLink ID="hlnkFileAttachment" runat="server"  Text="匯入格式範例" style="color:blue;" NavigateUrl="~/Sample/Import_HealthGroup.xlsx" />
                </td>
            </tr>
        </table>
        <table cellspacing="0">
            <tr>
                <td>
                    <asp:GridView ID="gridHealthGroup" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AllowPaging="True"
                        EmptyDataText="無符合資料" AutoGenerateColumns="False" BorderColor="White"
                        PageSize="20" OnRowDataBound="gridHealthGroup_RowDataBound" OnPageIndexChanging="gridHealthGroup_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="工號" DataField="empid">
                                <HeaderStyle Width="250px"></HeaderStyle>
                            </asp:BoundField>
                             <asp:BoundField HeaderText="姓名" DataField="name">
                                <HeaderStyle Width="250px"></HeaderStyle>
                            </asp:BoundField>
                             <asp:BoundField HeaderText="健檢報名組別" DataField="groupname">
                                <HeaderStyle Width="250px"></HeaderStyle>
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#595959" ForeColor="White" Font-Names=" Microsoft JhengHei, Georgia" Font-Size="14px" Height="30px" HorizontalAlign="Center"></HeaderStyle>
                        <RowStyle Font-Names=" Microsoft JhengHei, Georgia" Font-Size="12px" Height="25px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>





    <div id="dialogRequired" title="Dialog Title">
        <asp:Panel ID="ContentPanel1" runat="server" Style="display: none">
            <%--dialog content--%>
            <asp:Label ID="lblFiledName" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblRequired" runat="server" Text="為必填欄位。"></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogSuccess" title="Dialog Title">
        <asp:Panel ID="ContentPanel2" runat="server" Style="display: none">
            <%--dialog content--%>
            <asp:Label ID="lblSuccess" runat="server" Text="成功。"></asp:Label>
        </asp:Panel>
    </div>
    <div id="dialogFailed" title="Dialog Title">
        <asp:Panel ID="ContentPanel3" runat="server" Style="display: none">
            <%--dialog content--%>
            <asp:Label ID="lblFailed" runat="server" Text="失敗。"></asp:Label>
            <asp:Label ID="lblErrMsgTxt" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label><br />
            <asp:Label ID="lblErrMsg" runat="server" Text="" Visible="false"></asp:Label><br />
        </asp:Panel>
    </div>
    <div id="dialogEmpidErr" title="Dialog Title">
        <asp:Panel ID="ContentPanel4" runat="server" Style="display: none">
            <%--dialog content--%>
            <asp:Label ID="Label2" runat="server" Text="工號輸入不正確。"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogExist" title="Dialog Title">
        <asp:Panel ID="ContentPanel5" runat="server" Style="display: none">
            <%--dialog content--%>
            <asp:Label ID="lblExistFiled" runat="server" Text=""></asp:Label>
            <asp:Label ID="lblExist" runat="server" Text="已存在。"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogDelete" title="Dialog Title">
        <asp:Panel ID="ContentPanel6" runat="server" Style="display: none">
            <%--dialog content--%>
            <asp:Label ID="lblDeleteWarning" runat="server" Text="確定刪除該筆資料？"></asp:Label>
        </asp:Panel>
    </div>

    <div id="dialogFileUpload" title="Dialog Title">
        <asp:Panel ID="ContentPanel7" runat="server" Style="display: none">
            <%--dialog content--%>
            <div>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div style="margin-top: 20px;">
                <asp:Button ID="btnImport" runat="server" Text="匯入" OnClick="btnImport_Click" />
            </div>
            <div style="margin-top: 5px;">
                <asp:TextBox ID="tbImportMsg" runat="server" TextMode="MultiLine" Height="250px" Width="412px" placeholder="匯入資訊..." ReadOnly="true" ></asp:TextBox>
            </div>
        </asp:Panel>
    </div>

    <asp:Button ID="btnReloadCategoryGrid" runat="server" Text="Button" OnClick="btnReloadCategoryGrid_Click" style="display:none;" />
    <asp:Button ID="btnReloadManagerGrid" runat="server" Text="Button" OnClick="btnReloadManagerGrid_Click" style="display:none;" />
    <asp:Button ID="btnReloadMailGroupGrid" runat="server" Text="Button" OnClick="btnReloadMailGroupGrid_Click" style="display:none;" />

    <asp:Label ID="lblDuplicate" runat="server" Text="以下員工報名健檢組別重複：" Visible="false"></asp:Label>
    <asp:Label ID="lblReimport" runat="server" Text="請重新匯入。" Visible="false"></asp:Label>
    <asp:Label ID="lblImportSuccess" runat="server" Text="匯入成功。" Visible="false"></asp:Label>
    <asp:Label ID="lblImportFailed" runat="server" Text="匯入失敗，請重新匯入。" Visible="false"></asp:Label>
    <asp:Label ID="lblImportFailedMsg" runat="server" Text="錯誤訊息：" Visible="false"></asp:Label>




    <asp:HiddenField ID="hfWarning" runat="server" Value="警告" />
    <asp:HiddenField ID="hfmsg" runat="server" Value="訊息" />
    <asp:HiddenField ID="hfEventCategory" runat="server" Value="活動分類" />
    <asp:HiddenField ID="hfEventAdmin" runat="server" Value="常態活動管理者" />
    <asp:HiddenField ID="hfMailGroup" runat="server" Value="郵件群組" />
    <asp:HiddenField ID="hfPreMenu" runat="server" Value="" />
</asp:Content>


