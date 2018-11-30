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
    public class bagagemModel : viewgenerico
    {
        List<Bagagem> lb = new List<Bagagem>();
        const string devil = "devil";
        public List<Bagagem> GetRoles3()
        {
            HttpContext.Session.SetObjectAsJson(devil, lb);
            lb = HttpContext.Session.GetObjectFromJson<List<Bagagem>>(devil);

            return lb;
        }
        public SelectList bilhete = new SelectList(new List<SelectListItem>());
        public string message { get; set; }
        public string name { get; set; }
        public void OnGet()
        {

        }
        public void OnPostWay2(string data)
        {
            /*
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
            message = commands["SALVAR"].execute(new Dominio.Check_in() { Bagagem = HttpContext.Session.GetObjectFromJson<List<Bagagem>>(devil), Entrada = new Bilhete() { ID = d }, Ocupante = new Assento(new Check_in()) { ID = f }, Passagem = new Viagem() { Tipo = new Classe() { ID = e }, Voo = new Passagens() { ID = c, Tipo = new Classe() { ID = e }, LO_partida = new Aeroporto() { ID = b } } } }).Msg;
            */
        }
    }
}