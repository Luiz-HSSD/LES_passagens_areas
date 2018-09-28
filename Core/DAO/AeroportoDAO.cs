using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    class AeroportoDAO : AbstractDAO
    {
        public AeroportoDAO() : base("aeroporto", "aero_id")
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
                Aeroporto Classe = (Aeroporto)entidade;
                string sql = null;

                if (Classe.Nome == null)
                {
                    Classe.Nome = "";
                }


                if (Classe.ID == 0)
                {
                    sql = "SELECT * FROM aeroporto ";
                }
                else
                {
                    sql = "SELECT * FROM aeroporto WHERE aero_id= :co";
                }
                pst = new NpgsqlCommand();

                pst.CommandText = sql;
                parameters = new NpgsqlParameter[] { new NpgsqlParameter("co", Classe.ID.ToString()) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Aeroporto p;
                while (vai.Read())
                {
                    p = new Aeroporto();
                    p.ID = Convert.ToInt32(vai["aero_id"]);
                    p.Nome = (vai["nome"].ToString());
                    p.sigla = (vai["sigla"].ToString());
                    p.lat =(float)Convert.ToDouble(vai["lat"]);
                    p.lng = (float)Convert.ToDouble(vai["lng"]);
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
            throw new NotImplementedException();
        }
    }
}
