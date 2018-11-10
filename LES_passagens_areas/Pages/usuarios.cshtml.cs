using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LES_passagens_areas.Pages
{
    public class usuariosModel : viewgenerico
    {
        public List<EntidadeDominio> GetRoles()
        {
            var roles = commands["CONSULTAR"].execute(new Usuarios()).Entidades;
            return roles;
        }

        public string id;
        public string Login;
        public string senha;
        public string permissao;
        public string message;
        public void OnGet(string cod,string del)
        {
            if(!autenticar(3))
                return;
            if (!string.IsNullOrEmpty(cod))
            {
                res = commands["CONSULTAR"].execute(new Dominio.Usuarios() { ID = int.Parse(cod) });
                var categoria = (Dominio.Usuarios)res.Entidades.ElementAt(0);
                id = Convert.ToString(categoria.ID);
                Login = categoria.Login.ToString();
                senha = categoria.Password;
                permissao = categoria.Permisao.ToString();
            }
            if (!string.IsNullOrEmpty(del))
            {
                commands["EXCLUIR"].execute(new Usuarios() { ID = int.Parse(del) });
            }
        }
    

        public void OnPostWay2(string data)
        {
            var cat = new Usuarios();
            int b=0;
            int.TryParse(Request.Form["id"], out b);
            cat.ID = b;
            cat.Permisao = 2;
            cat.Login = Request.Form["login"];
            cat.Password = Request.Form["senha"];
            message = commands["SALVAR"].execute(cat).Msg;
        }

        public void OnPostWay3(string data)
        {
            var cat = new Usuarios();
            int b = 0;
            int.TryParse(Request.Form["id"], out b);
            cat.ID = b;
            cat.Login = Request.Form["login"];
            cat.Password = Request.Form["senha"];
            int c = 0;
            int.TryParse(Request.Form["permisao"], out c);
            cat.Permisao = c;
            message = commands["ALTERAR"].execute(cat).Msg;

        }

        public void OnPostWay4(string data)
        {
            id = "";
            Login = "";
            senha = "";
        }

    }
}