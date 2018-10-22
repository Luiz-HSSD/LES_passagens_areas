using System;
using System.Linq;
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
            Viagem vg = new Viagem() {Passageiros=new List<Bilhete>() {chk_in.Entrada } };
            ViagemDAO vgDAO = new ViagemDAO() { };
            chk_in.Passagem=(Viagem)vgDAO.consultar(vg).ElementAt(0);
            Check_inDAO chkdao = new Check_inDAO();
            chkdao.salvar(chk_in);
            BagagemDAO bagagemDAO = new BagagemDAO();
            foreach (Bagagem a in chk_in.Bagagem)
            {
                a.dono = chk_in;
                bagagemDAO.salvar(a);
            }
            AssentoDAO assendao = new AssentoDAO();
            var luiz = (Assento)assendao.consultar(chk_in.Ocupante).ElementAt(0);
            chk_in.Ocupante.Tag = luiz.Tag;
            chk_in.Ocupante.tipo.ID = luiz.tipo.ID;
            chk_in.Ocupante.viagem.ID = luiz.viagem.ID;
            chk_in.Ocupante.ocupante = chk_in;
            assendao.alterar(chk_in.Ocupante);
            return "sucesso!";
        }
    }
}
