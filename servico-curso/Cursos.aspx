<%@ Page Title="Cursos" Language="C#" AutoEventWireup="true" CodeBehind="Cursos.aspx.cs" Inherits="servico_curso.Cursos" %>

<%--<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>Your application description page.</h3>
        <p>Use this area to provide additional information.</p>
    </main>
</asp:Content>--%>
<!DOCTYPE html>

<html lang="pt-br">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %> - My ASP.NET Application</title>

    <%--<asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>--%>

</head>
<body>
    <h1><%: Page.Title %></h1>
    <form runat="server">
        <asp:GridView ID="GridViewCursos" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="CodigoCurso" HeaderText="Código do Curso" />
                <asp:BoundField DataField="NomeCurso" HeaderText="Nome do Curso" />
                <asp:BoundField DataField="DescricaoCurso" HeaderText="Descrição" />
                <asp:BoundField DataField="CodigoPrecoCurso" HeaderText="Código do Preço" />
                <asp:BoundField DataField="ValorCurso" HeaderText="Preço" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
