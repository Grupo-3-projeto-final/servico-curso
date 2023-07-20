using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using servico_curso.Model;
using servico_curso.Repository.Interface;
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
        protected ILogger _logger;
        protected IConfiguration _configuration;
        public CursoRepository( ILogger<CursoRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        protected IDbConnection GetConnection()
        {
            try
            {
                IDbConnection conection = new NpgsqlConnection(_configuration.GetConnectionString("PgsqlDatabase"));
                
                return conection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<CursoModel>> BuscarListaCursos()
        {
            throw new NotImplementedException();
        }
    }
}