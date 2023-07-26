using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using servico_curso.Model;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace servico_curso
{
    public partial class _Default : Page
    {
        private string Email;
        private string Senha;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            this.Email = txtUsername.Text;
            this.Senha = txtPassword.Text;

            if (Email.IsNullOrWhiteSpace())
            {
                Response.Write("<script>alert('Favor informar um email válido.')</script>");
                return;
            }

            if (Senha.IsNullOrWhiteSpace())
            {
                Response.Write("<script>alert('A senha não pode ser vazia, favor informe a senha.')</script>");
                return;
            }

            Usuario usuario = new Usuario(Email, Senha);

            string token = usuario.SignIn();

            if (token == null)
            {
                Response.Write("<script>alert('Não existe o usuário cadastrado ou a senha está incorreta!.')</script>");
                return;
            }

            HttpCookie MeuCookie = new HttpCookie("token", token);
            Response.Cookies.Add(MeuCookie);

            // redirecionar para tela de cadastro de curso
            Response.Write("<script>window.location.href = '/Cursos' </script>");
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ClearCookie();


        }

        private void ClearCookie()
        {
            HttpCookie cookie = new HttpCookie("token");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
        }


    }
}