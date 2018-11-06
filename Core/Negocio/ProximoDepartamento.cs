﻿using Core.Core;
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
            var devil = Classe.Atual.ID;
            bool go = false;
            List<EntidadeDominio> ld= depDAO.consultar(Classe.Passageiro.passagem.Voo);
            foreach (Departamento d in ld)
            {
                if (go)
                {
                    Classe.Atual.ID = d.ID;
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
                        Classe.Atual.ID = 0;
                    else
                    {
                        Classe.Atual.ID = ld.ElementAt(0).ID;
                        Classe.IsDesembarque = true;
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