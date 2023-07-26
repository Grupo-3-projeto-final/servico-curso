using IdentityGama.Filters;
using servico_curso.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Servico_Curso.Controller
{
    [Authentication]
    public class CursosController : ApiController
    {
        private readonly CursoModel _cursoModel = new CursoModel();

        public CursosController() { }

        public CursosController(CursoModel cursoModel)
        {
            this._cursoModel = cursoModel;
        }
        /// <summary>
        /// Busca todos os cursos cadastrados.
        /// </summary>
        /// <param name="ativo">Parâmetro boleano opcional que caso seja passado filtra a lista de cursos ativos ou inativos.</param>
        /// <returns></returns>
        [HttpGet]
        [Authentication]
        public async Task<HttpResponseMessage> BuscarCursos(bool? ativo = null)
        {
            try
            {
                CursoModel curso = _cursoModel;
                List<CursoModel> cursos = await curso.BuscarCursos(ativo);

                if (cursos == null || cursos.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, cursos);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// Busca as informações de um curso com base no Id informado
        /// </summary>
        /// <param name="id">Parâmetro obrigatório a ser informado para realizar a busca por Id.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> BuscarCurso(int id)
        {
            try
            {
                CursoModel curso = _cursoModel;

                curso = await curso.BuscarCurso(id);

                if (curso == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound);

                return Request.CreateResponse(HttpStatusCode.OK, curso);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}