

using servico_curso.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Servico_Curso.Controller
{
    public class CursosController : ApiController
    {
        /// <summary>
        /// Busca todos os cursos cadastrados.
        /// </summary>
        /// <param name="ativo">Parâmetro boleano opcional que caso seja passado filtra a lista de cursos ativos ou inativos.</param>
        /// <returns></returns>
        [HttpGet]
        public Task<List<Curso>> BuscarCursos(bool? ativo = null)
        {
            return Curso.BuscarCursos(ativo);
        }
        /// <summary>
        /// Busca as informações de um curso com base no Id informado
        /// </summary>
        /// <param name="id">Parâmetro obrigatório a ser informado para realizar a busca por Id.</param>
        /// <returns></returns>
        [HttpGet]
        public Task<Curso> BuscarCurso(int id)
        {
            return Curso.BuscarCurso(id);
        }
    }
}