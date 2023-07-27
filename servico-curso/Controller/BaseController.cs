using IdentityGama.Authentication;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace servico_curso.Controller
{
    public class BaseController : ApiController
    {
        public static bool GetAuthTokenFromRequest()
        {
            if (HttpContext.Current.Request.Headers["Authorization"] != null)
            {
                string authHeader = HttpContext.Current.Request.Headers["Authorization"];

                if (authHeader.StartsWith("Bearer "))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    var _service = new AuthenticationService();
                    if(!_service.IsAuthenticated(token))
                    {
                        return false;
                    }
                    return true;
                }
            }

            return false; 
        }
    }
}