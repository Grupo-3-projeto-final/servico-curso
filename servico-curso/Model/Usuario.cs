using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.EnterpriseServices;
using System.Linq;
using System.Net.Mail;
using System.Web;

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

        public Usuario CarregarDadosUsuario()
        {
            if (Email.IsNullOrWhiteSpace()) 
                return null;

            //Mudar para banco
            Usuario usuario = BuscarUsuario(Email);

            if (usuario == null)
            {
                UsuarioEncontrado = false;
                return this;
            }

            UsuarioEncontrado = Email.Equals(usuario.Email);
            SenhaValidada = Senha.Equals(usuario.Senha);
            Nome = usuario.Nome;

            return this;
        }

        private Usuario BuscarUsuario(string email)
        {
            if (!UsuariosTeste.Any())
                CarregarUsuarios();

            var usuario = UsuariosTeste.Where(x => x.Email.Equals(email)).FirstOrDefault();

            return usuario;
        }

        private void CarregarUsuarios()
        {
            UsuariosTeste.Add(new Usuario("teste1","123")
            {
                Id = 1,
                Nome = "João",
            });
            UsuariosTeste.Add(new Usuario("teste2", "456")
            {
                Id = 2,
                Nome = "Maria"
            });
        }
    }
}