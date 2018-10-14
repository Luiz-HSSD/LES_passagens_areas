using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
namespace LES_passagens_areas.Pages
{
    public class cartModel : viewgenerico
    {
        public Venda ven = new Venda();
        private Passagens liv = new Passagens();
        string devil = "cart";
        //private static Gerar_produtos gp = new Gerar_produtos();
        public void OnGet(string cod, string code)
        {
            var venn = HttpContext.Session.GetObjectFromJson<Venda>(devil);
            if (venn!=null)
                ven = venn;
            
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
            //Pesquisar();
            HttpContext.Session.SetObjectAsJson(devil, ven);
            //*/
        }
        
        private void Pesquisar()
        {
            /*
            int evade = 0;
            string GRID = "<TABLE class='display' onload=\"bora()\" id='GridViewcat'><THEAD>{0}</THEAD><TBODY>{1}</TBODY></TABLE>";
            string tituloColunas = "<tr><th></th><th>Código</th><th>Nome</th><th>Descrição </th><th>preço unitario</th><th>qtd</th><th>preço</th></tr>";
            string linha = "<tr><td></td>  ";
            linha += "<td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td><a href='cart.aspx?code={0}'>-</a>{4}<a href='cart.aspx?cod={0}'>+</a></td><td>{5}</td></tr>";
            try
            {
                evade = ven.Produtos.Count;
            }
            catch
            {
                evade = 0;
            }
            StringBuilder conteudo = new StringBuilder();
            for (int i = 0; i < evade; i++)
            {
                liv = (Livro)ven.Produtos.ElementAt(i).Pro;
                conteudo.AppendFormat(linha,
                    liv.Id.ToString(),
                    liv.Nome.ToString(),
                    liv.Descricao.ToString(),
                    liv.Preco,
                    ven.Produtos.ElementAt(i).Qtd,
                    liv.Preco * ven.Produtos.ElementAt(i).Qtd
                    );


            }
            string tabelafinal = string.Format(GRID, tituloColunas, conteudo.ToString());
            divTable.InnerHtml = tabelafinal;
            */
        }

    }
}