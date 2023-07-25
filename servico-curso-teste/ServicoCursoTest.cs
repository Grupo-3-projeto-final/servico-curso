using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace servico_curso_teste
{
    [Collection("Curso")]
    public class ServicoCursoTest
    {
        private readonly HttpClient _httpClient;

        public ServicoCursoTest()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44336/Api/");
        }

        [Fact]
        public async Task DeveRetornar200()
        {
            // Arrange
            var rota = "Cursos"; // Defina o nome da rota que você deseja testar

            // Act
            var response = await _httpClient.GetAsync(rota);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}