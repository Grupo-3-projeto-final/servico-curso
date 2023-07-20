using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace servico_curso.Configurations
{
    public abstract class Curso
    {
        public int CodigoCurso { get; set; }
        public string NomeCurso { get; set; }
        public float PrecoCurso { get; set; }
        public bool Ativo { get; set; }
    }
}