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
            //bilhete = (SelectList)GetRoles();
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
                res = commands["CONSULTAR"].execute(new Dominio.Check_in() { ID = int.Parse(cod) });
                var categoria = (Dominio.Check_in)res.Entidades.ElementAt(0);
                id = Convert.ToString(categoria.ID);
                var selected = aeroporto.Where(x => x.Value == categoria.Passagem.Voo.LO_partida.ID.ToString()).First();
                selected.Selected = true;
                var selected2 = voo.Where(x => x.Value == categoria.Passagem.Voo.ID.ToString()).First();
                selected2.Selected = true;
                var selected3 = bilhete.Where(x => x.Value == categoria.Entrada.ID.ToString()).First();
                selected3.Selected = true;
                var selected4 = classe.Where(x => x.Value == categoria.Passagem.Tipo.ID.ToString()).First();
                selected4.Selected = true;
                var selected5 = assento.Where(x => x.Value == categoria.Ocupante.ID.ToString()).First();
                selected5.Selected = true;
                lb = categoria.Bagagem;
                HttpContext.Session.SetObjectAsJson(devil, lb);
            }
            if (!string.IsNullOrEmpty(del))
            {

                commands["EXCLUIR"].execute(new Dominio.Check_in() { ID = int.Parse(del) });

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
            int.TryParse(Request.Form["aeroporto"].ToString(), out b);
            int c = 0;
            int.TryParse(Request.Form["voo"].ToString(), out c);
            int d = 0;
            int.TryParse(Request.Form["bilhete"].ToString(), out d);
            int e = 0;
            int.TryParse(Request.Form["go"].ToString(), out e);
            int f = 0;
            int.TryParse(Request.Form["goo"].ToString(), out f);
            message = commands["SALVAR"].execute(new Dominio.Check_in() { Bagagem = HttpContext.Session.GetObjectFromJson<List<Bagagem>>(devil), Entrada=new Bilhete() { ID=d},Ocupante=new Assento(new Check_in()) { ID=f} ,Passagem=new Viagem() {Tipo=new Classe() { ID=e}, Voo=new Passagens() {ID=c, Tipo = new Classe() { ID = e }, LO_partida =new Aeroporto() { ID=b} } } }).Msg;
        }
        public void OnPostWay3(string data)
        {
            int a = 0;
            int.TryParse(Request.Form["id"].ToString(), out a);
            int b = 0;
            int.TryParse(Request.Form["aeroporto"].ToString(), out b);
            int c = 0;
            int.TryParse(Request.Form["voo"].ToString(), out c);
            int d = 0;
            int.TryParse(Request.Form["bilhete"].ToString(), out d);
            int e = 0;
            int.TryParse(Request.Form["go"].ToString(), out e);
            int f = 0;
            int.TryParse(Request.Form["goo"].ToString(), out f);
            message = commands["ALTERAR"].execute( new Dominio.Check_in() { ID = a,Bagagem= HttpContext.Session.GetObjectFromJson<List<Bagagem>>(devil),  Entrada = new Bilhete() { ID = d }, Ocupante = new Assento(new Check_in()) { ID = f }, Passagem = new Viagem() { Tipo = new Classe() { ID = e}, Voo = new Passagens() { ID = c, Tipo = new Classe() { ID = e }, LO_partida = new Aeroporto() { ID = b } } } }).Msg;
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
            double b = 0;
            double.TryParse(Request.Form["partida"].ToString(), out b);
            bg.peso= b;
            lb.Add(bg);
            HttpContext.Session.SetObjectAsJson(devil, lb);
           
            ///message = commands["EXCLUIR"].execute(new Aviao() { ID = a }).Msg;
        }
    }
}