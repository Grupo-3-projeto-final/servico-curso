﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="servico_curso._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div>
            <h2>Login</h2>
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Usuário"/><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Senha" /><br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
        </div>
    </main>

</asp:Content>
