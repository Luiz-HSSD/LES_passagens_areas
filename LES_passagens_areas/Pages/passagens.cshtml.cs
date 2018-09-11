using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.DAO;
using Dominio;
using System.IO;
using Microsoft.AspNetCore.Html;
using System.Globalization;

namespace LES_passagens_areas.Pages
{
    public class passagensModel : viewgenerico
    {
        public IEnumerable<SelectListItem> listItems ;
        public IEnumerable<SelectListItem> GetRoles()
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
        public IEnumerable<SelectListItem> listItems1;
        public IEnumerable<SelectListItem> GetRoles1()
        {

            var roles = commands["CONSULTAR"].execute(new Aviao()).Entidades
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = ((Aviao)x).Nome
                                });

            return new SelectList(roles, "Value", "Text");
        }
        public List<EntidadeDominio> GetRoles2()
        {

            var roles = commands["CONSULTAR"].execute(new Dominio.Passagens()).Entidades;
                        

            return roles;
        }
        public string message { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public void OnGet(string cod,string del)
        {
            id = "";
            name = "";
            if (!string.IsNullOrEmpty(cod))
            {
                res = commands["CONSULTAR"].execute(new Dominio.Passagens() { ID = int.Parse(cod) });
                var categoria = (Dominio.Passagens)res.Entidades.ElementAt(0);
                id = Convert.ToString(categoria.ID);
                name = categoria.LO_chegada;
                
            }
            if (!string.IsNullOrEmpty(del))
            {
                
                commands["EXCLUIR"].execute(new Dominio.Passagens() { ID=int.Parse(del)});
                
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
            DateTime e=DateTime.Now;
            DateTime.TryParseExact(Request.Form["dt_partida"].ToString() +" " + Request.Form["hr_partida"].ToString(),"dd/MM/yyyy HH:mm",new CultureInfo("pt-BR"), DateTimeStyles.None,  out e);
            DateTime f = DateTime.Now;
            DateTime.TryParseExact(Request.Form["dt_destino"].ToString() + " " + Request.Form["hr_destino"].ToString(), "dd/MM/yyyy HH:mm", new CultureInfo("pt-BR"), DateTimeStyles.None, out f);
            message = commands["SALVAR"].execute(new Dominio.Passagens() { QTD = b, DT_partida = e, DT_chegada = f, LO_partida = Request.Form["partida"], LO_chegada = Request.Form["destino"], Tipo = new Classe() { ID = c }, Aviao_v = new Aviao() { ID = d } }).Msg;
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
            message = commands["ALTERAR"].execute(new Dominio.Passagens() { ID = a,DT_partida=e,DT_chegada=f, QTD = b, LO_partida = Request.Form["partida"], LO_chegada = Request.Form["destino"], Tipo = new Classe() { ID = c }, Aviao_v = new Aviao() { ID = d } }).Msg;
        }
        public void OnPostWay4(string data)
        {
            int a = 0;
            int.TryParse(Request.Form["id"].ToString(), out a);
            message = commands["EXCLUIR"].execute(new Aviao() { ID = a }).Msg;
        }
    }
}