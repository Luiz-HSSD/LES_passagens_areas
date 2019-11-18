using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class UsuariosDAO : AbstractDAO
    {
        public UsuariosDAO() : base("usuarios", "id_user")
        {

        }

        public override void alterar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Usuarios Classe = (Usuarios)entidade;
            pst.CommandText = "update usuarios set  login = :nome , password_user = :nom , permisao = :nomm WHERE id_user = :co ";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome" , Classe.Login),
                        new NpgsqlParameter("nom"  , Classe.Password),
                        new NpgsqlParameter("nomm"  , Classe.Permisao),
                        new NpgsqlParameter("co"  , Classe.ID)
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

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.Dispose();
                Usuarios Classe = (Usuarios)entidade;
                string sql = null;

                if (Classe.Login == null)
                {
                    Classe.Login = "";
                }
                if (Classe.ID == 0 && string.IsNullOrEmpty(Classe.Login) && Classe.Permisao == 0)
                {
                    sql = "SELECT * FROM usuarios ";
                }
                else if (!string.IsNullOrEmpty(Classe.Login))
                {
                    sql = "SELECT * FROM usuarios WHERE login = :lo and password_user = :pas ";
                }
                else if (Classe.Permisao != 0)
                {
                    sql = "SELECT * FROM usuarios WHERE permisao >= :per ";
                }
                else
                {
                    sql = "SELECT * FROM usuarios WHERE id_user = :co";
                }
                pst = new NpgsqlCommand();

                pst.CommandText = sql;
                parameters = new NpgsqlParameter[]
                {
                    new NpgsqlParameter("co", Classe.ID),
                    new NpgsqlParameter("lo", Classe.Login),
                    new NpgsqlParameter("pas", Classe.Password),
                    new NpgsqlParameter("per", Classe.Permisao)
                };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Usuarios p;
                while (vai.Read())
                {
                    p = new Usuarios();
                    p.ID = Convert.ToInt32(vai["id_user"]);
                    p.Login = (vai["login"].ToString());
                    p.Password = (vai["password_user"].ToString());
                    p.Permisao = Convert.ToInt32(vai["permisao"]);
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
            Usuarios Classe = (Usuarios)entidade;
            pst.CommandText = "insert into usuarios ( login , password_user, permisao  )   values ( :nome, :nom , :nomm ) returning id_user";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome" , Classe.Login),
                        new NpgsqlParameter("nom"  , Classe.Password),
                        new NpgsqlParameter("nomm"  , Classe.Permisao)
                    };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            Classe.ID = (int)pst.ExecuteScalar();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            return;
        }
    }
}
