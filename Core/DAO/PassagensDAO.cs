﻿using System;
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
    public class PassagensDAO : AbstractDAO
    {
        
        public PassagensDAO() : base("passagens", "pass_id")
        {

        }
        


        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Passagens Classe = (Passagens)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "insert into passagens ( class_id, avi_id,data_chegada,data_partida,pass_lo_chegada,pass_lo_partida,qtd ) values (  :nome,:nom,:no,:nod,:node,:nodei,:dev )";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.Tipo.ID),
                        new NpgsqlParameter("nom",Classe.Aviao_v.ID),
                        new NpgsqlParameter("no",Classe.DT_chegada),
                        new NpgsqlParameter("nod",Classe.DT_partida),
                        new NpgsqlParameter("node",Classe.LO_chegada.ID),
                        new NpgsqlParameter("nodei",Classe.LO_partida.ID),
                        new NpgsqlParameter("dev",Classe.QTD)
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
                Passagens Classe = (Passagens)entidade;
                pst.CommandText = "UPDATE passagens SET class_id=:nome, avi_id=:nom ,data_chegada=:no ,data_partida=:nod ,pass_lo_chegada=:node,pass_lo_partida=:nodei,qtd=:dev WHERE pass_id=:co";
                parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.Tipo.ID),
                        new NpgsqlParameter("nom",Classe.Aviao_v.ID),
                        new NpgsqlParameter("no",Classe.DT_chegada),
                        new NpgsqlParameter("nod",Classe.DT_partida),
                        new NpgsqlParameter("node",Classe.LO_chegada.ID),
                        new NpgsqlParameter("nodei",Classe.LO_partida.ID),
                        new NpgsqlParameter("dev",Classe.QTD),
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
                Passagens Classe = (Passagens)entidade;
                string sql = null;
                if (Classe.ID == 0 && Classe.LO_chegada.ID == 0)
                {
                    sql = "SELECT * FROM passagens ";
                }
                else if (Classe.LO_chegada.ID != 0)
                {
                    sql = "SELECT * FROM passagens join aeroporto on(aero_id=pass_lo_chegada) WHERE pass_lo_partida = :cod";
                }
                else
                {
                    sql = "SELECT * FROM passagens  WHERE pass_id= :co";
                }
                pst = new NpgsqlCommand();

                pst.CommandText = sql;
                parameters = new NpgsqlParameter[] { new NpgsqlParameter("co", Classe.ID), new NpgsqlParameter("cod", Classe.LO_chegada.ID) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Passagens p;
                while (vai.Read())
                {
                    p = new Passagens();
                    p.ID = Convert.ToInt32(vai["pass_id"]);
                    p.DT_chegada = Convert.ToDateTime(vai["data_chegada"]);
                    p.DT_partida = Convert.ToDateTime(vai["data_partida"]);
                    p.LO_chegada.ID= Convert.ToInt32(vai["pass_lo_chegada"].ToString());
                    p.LO_partida.ID = Convert.ToInt32(vai["pass_lo_partida"].ToString());
                    p.Tipo.ID = Convert.ToInt32(vai["class_id"]);
                    p.Aviao_v.ID = Convert.ToInt32(vai["avi_id"]);
                    if (Classe.LO_chegada.ID != 0)
                    {
                        p.LO_chegada.ID = Convert.ToInt32(vai["aero_id"]);
                        p.LO_chegada.Nome = (vai["nome"].ToString());
                        p.LO_chegada.sigla = (vai["sigla"].ToString());
                        p.LO_chegada.lat = (float)Convert.ToDouble(vai["lat"]);
                        p.LO_chegada.lng = (float)Convert.ToDouble(vai["lng"]);
                    }
                        if (vai["qtd"]!=  DBNull.Value)
                    p.QTD = Convert.ToInt32(vai["qtd"]);
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