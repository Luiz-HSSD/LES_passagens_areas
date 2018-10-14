using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Dominio;
using Core.Utils;
using System.Data;

namespace Core.DAO
{
    public class ClasseDAO : AbstractDAO
    {
        
        public  ClasseDAO() : base( "Classe", "class_id")
        {

        }
        


        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Classe Classe = (Classe)entidade;
            pst.CommandText = "insert into Classe ( class_nome,peso ) values (  :nome ,:nom )";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.Nome),
                        new NpgsqlParameter("nom",Classe.Peso)
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

        public override void alterar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                Classe Classe = (Classe)entidade;
                pst.CommandText = "UPDATE Classe SET class_nome=:nome , peso=:nom  WHERE class_id=:co";
                parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.Nome),
                        new NpgsqlParameter("nom",Classe.Peso),
                        new NpgsqlParameter("co",Classe.ID)
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
            catch (Exception e)
            {
                throw e;
            }
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.Dispose();
                Classe Classe = (Classe)entidade;
                string sql = null;

                if (Classe.Nome == null)
                {
                    Classe.Nome="";
                }
                

                if (Classe.ID == 0)
                {
                    sql = "SELECT * FROM Classe ";
                }
                else
                {
                    sql = "SELECT * FROM Classe WHERE class_id= :co";
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
                Classe p;
                while (vai.Read())
                {
                    p = new Classe();
                    p.ID = Convert.ToInt32(vai["class_id"]);
                    p.Peso = Convert.ToDouble(vai["peso"]);
                    p.Nome=(vai["class_nome"].ToString());
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


    }
}