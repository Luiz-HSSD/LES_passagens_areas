﻿using System;
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
                    ld.Add(su);
                }
                else
                {
                    bool exists = false;
                    foreach(Sumario sum in ld)
                    {
                        if (s.Atual.ID == sum.Dep.ID)
                        {
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
                        ld.Add(su);
                    }

                }
            }
            return ld;
                      
                
        }
        public void OnGet()
        {
            autenticar(2);
        }
    }
}