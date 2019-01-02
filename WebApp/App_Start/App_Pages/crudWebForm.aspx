<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="crudWebForm.aspx.cs" Inherits="WebApp.App_Start.App_Pages.crudWebForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="nameTextBox" runat="server" Height="28px" Width="367px"></asp:TextBox>
    <br />
    <asp:TextBox ID="ageTextBox" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" Text="Add" />
    <asp:Button ID="BtnDel" runat="server" OnClick="BtnDel_Click" Text="Del" />
    <asp:Button ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" Text="edit" />
    <asp:Button ID="BtnSelect" runat="server" OnClick="BtnSelect_Click" Text="Query" />
    <br />
    <br />
</asp:Content>
