using Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LES_passagens_areas.Api
{
    public class statusApi:ApiGenerico
    {
        public IEnumerable<SelectListItem> GetRoles4()
        {

            var roles = commands["CONSULTAR"].execute(new Aeroporto()).Entidades
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = ((Aeroporto)x).Nome
                                }).ToList();
            roles.Insert(0, new SelectListItem { Value = "0", Text = "Selecione o aroporto" });
            return new SelectList(roles, "Value", "Text");
        }
        public List<EntidadeDominio> getStatus()
        {
            return commands["CONSULTAR"].execute(new Status()).Entidades;
        }

        public SelectList getMotivos(int cod)
        {

            var roles = commands["CONSULTAR"].execute(new Motivo() { Dep = new Departamento { ID = cod } }).Entidades
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = ((Motivo)x).Nome
                                }).ToList();
            roles.Insert(0, new SelectListItem { Value = "0", Text = "Selecione o motivo" });
            return new SelectList(roles, "Value", "Text");
        }
        public string mount_url(int id)
        {
            if (!string.IsNullOrEmpty(dep) && !string.IsNullOrEmpty(voo))
                return "./status?dep=" + dep + "&voo=" + voo + "&st=" + id;
            else if (!string.IsNullOrEmpty(voo))
                return "./status?voo=" + voo + "&st=" + id;
            else if (!string.IsNullOrEmpty(dep))
                return "./status?dep=" + dep + "&st=" + id;
            else
                return "./status?st=" + id;
        }
        public List<EntidadeDominio> resposta = new List<EntidadeDominio>();
        public string nome_dep = null;
        public string dep, voo;
        public string message { get; set; }
        public SelectList go = new SelectList(new List<SelectListItem>(), "", "");
        public void OnGet(string dep, string voo, string st)
        {
            if (!autenticar(2))
                return;
            if (!string.IsNullOrEmpty(st))
            {
                int a;
                int.TryParse(st, out a);
                resposta = commands["ALTERAR"].execute(new Status() { ID = a }).Entidades;
            }
            if (!string.IsNullOrEmpty(dep))
            {

                this.dep = dep;
                HttpContext.Session.Setstring("dep", dep);
                int a;
                int.TryParse(dep, out a);
                go = getMotivos(a);
                if (!string.IsNullOrEmpty(voo))
                {

                    this.voo = voo;
                    HttpContext.Session.Setstring("voo", voo);
                    int b;
                    int.TryParse(voo, out b);
                    resposta = commands["CONSULTAR"].execute(new Status() { Atual = new Departamento() { ID = a }, Passageiro = new Bilhete() { passagem = new Viagem() { Voo = new Passagens() { ID = b } } } }).Entidades;
                }
                else
                    resposta = commands["CONSULTAR"].execute(new Status() { Atual = new Departamento() { ID = a } }).Entidades;
                if (resposta.Count > 0)
                {
                    nome_dep = ((Status)resposta.ElementAt(0)).Atual.Nome;
                }
            }
            else if (!string.IsNullOrEmpty(voo))
            {

                this.voo = voo;
                HttpContext.Session.Setstring("voo", voo);
                int a;
                int.TryParse(voo, out a);
                resposta = commands["CONSULTAR"].execute(new Status() { Passageiro = new Bilhete() { passagem = new Viagem() { Voo = new Passagens() { ID = a } } } }).Entidades;
            }
            else
                resposta = getStatus();

        }
        public void OnPostDevil(string data)
        {
            int b = 0;
            int.TryParse(Request.Form["id_mt"].ToString(), out b);
            int c = 0;
            int.TryParse(Request.Form["categoria"].ToString(), out c);
            message = commands["SALVAR"].execute(new Dominio.Barrado() { Causa = Request.Form["causa"], Categoria = new Motivo() { ID = c }, Passageiro = new Status() { ID = b } }).Msg;
            voo = HttpContext.Session.Getstring("voo");
            dep = HttpContext.Session.Getstring("dep");
            int a;
            int.TryParse(dep, out a);
            go = getMotivos(a);
            int r = 0;
            int.TryParse(voo, out r);
            resposta = commands["CONSULTAR"].execute(new Status() { Atual = new Departamento() { ID = a }, Passageiro = new Bilhete() { passagem = new Viagem() { Voo = new Passagens() { LO_chegada = new Aeroporto() { ID = r } } } } }).Entidades;
            if (!string.IsNullOrEmpty(dep) && resposta.Count > 0)
            {
                nome_dep = ((Status)resposta.ElementAt(0)).Atual.Nome;
            }

        }
    }
}
