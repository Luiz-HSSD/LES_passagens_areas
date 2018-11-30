using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Core.DAO;
using Dominio;

namespace Core.Negocio
{
    public class Fazer_Barrado : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Barrado bar = (Barrado)entidade;
            StatusDAO stDAO = new StatusDAO();
            bar.Passageiro =(Status) stDAO.consultar(new Status() { ID= bar.Passageiro.ID })[0];
            bar.Passageiro.Atual.ID=0;            
            stDAO.alterar(bar.Passageiro);
            return null;
        }
    }
}
