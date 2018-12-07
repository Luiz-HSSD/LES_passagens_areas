using Core.Core;
using Core.DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Negocio
{
    class ProximoDepartamento : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Status Classe = (Status)entidade;
            StatusDAO goo = new StatusDAO();
            List <EntidadeDominio> develop= goo.consultar(Classe);
            if (develop.Count > 0)
                Classe = (Status)develop.ElementAt(0);
            DepartamentoDAO depDAO = new DepartamentoDAO();
            BarradoDAO barDAO = new BarradoDAO();
            var devil = Classe.Atual.ID;
            bool go = false;
            Agradecimento agra = new Agradecimento();
            List<EntidadeDominio> ld= depDAO.consultar(new Departamento() { Pass = Classe.Passageiro.passagem.Voo });
            foreach (Departamento d in ld)
            {
                if (go)
                {
                    Classe.Atual.ID = d.ID;
                    break;
                }
                if (Classe.Atual.ID ==d.ID)
                {
                    go = true;
                }
            }
            if (ld.Count > 0)
            {
                if(ld.ElementAt(ld.Count-1).ID==Classe.Atual.ID && ld.ElementAt(ld.Count - 1).ID==devil)
                {
                    if (Classe.IsDesembarque)
                    {
                        
                            Classe.Atual.ID = 1;
                            agra.processar(Classe.Passageiro);
                         
                    }
                    else
                    {
                        if(Classe.Passageiro.passagem.Voo.DT_partida>=DateTime.Now)
                        { 
                            Classe.Atual.ID = ld.ElementAt(0).ID;
                            Classe.IsDesembarque = true;
                        }
                        else
                        {
                            Classe.Atual.ID = 0;
                            barDAO.salvar(new Barrado() {Categoria =new Motivo() {ID=2 },Passageiro=new Status() {ID=Classe.ID },Causa="passageiro atrasado gerado automaticamente (LES_Passagens_aereas)"  });
                        }
                    }
                }
            }
            entidade.ID = Classe.ID;
            ((Status)entidade).IsDesembarque = Classe.IsDesembarque;
            ((Status)entidade).Atual.ID = Classe.Atual.ID;
            ((Status)entidade).Passageiro.ID = Classe.Passageiro.ID;
            return null;
        }
    }
}
