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
            int b = 0;
            int.TryParse(Request.Form["qtd"].ToString(), out b);
            int c = 0;
            int.TryParse(Request.Form["go"].ToString(), out c);
            int d = 0;
            int.TryParse(Request.Form["aviao"].ToString(), out d);
            DateTime e = DateTime.Now;
            DateTime.TryParseExact(Request.Form["dt_partida"].ToString() + " " + Request.Form["hr_partida"].ToString(), "dd/MM/yyyy HH:mm", new CultureInfo("pt-BR"), DateTimeStyles.None, out e);
            DateTime f = DateTime.Now;
            DateTime.TryParseExact(Request.Form["dt_destino"].ToString() + " " + Request.Form["hr_destino"].ToString(), "dd/MM/yyyy HH:mm", new CultureInfo("pt-BR"), DateTimeStyles.None, out f);
            message = commands["SALVAR"].execute(new Dominio.Passagens() { QTD = b, DT_partida = e, DT_chegada = f, LO_partida = new Aeroporto() { ID = Convert.ToInt32(Request.Form["partida"]) }, LO_chegada = new Aeroporto() { ID = Convert.ToInt32(Request.Form["destino"]) }, Tipo = new Classe() { ID = c }, Aviao_v = new Aviao() { ID = d } }).Msg;
        }
        public void OnPostWay3(string data)
        {
            Response.Redirect(("./"));
        }

    }
}