<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="crudWebForm.aspx.cs" Inherits="WebApp.App_Start.App_Pages.crudWebForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="usernameLabel" runat="server" Text="用户名"></asp:Label>
    <asp:TextBox ID="nameTextBox" runat="server" Height="22px" Width="232px"></asp:TextBox>
    <br />
    <asp:Label ID="userageLabel" runat="server" Text="用户年龄"></asp:Label>
    <asp:TextBox ID="ageTextBox" runat="server" OnTextChanged="ageTextBox_TextChanged" Width="123px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" Text="Add" />
    <asp:Button ID="BtnDel" runat="server" OnClick="BtnDel_Click" Text="Del by username and userage" />
    <asp:Button ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" Text="edit user age" />
    <asp:Button ID="BtnSelect" runat="server" OnClick="BtnSelect_Click" Text="Query by user name" Width="100px" />
    <br />
    <br />
</asp:Content>
