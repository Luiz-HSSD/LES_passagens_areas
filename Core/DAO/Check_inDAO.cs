using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class Check_inDAO : AbstractDAO
    {
        public Check_inDAO() : base("check_in", "check_in_id")
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
            Check_in Classe = (Check_in)entidade;
            pst.CommandText = "insert into check_in ( viagem_id ,bilhete_id ) values (  :nome , :nom ) ";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.Passagem.ID),
                        new NpgsqlParameter("nom",Classe.Entrada.ID),

                    };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            NpgsqlParameter Out = new NpgsqlParameter("cod", Classe.ID);
            Out.Direction = ParameterDirection.ReturnValue;
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "SELECT CURRVAL(pg_get_serial_sequence('check_in', 'check_in_id'))";
            vai= pst.ExecuteReader();
            while (vai.Read())
            {
                Classe.ID= Convert.ToInt32(vai[0]);
            }
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            
            Classe.ID = Convert.ToInt32(Out.Value);
            connection.Close();
            return;
        }
    }
}
