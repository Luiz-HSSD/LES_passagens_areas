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
            string result="";
            ValidarCompra vd =new ValidarCompra();
            string vdv = vd.processar(entidade);
            if (!string.IsNullOrEmpty(vdv))
                result += vdv;
            Classe.Total = 0;
            if (result != "")
                return result;
            foreach (Viagem vg in Classe.Viagems)
            {
                Classe.Total += (decimal)(vg.qtd*vg.Valor_Unidade);
            }
            VendaDAO go  =  new VendaDAO();
            go.salvar(Classe);
            ViagemDAO vgDAO = new ViagemDAO();
            BilheteDAO biDAO = new BilheteDAO();
            foreach (Viagem vg in Classe.Viagems)
            {             
                vg.Compra.ID = Classe.ID;
                vgDAO.salvar(vg);
                foreach(Bilhete b in vg.Passageiros)
                {
                    b.passagem.ID = vg.ID;
                    biDAO.salvar(b);
                }
            }

            
                return "sucesso!";

            
        }
    }
}
