<%@ Page Title="Curso" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Curso.aspx.cs" Inherits="servico_curso.Curso" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">
        <h2><%: Page.Title %></h2>
        <p>
            Nome do curso:
            <asp:TextBox ID="NomeCursoText" runat="server" AutoPostBack="true" Visible="true" OnTextChanged="CamposTextChanged"></asp:TextBox>
            <asp:DropDownList ID="NomeCursoSelect" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="NomeCursoSelect_SelectedIndexChanged">
                <asp:ListItem Text="Selecione um curso..." Value="" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorNome" runat="server"
                ControlToValidate="NomeCursoText"
                ErrorMessage="Campo obrigatório."
                Display="Dynamic"
                ValidationGroup="CursoValidation">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelect" runat="server"
                ControlToValidate="NomeCursoSelect"
                ErrorMessage="Campo obrigatório"
                Display="Dynamic"
                ValidationGroup="CursoValidationSelect">
            </asp:RequiredFieldValidator>
        </p>
        <p>
            Descrição:
            <asp:TextBox ID="DescricaoCurso" runat="server" Width="1290px" AutoPostBack="true" OnTextChanged="CamposTextChanged"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescricao" runat="server"
                ControlToValidate="DescricaoCurso"
                ErrorMessage="Campo obrigatório."
                Display="Dynamic"
                ValidationGroup="CursoValidation">
            </asp:RequiredFieldValidator>
        </p>
        <p>
            Valor Curso:
            <asp:TextBox ID="ValorCurso" runat="server" Width="250px" AutoPostBack="true" OnTextChanged="CamposTextChanged"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                ControlToValidate="ValorCurso"
                ErrorMessage="O valor deve ser um número decimal com no máximo duas casas decimais."
                ValidationExpression="^\d+(\.\d{1,2})?$|^\d+(\,\d{1,2})?$"
                Display="Dynamic"
                ValidationGroup="CursoValidation">
            </asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorValor" runat="server"
                ControlToValidate="ValorCurso"
                ErrorMessage="Campo obrigatório."
                Display="Dynamic"
                ValidationGroup="CursoValidation">
            </asp:RequiredFieldValidator>
        </p>
        <asp:RadioButtonList ID="IndicadorAtivado" runat="server" Visible="false" CssClass="radio-list">
            <asp:ListItem Text="Ativado" Value="true" />
            <asp:ListItem Text="Desativado" Value="false" />
        </asp:RadioButtonList>
        <asp:Button ID="BotaoSalvar" runat="server" Text="Salvar" Visible="false" OnClick="SalvarClick" ValidationGroup="CursoValidation" />
        <asp:Button ID="BotaoEditar" runat="server" Text="Editar" Visible="false" OnClick="EditarClick" ValidationGroup="CursoValidation" />
        <asp:Button ID="BotaoDeletar" runat="server" Text="Deletar" Visible="false" OnClick="DeletarClick" ValidationGroup="CursoValidation" />
        <asp:Button ID="BotaoVoltar" runat="server" Text="Voltar" Visible="true" OnClick="VoltarClick"/>
    </main>
    <style>
        .radio-list label{
            display: inline-block;
            margin-right: 10px;
        }
    </style>
</asp:Content>
