using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenQA.Selenium.Remote;

namespace LES_passagens_areas.Pages
{
    public class check_inModel : viewgenerico
    {
        public IEnumerable<SelectListItem> listItems;
        public IEnumerable<SelectListItem> GetRoles()
        {
            var roles = commands["CONSULTAR"].execute(new Bilhete()).Entidades
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = ((Bilhete)x).Nome.ToString() + " " + ((Bilhete)x).RG.ToString()
                                });

            return new SelectList(roles, "Value", "Text");
        }
        public IEnumerable<SelectListItem> listItems1;
        public IEnumerable<SelectListItem> GetRoles1()
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
        public List<EntidadeDominio> GetRoles2()
        {

            var roles = commands["CONSULTAR"].execute(new Dominio.Passagens()).Entidades;


            return roles;
        }
        public List<Bagagem> GetRoles3()
        {
            HttpContext.Session.SetObjectAsJson(devil, lb);
            lb = HttpContext.Session.GetObjectFromJson<List<Bagagem>>(devil);

            return lb;
        }
        public string message { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string partida { get; set; }
        public string chegada { get; set; }
        public string dat_partida { get; set; }
        public string dat_chegada { get; set; }
        public string hor_partida { get; set; }
        public string hor_chegada { get; set; }
        public SelectList aeroporto { get; set; }
        public SelectList voo { get; set; }
        public SelectList bilhete { get; set; }

        public SelectList assento { get; set; }
        public SelectList classe { get; set; }
        
        public void OnGet(string cod, string del, string dele)
        {
            classe = (SelectList)GetRoles1();
            bilhete = (SelectList)GetRoles();
            id = "";
            name = "";
            partida = "";
            chegada = "";
            dat_partida = "";
            dat_chegada = "";
            hor_partida = "";
            hor_chegada = "";
            if (!string.IsNullOrEmpty(cod))
            {
                res = commands["CONSULTAR"].execute(new Dominio.Passagens() { ID = int.Parse(cod) });
                var categoria = (Dominio.Passagens)res.Entidades.ElementAt(0);
                id = Convert.ToString(categoria.ID);
                name = categoria.QTD.ToString();
                partida = categoria.LO_partida.ID.ToString();
                chegada = categoria.LO_chegada.ID.ToString();
                dat_partida = categoria.DT_partida.ToString("dd/MM/yyyy");
                dat_chegada = categoria.DT_chegada.ToString("dd/MM/yyyy");
                hor_partida = categoria.DT_partida.ToString("HH:mm");
                hor_chegada = categoria.DT_chegada.ToString("HH:mm");
                var selected = aeroporto.Where(x => x.Value == categoria.Aviao_v.ID.ToString()).First();
                selected.Selected = true;
                var selected2 = classe.Where(x => x.Value == categoria.Tipo.ID.ToString()).First();
                selected2.Selected = true;
            }
            if (!string.IsNullOrEmpty(del))
            {

                commands["EXCLUIR"].execute(new Dominio.Passagens() { ID = int.Parse(del) });

            }
            listItems = GetRoles();
            listItems1 = GetRoles1();
            message = "";
            TagBuilder td = new TagBuilder("div");
            TagBuilder tdt = new TagBuilder("table");
            var write = new StringWriter();
            td.InnerHtml.Append(tdt.ToString());
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
            int a = 0;
            int.TryParse(Request.Form["id"].ToString(), out a);
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
            message = commands["ALTERAR"].execute(new Dominio.Passagens() { ID = a, DT_partida = e, DT_chegada = f, QTD = b, LO_partida = new Aeroporto() { ID = Convert.ToInt32(Request.Form["partida"]) }, LO_chegada = new Aeroporto() { ID = Convert.ToInt32(Request.Form["destino"]) }, Tipo = new Classe() { ID = c }, Aviao_v = new Aviao() { ID = d } }).Msg;
        }
        public void OnPostWay4(string data)
        {
            classe = (SelectList)GetRoles();
            bilhete = (SelectList)GetRoles1();
            id = "";
            name = "";
            partida = "";
            chegada = "";
            dat_partida = "";
            dat_chegada = "";
            hor_partida = "";
            hor_chegada = "";
            ///message = commands["EXCLUIR"].execute(new Aviao() { ID = a }).Msg;
        }
        public string asdf="";
        public string asdf2 = "";
        List<Bagagem> lb = new List<Bagagem>();
        const string devil = "devil";
        public void OnPostWay5(string data)
        {
            lb= HttpContext.Session.GetObjectFromJson<List<Bagagem>>(devil);
            Bagagem bg = new Bagagem(new Check_in());
            bg.ID = lb.Count + 1;
            string medidas = Request.Form["qtd"];
            bg.peso= Request.Form["partida"];
            lb.Add(bg);
            HttpContext.Session.SetObjectAsJson(devil, lb);
           
            ///message = commands["EXCLUIR"].execute(new Aviao() { ID = a }).Msg;
        }
    }
}