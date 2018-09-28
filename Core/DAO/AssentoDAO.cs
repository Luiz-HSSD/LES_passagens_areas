using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class AssentoDAO : AbstractDAO
    {
        public AssentoDAO() : base("assento", "assento_id")
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
            Assento Classe = (Assento)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "insert into assento ( tag ,class_id,chck_in_id,pass_id ) values (  :no,:nomm,:nome,:nom )";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("no",Classe.Tag),
                        new NpgsqlParameter("nomm",Classe.tipo.ID),
                        new NpgsqlParameter("nome",Classe.ocupante.ID),
                        new NpgsqlParameter("nom",Classe.viagem.ID)
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
