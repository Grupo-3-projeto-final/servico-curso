using servico_curso.App_Start;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace servico_curso
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        { // Habilitar o CORS para todas as origens, métodos e cabeçalhos
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");

            // Permitir que os cookies sejam enviados nas solicitações de origem cruzada (opcional)
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Credentials", "true");
            // Se for uma requisição OPTIONS, definir um código de status de sucesso (200) e encerrar a resposta
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.StatusCode = 200; HttpContext.Current.Response.End();
            }
        }
    }
}