using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace servico_curso.Model
{
    public class Usuario
    {
        private int Id;
        public string Nome;
        private readonly string Email;
        private readonly string Senha;
        public bool UsuarioEncontrado;
        public bool SenhaValidada;

        readonly List<Usuario> UsuariosTeste = new List<Usuario>();

        //usuarioCadastrado

        public Usuario(string email, string senha)
        {
            this.Email = email;
            this.Senha = senha;
        }

        internal string SignIn()
        {
            dynamic loginRequest = new
            {
                Email = this.Email,
                Password = this.Senha
            };
            string json = JsonConvert.SerializeObject(loginRequest);
            var apiUrl = $"{ConfigurationManager.AppSettings["URLIdentityServer"]}/login";
            try
            {
                HttpClient httpClient = new HttpClient();

                var taskApi = httpClient.PostAsync(apiUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                taskApi.Wait();
                var response = taskApi.Result;

                if (response.IsSuccessStatusCode)
                {

                    var taskReader = response.Content.ReadAsStringAsync();
                    taskReader.Wait();

                    string conteudo = taskReader.Result;

                    var respostaJson = JsonConvert.DeserializeAnonymousType(conteudo, new { token = "" });

                    return respostaJson.token;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
