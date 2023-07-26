using IdentityGama.Authentication;
using IdentityGama.Authorization;
using IdentityGama.Filters;
using Npgsql;
using System;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;

namespace servico_curso
{
    public partial class Cursos : Page
    {
        private const string RolePag = "Administrator";
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarDados();
        }

        private void CarregarDados()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (NpgsqlConnection conection = new NpgsqlConnection(connectionString))
            {
                conection.Open();

                // Exemplo de consulta no banco de dados
                string sql = @"SELECT CUR.cod_curso CodigoCurso, 
                                      CUR.NOME_CURSO NomeCurso,
                                      CUR.descricao_curso DescricaoCurso, 
                                      PCU.cod_preco_curso CodigoPrecoCurso,
                                      PCU.valor_curso ValorCurso
                                 FROM CURSO CUR
                                 JOIN PRECO_CURSO PCU ON CUR.cod_curso = PCU.cod_curso
                                WHERE CUR.ativo
                                ORDER BY NomeCurso";
                using (NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conection))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    GridViewCursos.DataSource = dt;
                    GridViewCursos.DataBind();
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            VerifyAcessPage();
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href = '/Curso?modo=inserir' </script>");
        }
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href = '/Curso?modo=editar' </script>");
        }
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href = '/Curso?modo=deletar' </script>");
        }

        private void VerifyAcessPage()
        {
            HttpCookie cookie = Request.Cookies["token"];
            if (cookie == null)
            {
                Response.StatusCode = 401;
                Response.End();
            }

            var service = new AuthorizationService();
            if (!service.IsAuthorized(cookie.Value, RolePag))
            {
                Response.StatusCode = 403;
                Response.End();
            }
        }

        private void LogoffSite()
        {
            HttpCookie cookie = new HttpCookie("MeuToken");

            // Define a data de expiração do cookie como uma data passada para removê-lo.
            cookie.Expires = DateTime.Now.AddDays(-1);

            // Adiciona o cookie à coleção de cookies da resposta (Response.Cookies).
            Response.Cookies.Add(cookie);

            Response.Write("<script>window.location.href = '/Default' </script>");
        }
    }
}