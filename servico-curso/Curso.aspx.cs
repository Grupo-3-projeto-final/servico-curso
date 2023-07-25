using Microsoft.Ajax.Utilities;
using Npgsql;
using servico_curso.Model;
using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace servico_curso
{
    public partial class Curso : Page
    {
        private readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            string modo = Request.QueryString["modo"];

            if (!modo.IsNullOrWhiteSpace())
                modo = modo.ToLower();

            if (!IsPostBack && (modo.IsNullOrWhiteSpace() || !modo.Equals("inserir")))
            {
                CarregarListaCursos(modo);
            }
            switch (modo)
            {
                case "inserir":
                    Page.Title = "Inserir Curso";
                    NomeCursoText.Visible = true;
                    NomeCursoSelect.Visible = false;
                    BotaoSalvar.Visible = true;
                    break;
                case "editar":
                    Page.Title = "Editar Curso";
                    NomeCursoText.Visible = false;
                    NomeCursoSelect.Visible = true;
                    BotaoEditar.Visible = true;
                    IndicadorAtivado.Visible = true;
                    DescricaoCurso.Enabled = false;
                    ValorCurso.Enabled = false;
                    IndicadorAtivado.Enabled = false;
                    break;
                case "deletar":
                    Page.Title = "Deletar Curso";
                    NomeCursoText.Visible = false;
                    NomeCursoSelect.Visible = true;
                    DescricaoCurso.Enabled = false;
                    ValorCurso.Enabled = false;
                    BotaoDeletar.Visible = true;
                    break;
                default:
                    //O ideal seria uma pagina de error que redireciona para home.
                    NomeCursoText.Visible = false;
                    NomeCursoSelect.Visible = true;
                    return;
            }
        }

        private void CarregarListaCursos(string cursoModo)
        {
            using (NpgsqlConnection conection = new NpgsqlConnection(connectionString))
            {
                conection.Open();

                // Exemplo de consulta no banco de dados
                string sql = @"SELECT CUR.COD_CURSO CodigoCurso, 
                                      CUR.NOME_CURSO || ' ('|| case when cur.ativo then 'Ativo' else 'Desativado' end || ')' NomeCurso
                                 FROM CURSO CUR";

                if (!cursoModo.IsNullOrWhiteSpace() && cursoModo.Equals("deletar"))
                {
                    sql += " WHERE CUR.ativo = true"; // Para modo "inserir", filtrar apenas cursos ativos
                }

                sql += " Order by NomeCurso";

                using (NpgsqlCommand command = new NpgsqlCommand(sql, conection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        // Limpar os itens existentes do DropDownList
                        NomeCursoSelect.Items.Clear();

                        // Adicionar a opção padrão ao DropDownList
                        NomeCursoSelect.Items.Add(new ListItem("Selecione um curso...", ""));

                        // Preencher o DropDownList com os dados do banco de dados
                        while (reader.Read())
                        {
                            string codigoCurso = reader["CodigoCurso"].ToString();
                            string nomeCurso = reader["NomeCurso"].ToString();


                            // Adicionar cada curso como um novo ListItem ao DropDownList
                            NomeCursoSelect.Items.Add(new ListItem(nomeCurso, codigoCurso));
                        }
                    }
                }
            }

        }
        protected void NomeCursoSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            DescricaoCurso.Enabled = true;
            ValorCurso.Enabled = true;
            IndicadorAtivado.Enabled = true;

            string codigoCursoSelecionado = NomeCursoSelect.SelectedValue;

            if (int.TryParse(NomeCursoSelect.SelectedValue, out int codigoCurso))
            {
                if (string.IsNullOrEmpty(codigoCursoSelecionado))
                {
                    DescricaoCurso.Text = string.Empty;
                    ValorCurso.Text = string.Empty;
                    IndicadorAtivado.ClearSelection();
                }

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                using (NpgsqlConnection conection = new NpgsqlConnection(connectionString))
                {
                    conection.Open();

                    string sql = @"SELECT CUR.cod_curso CodigoCurso, 
                                      CUR.NOME_CURSO NomeCurso,
                                      CUR.descricao_curso DescricaoCurso,
                                      CUR.ATIVO Ativo,
                                      PCU.cod_preco_curso CodigoPrecoCurso,
                                      PCU.valor_curso ValorCurso
                                 FROM CURSO CUR
                                 JOIN PRECO_CURSO PCU ON CUR.cod_curso = PCU.cod_curso
                                WHERE CUR.cod_curso = @CodigoCurso";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, conection))
                    {
                        command.Parameters.AddWithValue("@CodigoCurso", codigoCurso);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Preencher os campos "Descrição" e "Valor Curso" com os valores do curso selecionado
                                DescricaoCurso.Text = reader["DescricaoCurso"].ToString();
                                ValorCurso.Text = reader["ValorCurso"].ToString();
                                bool ativo = Convert.ToBoolean(reader["Ativo"]);
                                IndicadorAtivado.SelectedValue = ativo ? "true" : "false";
                            }
                        }
                    }
                }
            }

        }

        protected void CamposTextChanged(object sender, EventArgs e)
        {
            Validate("CursoValidation");
            BotaoSalvar.Visible = IsValid;
        }
        protected void SalvarClick(object sender, EventArgs e)
        {
            Validate("CursoValidation");

            // Verificar se todos os campos estão preenchidos corretamente e definir a visibilidade do botão
            if (!IsValid)
            {
                BotaoSalvar.Visible = false;
                return;
            }

            CursoModel curso = new CursoModel()
            {
                NomeCurso = NomeCursoText.Text,
                DescricaoCurso = DescricaoCurso.Text,
                ValorCurso = float.Parse(ValorCurso.Text)
            };

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Inserir o curso na tabela CURSO
                string sqlInsertCurso = "INSERT INTO CURSO (NOME_CURSO, descricao_curso) VALUES (@NomeCurso, @DescricaoCurso);";
                using (NpgsqlCommand commandInsertCurso = new NpgsqlCommand(sqlInsertCurso, connection))
                {
                    commandInsertCurso.Parameters.AddWithValue("@NomeCurso", curso.NomeCurso);
                    commandInsertCurso.Parameters.AddWithValue("@DescricaoCurso", curso.DescricaoCurso);
                    commandInsertCurso.ExecuteNonQuery();
                }

                // Obter o ID do curso inserido
                int codigoCurso;
                string sqlSelectLastInsertedId = "SELECT LASTVAL();";
                using (NpgsqlCommand commandSelectLastInsertedId = new NpgsqlCommand(sqlSelectLastInsertedId, connection))
                {
                    codigoCurso = Convert.ToInt32(commandSelectLastInsertedId.ExecuteScalar());
                }

                // Inserir o preço na tabela PRECO_CURSO
                string sqlInsertPrecoCurso = "INSERT INTO PRECO_CURSO (cod_curso, valor_curso, ativo) VALUES (@CodigoCurso, @ValorCurso, @Ativo);";
                using (NpgsqlCommand commandInsertPrecoCurso = new NpgsqlCommand(sqlInsertPrecoCurso, connection))
                {
                    commandInsertPrecoCurso.Parameters.AddWithValue("@CodigoCurso", codigoCurso);
                    commandInsertPrecoCurso.Parameters.AddWithValue("@ValorCurso", curso.ValorCurso);
                    commandInsertPrecoCurso.Parameters.AddWithValue("@Ativo", true); // Aqui você pode definir o valor de ativo, por exemplo, com base no RadioButtonList.

                    commandInsertPrecoCurso.ExecuteNonQuery();
                }
            }

            Response.Write("<script>window.location.href = '/Cursos' </script>");
        }

        protected void EditarClick(object sender, EventArgs e)
        {
            // Verificar se todos os campos estão preenchidos corretamente e definir a visibilidade do botão
            if (NomeCursoSelect.SelectedValue.IsNullOrWhiteSpace() || DescricaoCurso.Text.IsNullOrWhiteSpace()|| ValorCurso.Text.IsNullOrWhiteSpace())
            {
                BotaoEditar.Visible = false;
                return;
            }

            if (int.TryParse(NomeCursoSelect.SelectedValue, out int codigoCurso))
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlUpdateCurso = "UPDATE CURSO SET NOME_CURSO = @NomeCurso, DESCRICAO_CURSO = @DescricaoCurso, ATIVO = @Ativo WHERE cod_curso = @CodigoCurso;";

                    using (NpgsqlCommand commandUpdateCurso = new NpgsqlCommand(sqlUpdateCurso, connection))
                    {
                        string nomeCurso = NomeCursoSelect.Items.FindByValue(NomeCursoSelect.SelectedValue).Text;
                        commandUpdateCurso.Parameters.AddWithValue("@NomeCurso", nomeCurso.Substring(0, nomeCurso.IndexOf('(')).Trim());
                        commandUpdateCurso.Parameters.AddWithValue("@DescricaoCurso", DescricaoCurso.Text);
                        commandUpdateCurso.Parameters.AddWithValue("@CodigoCurso", codigoCurso);
                        commandUpdateCurso.Parameters.AddWithValue("@Ativo", bool.Parse(IndicadorAtivado.SelectedValue));

                        commandUpdateCurso.ExecuteNonQuery();
                    }
                }
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlUpdatePrecoCurso = "UPDATE PRECO_CURSO SET valor_curso = @ValorCurso WHERE cod_curso = @CodigoCurso;";

                    using (NpgsqlCommand commandUpdatePrecoCurso = new NpgsqlCommand(sqlUpdatePrecoCurso, connection))
                    {
                        commandUpdatePrecoCurso.Parameters.AddWithValue("@ValorCurso", float.Parse(ValorCurso.Text));
                        commandUpdatePrecoCurso.Parameters.AddWithValue("@CodigoCurso", codigoCurso);

                        commandUpdatePrecoCurso.ExecuteNonQuery();
                    }
                }
            }
            Response.Write("<script>window.location.href = '/Cursos' </script>");
        }

        protected void DeletarClick(object sender, EventArgs e)
        {

            if (int.TryParse(NomeCursoSelect.SelectedValue, out int codigoCurso))
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlUpdateCurso = "UPDATE CURSO SET ativo = false WHERE cod_curso = @CodigoCurso;";

                    using (NpgsqlCommand commandUpdateCurso = new NpgsqlCommand(sqlUpdateCurso, connection))
                    {
                        commandUpdateCurso.Parameters.AddWithValue("@CodigoCurso", codigoCurso);

                        commandUpdateCurso.ExecuteNonQuery();
                    }
                }
            }
            Response.Write("<script>window.location.href = '/Cursos' </script>");
        }

        protected void VoltarClick(object sender, EventArgs e)
        {
            Response.Write("<script>window.location.href = '/Cursos' </script>");
        }

    }
}