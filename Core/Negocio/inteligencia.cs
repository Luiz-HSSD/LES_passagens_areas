using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Dominio;
using Core.DAO;
using System.Linq;

namespace Core.Negocio
{
    class inteligencia : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Analise ana = (Analise)entidade;
            List<string> lbls = new List<string>();
            BagagemDAO go = new BagagemDAO();
            List<EntidadeDominio> dii = go.consultar(new Bagagem(new Check_in()) { Flg = true,dono=new Check_in(){Passagem=new Viagem() { Voo=new Passagens() {DT_partida= ana.Data_min, DT_chegada = ana.Data_max } } } });
            //dii= dii.OrderBy(x => ((Bagagem)x).dono.Passagem.Voo.DT_partida).ToList();
            foreach (Bagagem b in dii)
            {
                string c = b.dono.Passagem.Voo.LO_partida.sigla + " To " + b.dono.Passagem.Voo.LO_chegada.sigla;
                if (!ana.resultado.ContainsKey(c))
                {
                    ana.resultado.Add(c, new List<EntidadeDominio>() { b });
                }
                else
                {
                    ana.resultado[c].Add(b);
                }
            }
            for (int i=0;i< ana.resultado.Keys.Count;i++)
            {
                List<EntidadeDominio> b = ana.resultado.Values.ElementAt(i);
                List<EntidadeDominio> bb = new List<EntidadeDominio>();

                foreach (Bagagem cc in b)
                {
                    if (bb.Count == 0)
                    {
                        bb.Add(cc);
                        continue;
                    }
                    //if (cc.dono.Passagem.Voo.DT_partida > ana.Data_max )
                      //  break;
                    if (cc.dono.Passagem.Voo.DT_partida.ToString("MM/yyyy") == ((Bagagem)bb.ElementAt(bb.Count - 1)).dono.Passagem.Voo.DT_partida.ToString("MM/yyyy")
                        && cc.dono.Passagem.Voo.LO_partida.sigla== ((Bagagem)bb.ElementAt(bb.Count - 1)).dono.Passagem.Voo.LO_partida.sigla
                        && cc.dono.Passagem.Voo.LO_chegada.sigla == ((Bagagem)bb.ElementAt(bb.Count - 1)).dono.Passagem.Voo.LO_chegada.sigla)
                        ((Bagagem)bb.ElementAt(bb.Count - 1)).peso += cc.peso; 
                    else
                        bb.Add(cc);
                }
                ana.resultado[ana.resultado.Keys.ElementAt(i)] = bb;
            }
            if(ana.resultado.Values.Count>0)
            { 
            foreach (Bagagem b in ana.resultado.Values.ElementAt(0))
            {
                lbls.Add(b.dono.Passagem.Voo.DT_partida.ToString("MMMM"));
            }
            ana.generic_labels = lbls.ToArray();
            }
            return null;
            //throw new NotImplementedException();
        }
    }
}
