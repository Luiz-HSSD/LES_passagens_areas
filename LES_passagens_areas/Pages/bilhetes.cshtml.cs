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
            public List<Bilhete> GetRoles3()
            {
            var venn = HttpContext.Session.GetObjectFromJson<Venda>(devil);
            if (venn != null)
                ven = venn;
            var bora = ven.Viagems.Find(x => x.Voo.ID == int.Parse(HttpContext.Session.GetString("cod")));
                var roles = bora.Passageiros;


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
            public List<Bilhete> devill = new List<Bilhete>();
            public void OnGet(string cod,string code, string del)
            {
            if(!autenticar(1))
                return;
            if(!string.IsNullOrEmpty(cod))
            HttpContext.Session.SetString("cod", cod);
            classe = (SelectList)GetRoles();
                aviao = (SelectList)GetRoles1();
            devill= GetRoles3();
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

                /*
                    if (bora.qtd <= bora.Passageiros.Count)
                        Response.Redirect("./cart");
                        */
                }
            if (!string.IsNullOrEmpty(del))
            {
                var venn = HttpContext.Session.GetObjectFromJson<Venda>(devil);
                if (venn != null)
                    ven = venn;
                var bora = ven.Viagems.Find(x => x.Voo.ID == int.Parse(HttpContext.Session.GetString("cod")));
                var roles = bora.Passageiros;
                roles.RemoveAt(int.Parse( del));
                HttpContext.Session.SetObjectAsJson(devil, ven);
            }
            devill = GetRoles3();
            listItems = GetRoles();
            listItems1 = GetRoles1();
            message = "";
            TagBuilder td = new TagBuilder("div");
            TagBuilder tdt = new TagBuilder("table");
            var write = new StringWriter();
            td.InnerHtml.Append(tdt.ToString());
            }
            Venda ven = new Venda();
        public const string devil = "cart";
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
                {
                    var go = ven.Viagems.Find(x => x.Voo.ID == int.Parse(cod));
                    go.Passageiros.Add(a);
                    if (go.qtd < go.Passageiros.Count)
                        go.qtd = go.Passageiros.Count;
                }
                HttpContext.Session.SetObjectAsJson(devil, ven);
                var bora=    ven.Viagems.Find(x => x.Voo.ID == int.Parse(cod));
                
                if (bora.qtd<=bora.Passageiros.Count )
                    Response.Redirect("./cart");
            listItems = GetRoles();
            listItems1 = GetRoles1();
            devill = GetRoles3();
        }
            public void OnPostWay4(string data)
            {
                classe = (SelectList)GetRoles();
                aviao = (SelectList)GetRoles1();
                devill = GetRoles3();
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