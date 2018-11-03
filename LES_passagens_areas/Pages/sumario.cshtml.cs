using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LES_passagens_areas.Pages
{
    public class sumarioModel : viewgenerico
    {
        public IEnumerable<EntidadeDominio> GetRoles()
        {
            return commands["CONSULTAR"].execute(new Classe()).Entidades;
                      
                
        }
        public void OnGet()
        {

        }
    }
}