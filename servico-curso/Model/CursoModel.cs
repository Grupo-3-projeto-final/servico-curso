using servico_curso.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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

        public static async Task<List<CursoModel>> BuscarCursos(bool? ativo)
        {
            List<CursoModel> cursos = await CursoRepository.BuscarCursos();

            if (ativo.HasValue)
            {
                cursos = cursos.Where(x=>x.Ativo == ativo.Value).ToList();
            }

            return cursos;
        }
        public static async Task<CursoModel> BuscarCurso(int id)
        {
            return await CursoRepository.BuscarCurso(id);
        }
    }
}