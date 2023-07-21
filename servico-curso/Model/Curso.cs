using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace servico_curso.Model
{
    public class Curso
    {
        public int CodigoCurso { get; set; }
        public string NomeCurso { get; set; }
        public float PrecoCurso { get; set; }
        public bool Ativo { get; set; }

        public static List<Curso> BuscarCursos()
        {
            List<Curso> cursos = new List<Curso>();

            return cursos;
        }
    }
}