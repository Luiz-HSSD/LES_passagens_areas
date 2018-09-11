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
    public class AviaoDAO : AbstractDAO
    {
        
        public AviaoDAO() : base( "aviao", "avi_id")
        {

        }
        


        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Aviao Classe = (Aviao)entidade;
            pst.CommandText = "insert into aviao ( avi_nome ) values (  :nome )";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.Nome)
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
                Aviao Classe = (Aviao)entidade;
                pst.CommandText = "UPDATE aviao SET avi_nome=:nome WHERE avi_id=:co";
                parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.Nome),
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
                Aviao Classe = (Aviao)entidade;
                string sql = null;

                if (Classe.Nome == null)
                {
                    Classe.Nome="";
                }
                

                if (Classe.ID == 0)
                {
                    sql = "SELECT * FROM aviao ";
                }
                else
                {
                    sql = "SELECT * FROM aviao WHERE avi_id= :co";
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
                Aviao p;
                while (vai.Read())
                {
                    p = new Aviao();
                    p.ID = Convert.ToInt32(vai["avi_id"]);
                    p.Nome=(vai["avi_nome"].ToString());
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