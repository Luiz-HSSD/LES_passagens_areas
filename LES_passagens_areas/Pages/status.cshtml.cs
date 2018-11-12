using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;

namespace LES_passagens_areas.Pages
{
    public class statusModel : viewgenerico
    {
        public List<EntidadeDominio> getStatus()
        {
            return commands["CONSULTAR"].execute(new Status()).Entidades;
        }

        public string mount_url(int id)
        {
            if(!string.IsNullOrEmpty(dep) && !string.IsNullOrEmpty(voo))
                return "./status?dep=" + dep  + "&voo=" + voo + "&st=" + id;
            else if (!string.IsNullOrEmpty(voo))
                return "./status?voo=" + voo  + "&st=" + id;
            else if (!string.IsNullOrEmpty(dep))
                return "./status?dep=" + dep  + "&st=" + id;
            else
                return "./status?st=" + id;
        }
        public List<EntidadeDominio> resposta =new List<EntidadeDominio>();

        public string nome_dep = null;
        public string dep, voo;
        public void OnGet(string dep,string voo, string st)
        {
            if(!autenticar(2))
                return;
            if (!string.IsNullOrEmpty(st))
            {
                int a;
                int.TryParse(st, out a);
                resposta = commands["ALTERAR"].execute(new Status() { ID = a } ).Entidades;
            }
            if (!string.IsNullOrEmpty(dep))
            {
                this.dep = dep;
                int a;
                int.TryParse(dep, out a);
                if (!string.IsNullOrEmpty(voo))
                {
                    this.voo = voo;
                    int b;
                    int.TryParse(voo, out b);
                    resposta = commands["CONSULTAR"].execute(new Status() { Atual = new Departamento() { ID = a }, Passageiro = new Bilhete() { passagem = new Viagem() { Voo = new Passagens() { LO_chegada = new Aeroporto() { ID = b } } } } }).Entidades;
                }
                else
                    resposta =  commands["CONSULTAR"].execute(new Status() {Atual =new Departamento() {ID =a } }).Entidades;
                if ( resposta.Count > 0 )
                {
                    nome_dep= ((Status)resposta.ElementAt(0)).Atual.Nome;
                }
            }
            else if (!string.IsNullOrEmpty(voo))
            {
                this.voo = voo;
                int a;
                int.TryParse(voo, out a);
                resposta = commands["CONSULTAR"].execute(new Status() { Passageiro = new Bilhete() { passagem =new Viagem() { Voo= new Passagens() { LO_chegada=new Aeroporto() { ID= a } } } } }).Entidades;                
            }
            else
                resposta = getStatus();
        }
    }
}