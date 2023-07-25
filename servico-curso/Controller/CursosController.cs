

using servico_curso.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
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
        public async Task<HttpResponseMessage> BuscarCursos(bool? ativo = null)
        {
            try
            {
                List<CursoModel> cursos = await CursoModel.BuscarCursos(ativo);
                if (cursos == null && cursos.Count == 0)
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
                CursoModel curso = await CursoModel.BuscarCurso(id);
                
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