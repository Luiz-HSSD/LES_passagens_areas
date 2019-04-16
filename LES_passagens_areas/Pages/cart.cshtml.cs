using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;
using iTextSharp.text;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;

namespace LES_passagens_areas.Pages
{
    public class cartModel : viewgenerico
    {
        public Venda ven = new Venda();
        private Passagens liv = new Passagens();
        string devil = "cart";
        //private static Gerar_produtos gp = new Gerar_produtos();
        IHostingEnvironment _host;
        public cartModel(IHostingEnvironment _host)
        {
            this._host = _host;
        } 
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
        public string id_cli { get; set; }
        public string message { get; set; }
        public void OnGet(string cod, string code)
        {
            if (!autenticar(1))
                return;
            var usu= HttpContext.Session.GetObjectFromJson<Usuarios>("login");
            if(usu==null)
            {
                Response.Redirect(("./"));
                return;
            }
            res =commands["CONSULTAR"].execute(new Cliente() {usuario=usu }); //usu
            if (res.Entidades.Count > 0)
              id_cli=  res.Entidades.ElementAt(0).ID.ToString();
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
                            Valor_Unidade = liv.Preco_uni,
                            Tipo = liv.Tipo
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
        public IActionResult OnPostWay2(string data)
        {
            var venn = HttpContext.Session.GetObjectFromJson<Venda>(devil);            
            int b = 0;
            int.TryParse(Request.Form["adf"], out b);
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
            if(!string.IsNullOrEmpty(message) && message != "sucesso!\n")
            {
               return OnPostWay4(venn);
            }
            else
            HttpContext.Session.SetObjectAsJson(devil, null);
            venn = HttpContext.Session.GetObjectFromJson<Venda>(devil);
            if (venn != null)
                ven = venn;
            var usu = HttpContext.Session.GetObjectFromJson<Usuarios>("login");
            if (usu == null)
            {
                
                Response.Redirect(("./"));
                return this.File(new byte[1], "application/pdf");
            }
            
            res = commands["CONSULTAR"].execute(new Cliente() { usuario = usu }); //usu
            if (res.Entidades.Count > 0)
                id_cli = res.Entidades.ElementAt(0).ID.ToString();
            return this.File(new byte[1], "application/pdf");
        }
        public void OnPostWay3(string data)
        {
            Response.Redirect(("./"));
        }
        public IActionResult OnPostWay4(Venda data)
        {
            byte[] result;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                //creating a sample Document
                iTextSharp.text.Document doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 30f, 30f, 30f, 30f);
                iTextSharp.text.pdf.PdfWriter writer = iTextSharp.text.pdf.PdfWriter.GetInstance(doc, ms);
                doc.Open();
                
                doc.Add(Image.GetInstance(new Uri(_host.WebRootPath+"/images/les_logo.jpg")));

                doc.Add(new Chunk("\n"));
                var b = new iTextSharp.text.Chunk("LES_passagens_aereas");
                b.Font.Color = BaseColor.Red;
                doc.Add(b);
                var bb = new Table(3);
                var bh=new Cell("partida");
                bh.Header = true;
                bb.AddCell(bh);
                bh = new Cell("chegada");
                bh.Header = true;
                bb.AddCell(bh);
                bh = new Cell("tempo partida");
                bh.Header = true;
                bb.AddCell(bh);
                for (int i=0; i<data.Viagems.Count;i++)
                {
                    bb.AddCell(new Cell(data.Viagems.ElementAt(i).Voo.LO_partida.Nome), i+1, 0);
                    bb.AddCell(new Cell(data.Viagems.ElementAt(i).Voo.LO_chegada.Nome), i+1, 1);
                    bb.AddCell(new Cell(data.Viagems.ElementAt(i).Voo.DT_partida.ToString("dd/MM/yyyy HH:mm")), i+1, 2);
                    
                }
                doc.Add(bb);
                doc.Close();
                result = ms.ToArray();
                   
            }
            
            return this.File(result, "application/pdf");
        }

    }
}