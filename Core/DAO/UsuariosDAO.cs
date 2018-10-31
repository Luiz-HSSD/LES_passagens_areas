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
            Usuarios Classe = (Usuarios)entidade;
            pst.CommandText = "insert into usuarios ( login , password_user  )   values ( :nome, :nom ) returning id_user";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome" , Classe.Login),
                        new NpgsqlParameter("nom"  , Classe.Password)
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
