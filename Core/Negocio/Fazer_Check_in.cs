using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Dominio;
using Core.DAO;

namespace Core.Negocio
{
    public class Fazer_Check_in : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Check_in chk_in = (Check_in)entidade;
            Check_inDAO chkdao = new Check_inDAO();
            chkdao.salvar(chk_in);
            BagagemDAO bagagemDAO = new BagagemDAO();
            foreach(Bagagem a in chk_in.Bagagem)
            bagagemDAO.salvar(a);
            AssentoDAO assendao = new AssentoDAO();
            assendao.alterar(chk_in.Ocupante);
            return "sucesso!";
        }
    }
}
