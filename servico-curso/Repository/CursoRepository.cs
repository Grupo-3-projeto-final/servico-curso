using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Npgsql;
using servico_curso.Model;

namespace servico_curso.Repository
{
    public static class CursoRepository
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static Task<List<CursoModel>> BuscarCursos()
        {
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
                                CursoModel curso = new CursoModel
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
                // Tratar exceções, se necessário
                Console.WriteLine("Erro: " + ex.Message);
            }

            return Task.FromResult(cursos);
        }
        public static Task<CursoModel> BuscarCurso(int codigoCurso)
        {
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
            return Task.FromResult(curso);
        }
        public static void InserirCurso(CursoModel curso)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"INSERT INTO CURSO (NOME_CURSO, descricao_curso) 
                                    VALUES (@NomeCurso, @DescricaoCurso);
                                   INSERT INTO PRECO_CURSO (cod_curso, valor_curso, ativo) 
                                   VALUES (currval('curso_cod_curso_seq'), @ValorCurso, @Ativo);";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@NomeCurso", curso.NomeCurso);
                        command.Parameters.AddWithValue("@DescricaoCurso", curso.DescricaoCurso);
                        command.Parameters.AddWithValue("@ValorCurso", curso.ValorCurso);
                        command.Parameters.AddWithValue("@Ativo", true); // Defina o valor adequado para a coluna 'ativo'

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções, se necessário
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public static void DesativarCurso(int codigoCurso)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"UPDATE PRECO_CURSO SET ativo = false WHERE cod_curso = @CodigoCurso;
                                   UPDATE CURSO SET ativo = false WHERE cod_curso = @CodigoCurso;";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CodigoCurso", codigoCurso);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções, se necessário
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        public static void AlterarCurso(CursoModel curso)
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = @"UPDATE CURSO 
                                   SET 
                                       descricao_curso = @DescricaoCurso
                                   WHERE cod_curso = @CodigoCurso;

                                   UPDATE PRECO_CURSO
                                   SET valor_curso = @ValorCurso,
                                       ativo = @Ativo
                                   WHERE cod_curso = @CodigoCurso;";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CodigoCurso", curso.CodigoCurso);

                        if (!string.IsNullOrEmpty(curso.NomeCurso))
                        {
                            sql += "NOME_CURSO = @NomeCurso";
                            command.Parameters.AddWithValue("@NomeCurso", curso.NomeCurso);
                        }

                        if (!string.IsNullOrEmpty(curso.DescricaoCurso))
                            command.Parameters.AddWithValue("@DescricaoCurso", curso.DescricaoCurso);

                        if (float.IsPositiveInfinity(curso.ValorCurso))
                            command.Parameters.AddWithValue("@ValorCurso", curso.ValorCurso);


                        command.Parameters.AddWithValue("@Ativo", curso.Ativo);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Tratar exceções, se necessário
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
    }
}
