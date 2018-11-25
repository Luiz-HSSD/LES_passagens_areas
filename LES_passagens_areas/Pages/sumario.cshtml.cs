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
        public bool getantes_ou_dps(List<EntidadeDominio> go, Sumario a, Status ago)
        {
            int at = 0, att = 0 ,i = 0;
            for( i =0;i<go.Count;i++)
            {
                if (a.Dep.ID == go[i].ID)
                    at = i;
            }
            i = 0;
            for ( i = 0; i < go.Count; i++)
            {
                if (ago.Atual.ID == go[i].ID)
                    att = i;
            }
            if (((at >= att) && ago.IsDesembarque == a.IsDesembarque) || (a.IsDesembarque == true &&  ago.IsDesembarque==false))
                return true;
            return false;
        }
        public IEnumerable<EntidadeDominio> GetRoles()
        {
            int b=0;
            int.TryParse(codd, out b);
            List<EntidadeDominio> ld = new List<EntidadeDominio>();
            List<EntidadeDominio> ldd = commands["CONSULTAR"].execute(new Status()).Entidades;
            if(b!=0)
               ldd=ldd.Where(x => ((Status)x).Passageiro.passagem.Voo.ID == b).ToList();
            List<EntidadeDominio> lldd = commands["CONSULTAR"].execute(new Departamento() {Pass=new Passagens() {ID=b } }).Entidades;
            foreach (Status s in  ldd)
            {
                Sumario su = new Sumario();
                if (ld.Count == 0)
                {
                    su.Qtd = 1;
                    su.Dep.ID = s.Atual.ID;
                    su.Voo = s.Passageiro.passagem.Voo;
                    su.Dep.Nome = s.Atual.Nome;
                    su.IsDesembarque = s.IsDesembarque;
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
                    if( b == 0 || (b == su.Voo.ID))
                    ld.Add(su);
                }
                else
                {
                    bool exists = false;
                    foreach(Sumario sum in ld)
                    {
                        if (s.Atual.ID == sum.Dep.ID && s.IsDesembarque==sum.IsDesembarque)
                        {
                            if (b == 0 || (b == su.Aero.ID))
                                exists = true;
                        }
                        
                    }
                    if (!exists)
                    {
                        su.Qtd = 1;
                        su.Dep.ID = s.Atual.ID;
                        su.Voo = s.Passageiro.passagem.Voo;
                        su.Dep.Nome = s.Atual.Nome;
                        su.IsDesembarque = s.IsDesembarque;
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
                        if (b == 0 || b == su.Voo.ID)
                        
                             ld.Add(su);
                        
                    }
                    foreach (Sumario sum in ld)
                    {
                        if (s.Atual.ID == sum.Dep.ID)
                        {
                            if (((b == 0) || (b == su.Aero.ID)) && s.IsDesembarque == sum.IsDesembarque && exists == true)
                                sum.Qtd++;

                        }


                    }
                }
            }
            for (int q=0;q< ld.Count;q++ )
            {
                for (int s=0;s<ldd.Count;s++)
                {

                    {
                        if (getantes_ou_dps(lldd, ((Sumario)ld[q]), ((Status)ldd[s])))
                            ((Sumario)ld[q]).Qtd_nao_passaram++;
                        else
                            ((Sumario)ld[q]).Qtd_passaram++;                        
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