using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
namespace LES_passagens_areas.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {

        }
        public void OnPostCev(string data)
        {
            HttpContext.Session.SetObjectAsJson("login", new Usuarios());
            Response.Redirect("./index");
        }
    }
}