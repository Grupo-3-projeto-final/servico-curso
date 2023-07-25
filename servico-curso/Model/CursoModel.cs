using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace servico_curso.Model
{
    public class CursoModel
    {
        public int CodigoCurso { get; set; }
        public string NomeCurso { get; set; }
        public string DescricaoCurso { get; set; }
        public int CodigoPrecoCurso { get; set; }
        public float ValorCurso { get; set; }
        public bool Ativo { get; set; }
        private static readonly string connectionString =
            System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public async Task<List<CursoModel>> BuscarCursos(bool? ativo)
        {
            CursoModel curso = new CursoModel();
            List<CursoModel> cursos = new List<CursoModel>();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"SELECT CUR.cod_curso CodigoCurso, 
                                      CUR.NOME_CURSO NomeCurso,
                                      CUR.descricao_curso DescricaoCurso,
                                      PCU.cod_preco_curso CodigoPrecoCurso,
                                      PCU.valor_curso ValorCurso,
                                      CUR.ativo Ativo
                                 FROM CURSO CUR
                                 JOIN PRECO_CURSO PCU ON CUR.cod_curso = PCU.cod_curso";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                curso = new CursoModel
                                {
                                    CodigoCurso = Convert.ToInt32(reader["CodigoCurso"]),
                                    NomeCurso = reader["NomeCurso"].ToString(),
                                    DescricaoCurso = reader["DescricaoCurso"].ToString(),
                                    CodigoPrecoCurso = Convert.ToInt32(reader["CodigoPrecoCurso"]),
                                    ValorCurso = Convert.ToSingle(reader["ValorCurso"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };

                                cursos.Add(curso);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }

            if (ativo.HasValue)
            {
                cursos = cursos.Where(x => x.Ativo == ativo.Value).ToList();
            }

            return await Task.FromResult(cursos);
        }
        public async Task<CursoModel> BuscarCurso(int codigoCurso)
        {
            if (codigoCurso <= 0)
                return null;

            CursoModel curso = new CursoModel();

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"SELECT CUR.cod_curso CodigoCurso, 
                                      CUR.NOME_CURSO NomeCurso,
                                      CUR.descricao_curso DescricaoCurso,
                                      PCU.cod_preco_curso CodigoPrecoCurso,
                                      PCU.valor_curso ValorCurso,
                                      Cur.ativo Ativo
                                 FROM CURSO CUR
                                 JOIN PRECO_CURSO PCU ON CUR.cod_curso = PCU.cod_curso
                                 WHERE CUR.COD_CURSO = @CodigoCurso";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CodigoCurso", codigoCurso);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                curso = new CursoModel
                                {
                                    CodigoCurso = Convert.ToInt32(reader["CodigoCurso"]),
                                    NomeCurso = reader["NomeCurso"].ToString(),
                                    DescricaoCurso = reader["DescricaoCurso"].ToString(),
                                    CodigoPrecoCurso = Convert.ToInt32(reader["CodigoPrecoCurso"]),
                                    ValorCurso = Convert.ToSingle(reader["ValorCurso"]),
                                    Ativo = Convert.ToBoolean(reader["Ativo"])
                                };
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                // Tratar exceções, se necessário
                Console.WriteLine("Erro: " + ex.Message);
            }

            return await Task.FromResult(curso);
        }
    }
}