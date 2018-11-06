using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace LES_passagens_areas.Pages
{
    public class cartModel : viewgenerico
    {
        public Venda ven = new Venda();
        private Passagens liv = new Passagens();
        string devil = "cart";
        //private static Gerar_produtos gp = new Gerar_produtos();

        public IEnumerable<SelectListItem> listItems;
        public IEnumerable<SelectListItem> GetRoles()
        {
            var roles = commands["CONSULTAR"].execute(new Cliente()).Entidades
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = ((Cliente)x).Nome
                                });
            var roles2 = roles.ToList();
            roles2.Insert(0, new SelectListItem { Value = 0.ToString(), Text = "Selecione o cliente:" });
            return new SelectList(roles2, "Value", "Text");
        }
        public string message { get; set; }
        public void OnGet(string cod, string code)
        {
            var venn = HttpContext.Session.GetObjectFromJson<Venda>(devil);
            if (venn!=null)
                ven = venn;
            listItems = GetRoles();
            if (!string.IsNullOrEmpty(cod))
                {
                    liv.ID = int.Parse(cod);
                //gp.Colocar_preco.Clear();
                //gp.Colocar_preco.Add(liv);
                var roles = commands["CONSULTAR"].execute(liv).Entidades;
                Passagem_Venda pass = new Passagem_Venda() { Pass = roles.Cast<Passagens>().ToList() };
                res = commands["CONSULTAR"].execute(pass);
                    liv = pass.Pass.ElementAt(0);
                    bool chk = false;
                    foreach (Viagem item in ven.Viagems)
                    {
                        if (item.Voo.ID == liv.ID)
                        {
                            item.qtd++;
                            item.Valor = item.Valor_Unidade * item.qtd;
                            chk = true;
                            break;
                        }
                    }
                    if (!chk)
                    {
                        Viagem item = new Viagem()
                        {
                            Voo = liv,
                            qtd = 1,
                            Valor = liv.Preco_uni,
                            Valor_Unidade = liv.Preco_uni
                        };
                        ven.Viagems.Add(item);
                    }
                }
                if (!string.IsNullOrEmpty(code))
                {
                liv.ID = int.Parse(code);
                var roles = commands["CONSULTAR"].execute(liv).Entidades;
                Passagem_Venda pass = new Passagem_Venda() { Pass = roles.Cast<Passagens>().ToList() };
                res = commands["CONSULTAR"].execute(pass);
                liv = pass.Pass.ElementAt(0);
                foreach (Viagem item in ven.Viagems)
                    {
                        if (item.Voo.ID == liv.ID)
                        {
                            item.qtd--;
                            item.Valor = item.Valor_Unidade * item.qtd;
                            if (item.qtd <= 0)
                                ven.Viagems.Remove(item);
                            break;
                        }
                    }

                }
            HttpContext.Session.SetObjectAsJson(devil, ven);
            //*/
        }
        public void OnPostWay2(string data)
        {
            var venn = HttpContext.Session.GetObjectFromJson<Venda>(devil);            
            int b = 0;
            int.TryParse(Request.Form["adf"].ToString(), out b);
            venn.Cliente_prop.ID = b;
            int c = 0;
            int.TryParse(Request.Form["id_card"].ToString(), out c);
            venn.Forma_pagamento.ID = c;
            int d = 0;
            int.TryParse(Request.Form["ccv"].ToString(), out d);
            venn.Forma_pagamento.CCV = d;
            venn.Forma_pagamento.Nome_Titular = Request.Form["nome_card"];
            venn.Forma_pagamento.Numero = Request.Form["num_card"];
            venn.Forma_pagamento.Validade = Request.Form["validade"];
            message = commands["SALVAR"].execute(venn).Msg;
        }
        public void OnPostWay3(string data)
        {
            Response.Redirect(("./"));
        }

    }
}