using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LES_passagens_areas.Pages
{
    public class RegisterModel : viewgenerico
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

            var roles = commands["CONSULTAR"].execute(new Check_in()).Entidades;


            return roles;
        }
        public List<Cartao_Credito> GetRoles3()
        {
            HttpContext.Session.SetObjectAsJson(devil, lb);
            lb = HttpContext.Session.GetObjectFromJson<List<Cartao_Credito>>(devil);

            return lb;
        }
        public string message { get; set; }
        public string id { get; set; }
        public string nome { get; set; }
        public string password { get; set; }
        public string com_password { get; set; }
        public string cpf { get; set; }
        public string rg { get; set; }
        public string dt_nas { get; set; }
        public string email { get; set; }
        public string cep { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public string complemento { get; set; }
        public string cidade { get; set; }
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
            nome = "";
            password = "";
            com_password = "";
            cpf = "";
            rg = "";
            dt_nas = "";
            email = "";
            cep = "";
            rua = "";
            numero = "";
            bairro = "";
            complemento = "";
            cidade = "";
            if (!string.IsNullOrEmpty(cod))
            {
                res = commands["CONSULTAR"].execute(new Dominio.Check_in() { ID = int.Parse(cod) });
                var categoria = (Dominio.Check_in)res.Entidades.ElementAt(0);
                id = Convert.ToString(categoria.ID);
                /*
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
                */
            }
            if (!string.IsNullOrEmpty(del))
            {

                commands["EXCLUIR"].execute(new Dominio.Check_in() { ID = int.Parse(del) });

            }
            if (!string.IsNullOrEmpty(dele))
            {

                lb = HttpContext.Session.GetObjectFromJson<List<Cartao_Credito>>(devil);
                int b;
                int.TryParse(dele, out b);
                lb.RemoveAt(b - 1);
                HttpContext.Session.SetObjectAsJson(devil, lb);

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
            Cliente bg = new Cliente();
            if(Request.Form["password"]!= Request.Form["com_password"])
            {
                message = "digite a mesma senha";
                return;
            }
            bg.Nome = Request.Form["nome"];
            bg.usuario.Password = Request.Form["password"];
            if (Convert.ToBoolean(Request.Form["sexo"]))
                bg.Sexo = 'M';
            else
                bg.Sexo = 'F';
            bg.Cpf = Request.Form["cpf"];
            bg.Rg = Request.Form["rg"];
            bg.Dt_Nas = DateTime.ParseExact(Request.Form["dt_nas"],"dd/mm/yyyy",CultureInfo.GetCultureInfo("pt-BR"));
            bg.usuario.Login = Request.Form["email"];
            bg.Endereco.Cep = Request.Form["cep"];
            bg.Endereco.Logradouro = Request.Form["rua"];
            bg.Endereco.Numero = Request.Form["numero"];
            bg.Endereco.Bairro = Request.Form["bairro"];
            bg.Endereco.Complemento = Request.Form["complemento"];
            bg.Endereco.Cidade = Request.Form["cidade"];
            bg.Endereco.UF = Request.Form["uf"];
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

            bg.Cartoes = HttpContext.Session.GetObjectFromJson<List<Cartao_Credito>>(devil);
            message = commands["SALVAR"].execute(bg).Msg;
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
            message = commands["ALTERAR"].execute(new Dominio.Check_in() {  }).Msg;
        }
        public void OnPostWay4(string data)
        {
            classe = (SelectList)GetRoles();
            bilhete = (SelectList)GetRoles1();
            id = "";
            nome = "";
            password = "";
            com_password = "";
            /*dat_partida = "";
            dat_chegada = "";
            hor_partida = "";
            hor_chegada = "";*/
            ///message = commands["EXCLUIR"].execute(new Aviao() { ID = a }).Msg;
        }
        public string asdf = "";
        public string asdf2 = "";
        List<Cartao_Credito> lb = new List<Cartao_Credito>();
        const string devil = "devil";
        public void OnPostWay5(string data)
        {
            lb = HttpContext.Session.GetObjectFromJson<List<Cartao_Credito>>(devil);
            id = Request.Form["id"];
            nome = Request.Form["nome"];
            password = Request.Form["password"];
            com_password = Request.Form["com_password"];
            cpf = Request.Form["cpf"];
            rg = Request.Form["rg"];
            dt_nas = Request.Form["dt"];
            email = Request.Form["email"];
            cep = Request.Form["cep"];
            rua = Request.Form["rua"];
            numero = Request.Form["numero"];
            bairro = Request.Form["bairro"];
            complemento = Request.Form["complemento"];
            cidade = Request.Form["cidade"];
            Cartao_Credito bg = new Cartao_Credito();
            bg.ID = lb.Count + 1;
            bg.Nome_Titular = Request.Form["nome_card"];
            bg.Numero = Request.Form["num_card"];
            bg.Validade = Request.Form["validade"];
            int a=0;
            int.TryParse(Request.Form["ccv"],out a);
            bg.CCV = a;
            bg.Bandeira.ID = 1;
            lb.Add(bg);
            HttpContext.Session.SetObjectAsJson(devil, lb);

            ///message = commands["EXCLUIR"].execute(new Aviao() { ID = a }).Msg;
        }
    }
}