﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

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
                Passagens Classe = (Passagens)entidade;
                string sql = null;
                


                if (Classe.ID == 0)
                {
                    sql = "select * from Departamento join dep_pass using(id_dep)  ";
                }
                else
                {
                    sql = "select * from Departamento join dep_pass using(id_dep) WHERE pass_id = :co ";
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
