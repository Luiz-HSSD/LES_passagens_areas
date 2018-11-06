using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class VendaDAO : AbstractDAO
    {
        public VendaDAO() : base("vendas", "id_ven")
        {
        }

        public override void alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Venda Classe = (Venda)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "insert into assento (        id_cli  , preco     ,    id_car   ) values (  :no,:nomm,:nome,:nom ) returning id_ven";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("no",Classe.Cliente_prop.ID),
                        new NpgsqlParameter("nomm",Classe.Total),
                        new NpgsqlParameter("nom",Classe.Forma_pagamento.ID)
                    };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            return;
        }
    }
}
