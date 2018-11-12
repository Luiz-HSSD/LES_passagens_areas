﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LES_passagens_areas.Pages
{
    public class pedidosModel : viewgenerico
    {
        public List<EntidadeDominio> lt = new List<EntidadeDominio>();
        public void OnGet()
        {
            if (!autenticar(1))
                return;
            res = commands["CONSULTAR"].execute(new Venda() { Cliente_prop =new Cliente() {ID=1 } });
            lt = res.Entidades;
        }
    }
}