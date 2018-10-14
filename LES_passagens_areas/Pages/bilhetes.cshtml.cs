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

namespace LES_passagens_areas.Pages
{
    public class bilhetesModel : viewgenerico
    {
            public IEnumerable<SelectListItem> listItems;
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
                                        Text = ((Aviao)x).Nome + " " + ((Aviao)x).Marca + " " + ((Aviao)x).Serie
                                    });

                return new SelectList(roles, "Value", "Text");
            }
            public List<EntidadeDominio> GetRoles2()
            {

                var roles = commands["CONSULTAR"].execute(new Dominio.Passagens()).Entidades;


                return roles;
            }
            public List<EntidadeDominio> GetRoles3()
            {

                var roles = commands["CONSULTAR"].execute(new Dominio.Passagens()).Entidades;


                return roles;
            }

            public IEnumerable<SelectListItem> GetRoles4()
            {

                var roles = commands["CONSULTAR"].execute(new Aeroporto()).Entidades
                            .Select(x =>
                                    new SelectListItem
                                    {
                                        Value = x.ID.ToString(),
                                        Text = ((Aeroporto)x).Nome
                                    });

                return new SelectList(roles, "Value", "Text");
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
            public SelectList aviao { get; set; }
            public SelectList classe { get; set; }
            public SelectList Passagens { get; set; }
            public void OnGet(string cod, string del)
            {
            if(!string.IsNullOrEmpty(cod))
            HttpContext.Session.SetString("cod", cod);
            classe = (SelectList)GetRoles();
                aviao = (SelectList)GetRoles1();
                //aviao = (SelectList)GetRoles1();
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
                    var selected = aviao.Where(x => x.Value == categoria.Aviao_v.ID.ToString()).First();
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
            Venda ven = new Venda();
        string devil = "cart";
        public void OnPostWay2(string data)
            {
                Bilhete a = new Bilhete()
                {
                    Nome= Request.Form["nome"],
                    RG= Request.Form["RG"],
                    cpf = Request.Form["CPF"],
                    Email=Request.Form["email"],
                    passaporte=Request.Form["Passaporte"],
                    Sexo=Convert.ToBoolean(Request.Form["sexo"])
                };
                var venn = HttpContext.Session.GetObjectFromJson<Venda>(devil);
                if (venn != null)
                    ven = venn;
                string cod = HttpContext.Session.GetString("cod");
                if (!string.IsNullOrEmpty(cod))
                ven.Viagems.Find(x => x.Voo.ID == int.Parse(cod)).Passageiros.Add(a);
                HttpContext.Session.SetObjectAsJson(devil, ven);
            
        }
            public void OnPostWay4(string data)
            {
                classe = (SelectList)GetRoles();
                aviao = (SelectList)GetRoles1();
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
        }
    }