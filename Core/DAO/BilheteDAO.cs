using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class BilheteDAO : AbstractDAO
    {
        public BilheteDAO() : base("bilhete", "bilhete_id")
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
                Bilhete Classe = (Bilhete)entidade;
                string sql = null;

                if (Classe.Nome == null)
                {
                    Classe.Nome="";
                }
                

                if (Classe.ID == 0)
                {
                    sql = "SELECT * FROM bilhete ";
                }
                else
                {
                    sql = "SELECT * FROM bilhete WHERE class_id= :co";
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
                Bilhete p;
                while (vai.Read())
                {
                    p = new Bilhete();
                    p.ID = Convert.ToInt32(vai["bilhete_id"]);
                    p.Nome=(vai["nome"].ToString());
                    p.cpf= (vai["cpf"].ToString());
                    p.RG = (vai["rg"].ToString());
                    p.Sexo =Convert.ToBoolean(vai["sexo"]);
                    p.passagem.ID = Convert.ToInt32(vai["sexo"]);
                    Classes.Add(p);
                }
                vai.Close();
                connection.Close();
                return Classes;
            }
            catch(NpgsqlException ora)
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
