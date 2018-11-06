using Core.Core;
using Core.DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Negocio
{
    public class Fazer_compra : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Venda Classe = (Venda)entidade;
            Classe.Total = 0;
            foreach (Viagem vg in Classe.Viagems)
            {
                Classe.Total += (decimal)(vg.qtd*vg.Valor_Unidade);
            }
            
                VendaDAO go  =  new VendaDAO();
            go.salvar(Classe);
            ViagemDAO vgDAO = new ViagemDAO();
            foreach(Viagem vg in Classe.Viagems)
            {
                
                //vg.Titular.
                vg.Compra.ID = Classe.ID;
                vgDAO.salvar(vg);

            }           
            return "sucesso!";
        }
    }
}
