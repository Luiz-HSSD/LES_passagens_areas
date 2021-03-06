﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class BarradoDAO : AbstractDAO
    {
        public BarradoDAO() : base("barrado", "id_bar")
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
            Barrado Classe = (Barrado)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "insert into barrado (  id_mot , id_status , causa ) values (  :no, :nome, :nomm )";
            parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("no",Classe.Categoria.ID),
                new NpgsqlParameter("nome",Classe.Passageiro.ID),
                new NpgsqlParameter("nomm",Classe.Causa)
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
