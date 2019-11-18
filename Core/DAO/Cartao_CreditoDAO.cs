using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    class Cartao_CreditoDAO : AbstractDAO
    {
        public Cartao_CreditoDAO() : base("cartao_credito", "id_car")
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
            Cartao_Credito Classe = (Cartao_Credito)entidade;
            pst.CommandText = "insert into cartao_credito ( numero , ccv  , nome_car ,validade , id_band  )   values ( :nomee, :nome, :nom , :cpf, :rg  ) returning id_car";
            parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("nomee" , Classe.Numero),
                new NpgsqlParameter("nome" ,  Classe.CCV),
                new NpgsqlParameter("nom"  ,  Classe.Nome_Titular),
                new NpgsqlParameter("cpf"  ,  Classe.Validade),
                new NpgsqlParameter("rg"  ,   Classe.Bandeira.ID)
            };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            Classe.ID = (int)pst.ExecuteScalar();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            return;
        }
    }
}
