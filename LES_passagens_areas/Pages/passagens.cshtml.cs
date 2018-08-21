using System;
using System.Web;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.DAO;
using Dominio;
namespace LES_passagens_areas.Pages
{
    public class passagensModel : viewgenerico
    {
        public IEnumerable<SelectListItem> listItems ;
        private IEnumerable<SelectListItem> GetRoles()
        {

            var roles = commands["CONSULTAR"].execute(new Classe()).Entidades
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = ((Classe)x).Nome
                                });

            return new SelectList(roles, "Value", "Text");
        }

        public string message { get; set; }
        public void OnGet()
        {
            listItems = GetRoles();
            message = "";
        }
        public void OnPostWay2(string data)
        {
            // to do : return something
            message = "Successfully registered "+Request.Form["go"];
        }
    }
}