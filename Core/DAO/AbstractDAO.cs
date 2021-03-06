﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;
using Dominio;
using Npgsql;
using Core.Utils;
using System.Data;
using NpgsqlTypes;

namespace Core.DAO
{
    public abstract class AbstractDAO : IDAO
    {
        protected string table;
        protected string id_table;
        protected NpgsqlConnection connection = Conexao.getconnection();
        protected NpgsqlCommand pst = new NpgsqlCommand();
        protected NpgsqlParameter[] parameters;
        protected NpgsqlDataReader vai;
        protected bool ctrlTransaction = true;
        public AbstractDAO(string table, string id_table)
        {
            this.table = table;
            this.id_table = id_table;
        }

        public AbstractDAO(NpgsqlConnection connection, string table, string id_table)
        {
            this.table = table;
            this.id_table = id_table;
            this.connection = connection;
        }



        public abstract void salvar(EntidadeDominio entidade);

        public abstract void alterar(EntidadeDominio entidade);

        public abstract List<EntidadeDominio> consultar(EntidadeDominio entidade);


        public virtual void excluir(EntidadeDominio entidade)
        {

            try
            {
                connection = Conexao.getconnection();
                connection.Open();
                pst.CommandText = "DELETE FROM " + table + " WHERE " + id_table + " =:dh ";
                parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("dh",entidade.ID)
                    };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.ExecuteNonQuery();
                pst.CommandText = "commit work";
                pst.ExecuteNonQuery();
                if (ctrlTransaction)
                    connection.Close();


            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
