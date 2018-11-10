using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
namespace LES_passagens_areas.Pages
{
    public class LoginModel : viewgenerico
    {
        public void OnGet()
        {

        }
        public void OnPostCev(string data)
        {
            var usu = new Usuarios();
            usu.Login = Request.Form["Email"];
            usu.Password = Request.Form["Password"];
            res=commands["CONSULTAR"].execute(usu);
            if (res.Entidades.Count > 0)
            { 
                HttpContext.Session.SetObjectAsJson("login", res.Entidades.ElementAt(0));
                Response.Redirect("./index");
            }
        }
    }
}