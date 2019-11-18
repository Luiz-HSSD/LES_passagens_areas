using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class Car_CliDAO : AbstractDAO
    {
        public Car_CliDAO() : base("", "")
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
            Cliente Classe = (Cliente)entidade;
            foreach (Cartao_Credito a in Classe.Cartoes)
            {
                pst.CommandText = "insert into car_cli ( id_cli , id_car   )   values ( :nomee, :nome  )";
                parameters = new NpgsqlParameter[]
                {
                        new NpgsqlParameter("nomee" , Classe.ID),
                        new NpgsqlParameter("nome" ,  a.ID)
                };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                pst.ExecuteNonQuery();
            }
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            return;
        }
    }
}
