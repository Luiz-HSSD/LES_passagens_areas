using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using MySql.Data.MySqlClient;

namespace Core.DAO
{
    public class DepartamentoDAO : AbstractDAO
    {
        public DepartamentoDAO() : base("", "")
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
                Departamento Classe = (Departamento)entidade;
                string sql = null;
                


                if (Classe.Pass.ID == 0)
                {
                    sql = "select * from Departamento join dep_pass using(id_dep)  ";
                }
                else
                {
                    sql = "select * from Departamento join dep_pass using(id_dep) WHERE pass_id = :co ";
                }
                pst = new MySqlCommand();
                pst.CommandText = sql;
                parameters = new MySqlParameter[] { new MySqlParameter("co", Classe.Pass.ID) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Departamento p;
                while (vai.Read())
                {
                    p = new Departamento();
                    p.ID = Convert.ToInt32(vai["id_dep"]);
                    p.Nome= (vai["nome"].ToString());
                    Classes.Add(p);
                }
                vai.Close();
                connection.Close();
                return Classes;
            }
            catch (MySqlException ora)
            {
                throw ora;
            }
        }

        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Passagens Classe = (Passagens)entidade;
            foreach (Departamento d  in Classe.Departamentos)
            { 
                pst.Dispose();
                pst = new MySqlCommand();
                pst.CommandText = "insert into dep_pass ( pass_id, id_dep ) values (  :no,:nomm )";
                parameters = new MySqlParameter[]
                {
                    new MySqlParameter("no",Classe.ID),
                    new MySqlParameter("nomm",d.ID)
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
