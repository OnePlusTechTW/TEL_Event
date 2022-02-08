<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Event.master" AutoEventWireup="true" CodeFile="Event_RegisterModel4_Create.aspx.cs" Inherits="Event_Event_RegisterModel4_Create" StylesheetTheme="Event" Culture="auto" UICulture="auto" %>

<%@ Register Src="~/Event/UserControl/UC_EventDescription.ascx" TagPrefix="uc1" TagName="UC_EventDescription" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:UC_EventDescription runat="server" ID="UC_EventDescription" />
    Event_RegisterModel4_Create
</asp:Content>

