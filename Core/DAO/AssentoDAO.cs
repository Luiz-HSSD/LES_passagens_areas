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
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.Dispose();
                Assento  Classe = (Assento)entidade;
                string sql = null;

                if (Classe.Tag == null)
                {
                    Classe.Tag = "";
                }


                if (Classe.viagem.ID == 0)
                {
                    sql = "SELECT * FROM Assento ";
                }
                else
                {
                    sql = "SELECT * FROM Assento WHERE pass_id= :co and class_id= :cod";
                }
                pst = new NpgsqlCommand();

                pst.CommandText = sql;
                parameters = new NpgsqlParameter[] { new NpgsqlParameter("co", Classe.viagem.ID), new NpgsqlParameter("cod", Classe.tipo.ID) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Assento p;
                while (vai.Read())
                {
                    p = new Assento(new Check_in());
                    p.ID = Convert.ToInt32(vai["assento_id"]);
                    p.Tag = (vai["tag"].ToString());
                    p.viagem.ID = Convert.ToInt32(vai["pass_id"]);
                    p.tipo.ID = Convert.ToInt32(vai["class_id"]);
                    if(vai["chck_in_id"]!=DBNull.Value)
                    p.ocupante.ID = Convert.ToInt32(vai["chck_in_id"]);
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
