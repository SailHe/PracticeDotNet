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
    <asp:TextBox ID="queryTextBox" runat="server"></asp:TextBox>
    <asp:Button ID="queryButton" runat="server" OnClick="queryButton_Click" Text="查询" />
    <br />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="user_id" DataSourceID="EntityDataSource1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            <asp:BoundField DataField="user_id" HeaderText="user_id" ReadOnly="True" SortExpression="user_id" />
            <asp:BoundField DataField="user_name" HeaderText="user_name" SortExpression="user_name" />
            <asp:BoundField DataField="user_realname" HeaderText="user_realname" SortExpression="user_realname" />
            <asp:BoundField DataField="user_nickname" HeaderText="user_nickname" SortExpression="user_nickname" />
            <asp:BoundField DataField="user_age" HeaderText="user_age" SortExpression="user_age" />
            <asp:BoundField DataField="user_passworld" HeaderText="user_passworld" SortExpression="user_passworld" />
            <asp:BoundField DataField="user_sex" HeaderText="user_sex" SortExpression="user_sex" />
            <asp:BoundField DataField="user_role" HeaderText="user_role" SortExpression="user_role" />
            <asp:BoundField DataField="user_contact_way" HeaderText="user_contact_way" SortExpression="user_contact_way" />
            <asp:BoundField DataField="create_time" HeaderText="create_time" SortExpression="create_time" />
            <asp:BoundField DataField="edit_time" HeaderText="edit_time" SortExpression="edit_time" />
            <asp:BoundField DataField="email" HeaderText="email" SortExpression="email" />
            <asp:BoundField DataField="head_portrait" HeaderText="head_portrait" SortExpression="head_portrait" />
            <asp:BoundField DataField="mobile_no" HeaderText="mobile_no" SortExpression="mobile_no" />
            <asp:BoundField DataField="password" HeaderText="password" SortExpression="password" />
            <asp:BoundField DataField="region" HeaderText="region" SortExpression="region" />
            <asp:BoundField DataField="register_time" HeaderText="register_time" SortExpression="register_time" />
            <asp:BoundField DataField="user_type" HeaderText="user_type" SortExpression="user_type" />
            <asp:BoundField DataField="validity" HeaderText="validity" SortExpression="validity" />
        </Columns>
    </asp:GridView>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=sail_heEntities" DefaultContainerName="sail_heEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="sys_user">
    </asp:EntityDataSource>
    <br />
    <br />
    <br />
</asp:Content>
