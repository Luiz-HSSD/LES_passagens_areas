using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using MySql.Data.MySqlClient;

namespace Core.DAO
{
    public class VendaDAO : AbstractDAO
    {
        public VendaDAO() : base("vendas", "id_ven")
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
               Venda Classe = (Venda)entidade;
                string sql = null;

                if (Classe.Cliente_prop.Nome == null)
                {
                    Classe.Cliente_prop.Nome = "";
                }


                if (Classe.Cliente_prop.ID == 0)
                {
                    sql = "SELECT * FROM vendas ";
                }
                else
                {
                    sql = "SELECT * FROM vendas WHERE id_cli= @co";
                }
                pst = new MySqlCommand();

                pst.CommandText = sql;
                parameters = new MySqlParameter[] { new MySqlParameter("co", Classe.Cliente_prop.ID) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Venda p;
                while (vai.Read())
                {
                    p = new Venda();
                    p.ID = Convert.ToInt32(vai["id_ven"]);
                    p.Cliente_prop.ID = Convert.ToInt32(vai["id_ven"]);
                    p.Total    = Convert.ToDecimal(vai["id_cli"]);
                    p.Forma_pagamento.ID = Convert.ToInt32(vai["id_car"]);
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
            Venda Classe = (Venda)entidade;
            pst.Dispose();
            pst = new MySqlCommand();
            pst.CommandText = "insert into vendas (   id_cli  , preco     ,    id_car   ) values (  @no,@nomm,@nom ) returning id_ven";
            parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("no",Classe.Cliente_prop.ID),
                        new MySqlParameter("nomm",Classe.Total),
                        new MySqlParameter("nom",Classe.Forma_pagamento.ID)
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
