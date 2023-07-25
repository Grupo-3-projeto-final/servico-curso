<%@ Page Title="Cursos" Language="C#" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="servico_curso.Cursos" %>
<!DOCTYPE html>

<html lang="pt-br">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %>- My ASP.NET Application</title>


</head>
<body>
    <h1><%: Page.Title %></h1>
    <form runat="server">
        <asp:GridView ID="GridViewCursos" runat="server" AutoGenerateColumns="False" Width="1064px">
            <Columns>
                <asp:BoundField DataField="CodigoCurso" HeaderText="Código do Curso" Visible="False"/>
                <asp:BoundField DataField="NomeCurso" HeaderText="Nome do Curso" />
                <asp:BoundField DataField="DescricaoCurso" HeaderText="Descrição" />
                <asp:BoundField DataField="CodigoPrecoCurso" HeaderText="Código do Preço" Visible="False" />
                <asp:BoundField DataField="ValorCurso" HeaderText="Preço" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="InsertButton" runat="server" Text="Inserir" Width="150px" OnClick="InsertButton_Click" />
        <asp:Button ID="EditButton" runat="server" Text="Editar" Width="150px" OnClick="EditButton_Click"/>
        <asp:Button ID="DeleteButton" runat="server" Text="Deletar" Width="150px" OnClick="DeleteButton_Click"/>
    </form>
</body>
</html>
