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
        public Check_inDAO() : base("check_in", "chck_in_id")
        {
        }

        public override void alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.Dispose();
                Check_in Classe = (Check_in)entidade;
                string sql = null;
                


                if (Classe.ID == 0)
                {
                    sql = "SELECT * FROM check_in ";
                }
                else
                {
                    sql = "SELECT * FROM check_in WHERE chck_in_id= :co";
                }
                pst = new NpgsqlCommand();

                pst.CommandText = sql;
                parameters = new NpgsqlParameter[] { new NpgsqlParameter("co", Classe.ID) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Check_in p;
                while (vai.Read())
                {
                    p = new Check_in();
                    p.ID = Convert.ToInt32(vai["chck_in_id"]);
                    if (vai["viagem_id"] != DBNull.Value)
                        p.Passagem.ID = Convert.ToInt32(vai["viagem_id"]);
                    if (vai["bilhete_id"] != DBNull.Value)
                        p.Entrada.ID = Convert.ToInt32(vai["bilhete_id"]);
                    Classes.Add(p);
                }
                vai.Close();
                connection.Close();
                return Classes;
            }
            catch (NpgsqlException ora)
            {
                throw ora;
            }
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
            pst.CommandText = "SELECT CURRVAL(pg_get_serial_sequence('check_in', 'chck_in_id'))";
            vai= pst.ExecuteReader();
            while (vai.Read())
            {
                Classe.ID= Convert.ToInt32(vai[0]);
            }
            vai.Close();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            
            connection.Close();
            return;
        }
    }
}
