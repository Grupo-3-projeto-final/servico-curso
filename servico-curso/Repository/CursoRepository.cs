using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace servico_curso.Repository
{
    public class CursoRepository : ICursoRepository
    {

        public Task<IEnumerable<int>> BuscarListaCursos()
        {
            throw new NotImplementedException();
        }
    }
}