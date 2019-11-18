using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class MotivoDAO : AbstractDAO
    {
        public MotivoDAO() : base("motivo", "id_mot")
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
                Motivo Classe = (Motivo)entidade;
                string sql = null;

                if (Classe.Nome == null)
                {
                    Classe.Nome = "";
                }


                if (Classe.Dep.ID == 0)
                {
                    sql = "SELECT * FROM motivo ";
                }
                else
                {
                    sql = "SELECT * FROM motivo join dep_mot using(id_mot) WHERE id_dep = :co";
                }
                pst = new NpgsqlCommand();

                pst.CommandText = sql;
                parameters = new NpgsqlParameter[] { new NpgsqlParameter("co", Classe.Dep.ID) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Motivo p;
                while (vai.Read())
                {
                    p = new Motivo();
                    p.ID = Convert.ToInt32(vai["id_mot"]);
                    p.Nome = (vai["nome"].ToString());
                    if (Classe.Dep.ID != 0)
                        p.Dep.ID = Convert.ToInt32(vai["id_dep"]);
                    Classes.Add(p);
                }
                vai.Close();
                connection.Close();
                return Classes;
            }
            catch (NpgsqlException ora)
            {
                vai.Close();
                connection.Close();
                throw ora;
            }


        }


        public override void salvar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }
    }
}
