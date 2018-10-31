using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Dominio;
using Core.DAO;

namespace Core.Negocio
{
    public class Fazer_Cliente : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Cliente Classe = (Cliente)entidade;
            GerarBandeira gb = new GerarBandeira();
            EnderecoDAO vgDAO = new EnderecoDAO() { };
            vgDAO.salvar(Classe.Endereco);
            UsuariosDAO goDAO = new UsuariosDAO() { };
            goDAO.salvar(Classe.usuario);
            Cartao_CreditoDAO carDAO = new Cartao_CreditoDAO() { };
            foreach (Cartao_Credito a in Classe.Cartoes)
            {
                gb.processar(a);
                carDAO.salvar(a);
            }
            ClienteDAO cliDAO = new ClienteDAO();
            cliDAO.salvar(Classe);
            Car_CliDAO cart_cliDAO = new Car_CliDAO();
            cart_cliDAO.salvar(Classe);
            return "sucesso!";
            //return null;

        }
    }
}
