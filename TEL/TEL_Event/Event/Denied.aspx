<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Denied.master" AutoEventWireup="true" CodeFile="Denied.aspx.cs" Inherits="Event_Denied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%; height: 150px">
        <tr>
            <td></td>
        </tr>
    </table>
    <table style="width: 100%; height: 250px; background-color: rgb(162, 209, 229);" cellpadding="0" cellspacing="0">
        <tr">
            <td style="width:200px"></td>
            <td><asp:Image runat="server" ImageUrl="~/Master/images/Denied.png" style="height:250px"></asp:Image></td>
        </tr>
    </table>
</asp:Content>

