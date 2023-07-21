using Microsoft.Ajax.Utilities;
using servico_curso.Model;
using Servico_Curso.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                return ;
            }

            if (Senha.IsNullOrWhiteSpace())
            {
                Response.Write("<script>alert('A senha não pode ser vazia, favor informe a senha.')</script>");
                return;
            }

            Usuario usuario = new Usuario(Email, Senha);

            usuario = usuario.CarregarDadosUsuario();

            if (usuario == null)
            {
                Response.Write("<script>alert('Não existe o usuário informado cadastrado, favor entre em contato com a equipe de TI.')</script>");
                return;
            }

            if (!usuario.UsuarioEncontrado)
            {
                Response.Write($"<script>alert('Não existe o usuário cadastrado para o email {Email}')</script>");
                return;
            }

            if (!usuario.SenhaValidada)
            {
                Response.Write($"<script>alert('{usuario.Nome} a senha informada esta incorreta')</script>");
                return;
            }

            // redirecionar para tela de cadastro de curso

        }

    }
}