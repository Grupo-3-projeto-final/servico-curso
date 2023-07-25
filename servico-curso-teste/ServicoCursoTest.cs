
using Moq;
using servico_curso.Model;
using Servico_Curso.Controller;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class CursosControllerTests
{
    [Fact]
    public async Task DeveRetornarOKEListaDeCursos()
    {
        // Arrange
        Mock<CursoModel> mockCursoModel = new Mock<CursoModel>();

        bool ativo = true;
        List<CursoModel> cursosMock = new List<CursoModel>
        {
            new CursoModel
            {
                CodigoCurso = 1,
                NomeCurso = "Curso 1",
                DescricaoCurso = "Descrição do Curso 1",
                CodigoPrecoCurso = 1001,
                ValorCurso = 99.99f,
                Ativo = true
            },
        };

        mockCursoModel.Setup(x => x.BuscarCursos(ativo)).Returns(Task.FromResult(cursosMock));

        CursosController controller = new CursosController(mockCursoModel.Object);

        // Act
        HttpResponseMessage response = await controller.BuscarCursos(ativo);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        List<CursoModel> cursosResult = await response.Content.ReadAsAsync<List<CursoModel>>();
        Assert.Equal(cursosMock.Count, cursosResult.Count);
    }

    [Fact]
    public async Task DeveRetornarOKEEncontrarCurso()
    {
        // Arrange
        Mock<CursoModel> mockCursoModel = new Mock<CursoModel>();

        int codigoCurso = 1;
        CursoModel cursoMock = new CursoModel
        {
            CodigoCurso = 1,
            NomeCurso = "Curso 1",
            DescricaoCurso = "Descrição do Curso 1",
            CodigoPrecoCurso = 1001,
            ValorCurso = 99.99f,
            Ativo = true
        };

        mockCursoModel.Setup(x => x.BuscarCurso(codigoCurso)).Returns(Task.FromResult(cursoMock));

        CursosController controller = new CursosController(mockCursoModel.Object);

        // Act
        HttpResponseMessage response = await controller.BuscarCurso(codigoCurso);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        CursoModel cursoResult = await response.Content.ReadAsAsync<CursoModel>();
        Assert.Equal(cursoMock.CodigoCurso, cursoResult.CodigoCurso);
        Assert.Equal(cursoMock.NomeCurso, cursoResult.NomeCurso);
    }
    [Fact]
    public async Task DeveRetornarNotFoundEListaVazia()
    {
        // Arrange
        Mock<CursoModel> mockCursoModel = new Mock<CursoModel>();
        bool ativo = true;
        List<CursoModel> cursosMock = new List<CursoModel>();
        mockCursoModel.Setup(x => x.BuscarCursos(ativo)).Returns(Task.FromResult(cursosMock));

        CursosController controller = new CursosController(mockCursoModel.Object);

        // Act
        HttpResponseMessage response = await controller.BuscarCursos(ativo);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task DeveRetornarNotFoundENaoEncontrarCurso()
    {
        // Arrange
        Mock<CursoModel> mockCursoModel = new Mock<CursoModel>();
        int codigoCurso = 1;
        mockCursoModel.Setup(x => x.BuscarCurso(codigoCurso)).Returns(Task.FromResult<CursoModel>(null));

        CursosController controller = new CursosController(mockCursoModel.Object);

        // Act
        HttpResponseMessage response = await controller.BuscarCurso(codigoCurso);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    [Fact]
    public async Task TestBuscarCursos_WhenExceptionOccurs_ReturnsInternalServerError()
    {
        // Arrange
        Mock<CursoModel> mockCursoModel = new Mock<CursoModel>();
        bool ativo = true;

        mockCursoModel.Setup(x => x.BuscarCursos(ativo)).Throws(new Exception("Erro de teste"));

        CursosController controller = new CursosController(mockCursoModel.Object);

        // Act
        HttpResponseMessage response = await controller.BuscarCursos(ativo);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task TestBuscarCurso_WhenExceptionOccurs_ReturnsInternalServerError()
    {
        // Arrange
        Mock<CursoModel> mockCursoModel = new Mock<CursoModel>();
        int codigoCurso = 1;

        mockCursoModel.Setup(x => x.BuscarCurso(codigoCurso)).Throws(new Exception("Erro de teste"));

        CursosController controller = new CursosController(mockCursoModel.Object);

        // Act
        HttpResponseMessage response = await controller.BuscarCurso(codigoCurso);

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}