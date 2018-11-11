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
    public class sumarioModel : viewgenerico
    {
        public IEnumerable<EntidadeDominio> GetRoles()
        {
            int b=0;
            int.TryParse(codd, out b);
            List<EntidadeDominio> ld = new List<EntidadeDominio>();
            foreach(Status s in  commands["CONSULTAR"].execute(new Status()).Entidades)
            {
                Sumario su = new Sumario();
                if (ld.Count == 0)
                {
                    su.Qtd = 1;
                    su.Dep.ID = s.Atual.ID;
                    su.Dep.Nome = s.Atual.Nome;
                    if (s.IsDesembarque)
                    {
                        su.Aero.ID = s.Passageiro.passagem.Voo.LO_chegada.ID;
                        su.Aero.Nome = s.Passageiro.passagem.Voo.LO_chegada.Nome;
                        su.Aero.sigla = s.Passageiro.passagem.Voo.LO_chegada.sigla;
                    }
                    else
                    {
                        su.Aero.ID = s.Passageiro.passagem.Voo.LO_partida.ID;
                        su.Aero.Nome = s.Passageiro.passagem.Voo.LO_partida.Nome;
                        su.Aero.sigla = s.Passageiro.passagem.Voo.LO_partida.sigla;
                    }
                    if( b == 0 || (b == su.Aero.ID))
                    ld.Add(su);
                }
                else
                {
                    bool exists = false;
                    foreach(Sumario sum in ld)
                    {
                        if (s.Atual.ID == sum.Dep.ID)
                        {
                            if (b == 0 || (b == su.Aero.ID))
                                sum.Qtd++;
                            exists = true;
                        }
                    }
                    if (!exists)
                    {
                        su.Qtd = 1;
                        su.Dep.ID = s.Atual.ID;
                        su.Dep.Nome = s.Atual.Nome;
                        if (s.IsDesembarque)
                        {
                            su.Aero.ID = s.Passageiro.passagem.Voo.LO_chegada.ID;
                            su.Aero.Nome = s.Passageiro.passagem.Voo.LO_chegada.Nome;
                            su.Aero.sigla = s.Passageiro.passagem.Voo.LO_chegada.sigla;
                        }
                        else
                        {
                            su.Aero.ID = s.Passageiro.passagem.Voo.LO_partida.ID;
                            su.Aero.Nome = s.Passageiro.passagem.Voo.LO_partida.Nome;
                            su.Aero.sigla = s.Passageiro.passagem.Voo.LO_partida.sigla;
                        }
                        if (b == 0 || b == su.Aero.ID)
                            ld.Add(su);
                    }

                }
            }
            
                return ld;
                      
                
        }
        public string codd;
        public IEnumerable<EntidadeDominio> lt =new List<EntidadeDominio>();
        public void OnGet(int cod)
        {
           if(!autenticar(2))
                return;
            if (cod != 0)
                codd = cod.ToString();
            lt= GetRoles();
        }
    }
}