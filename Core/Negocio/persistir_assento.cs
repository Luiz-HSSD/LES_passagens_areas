using Core.Core;
using Core.DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Negocio
{
    public class persistir_assento : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Passagens Classe = (Passagens)entidade;
            AssentoDAO DAO = new AssentoDAO();
            for (int i = 1; i <= Classe.QTD; i++)
                DAO.salvar(new Assento(new Check_in()) { tipo =Classe.Tipo,Tag="A"+i,viagem =new Passagens() { ID =Classe.ID }  });
            return null;
        }
    }
}
