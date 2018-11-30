using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class StatusDAO : AbstractDAO
    {
        public StatusDAO() : base("status", "id_status")
        {
        }

        public override void alterar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Status Classe = (Status)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "UPDATE Status SET bilhete_id = :nome , atual = :nom ,  IsDesembarque =:no  where id_status = :co ";
            parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("nome",Classe.Passageiro.ID),
                new NpgsqlParameter("nom",Classe.Atual.ID),
                new NpgsqlParameter("no",Classe.IsDesembarque),
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
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                pst.Dispose();
                Status Classe = (Status)entidade;
                string sql = null;
                if (Classe.Flg_completo)
                {
                    if (Classe.Atual.ID == 0 && Classe.Passageiro.passagem.Voo.LO_chegada.ID == 0 && Classe.ID == 0)
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep)";
                    }
                    else if (Classe.Atual.ID == 0 && Classe.Passageiro.passagem.Voo.LO_chegada.ID != 0)
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE (IsDesembarque=false AND pass_lo_partida=:codd) OR (IsDesembarque=true AND pass_lo_chegada= :codd)";
                    }
                    else if (Classe.Atual.ID != 0 && Classe.Passageiro.passagem.Voo.LO_chegada.ID == 0)
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE atual=:cod";
                    }
                    else if (Classe.ID != 0)
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE id_status = :dev";
                    }
                    else
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE ((IsDesembarque=false AND pass_lo_partida=:codd) OR (IsDesembarque=true AND pass_lo_chegada= :codd))and atual=:cod";
                    }
                }
                else
                {
                    if (Classe.Atual.ID == 0 && Classe.Passageiro.passagem.Voo.LO_chegada.ID == 0 && Classe.ID == 0)
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE id_dep!=0 and id_dep!=1";
                    }
                    else if (Classe.Atual.ID == 0 && Classe.Passageiro.passagem.Voo.LO_chegada.ID != 0)
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE (IsDesembarque=false AND pass_lo_partida=:codd) OR (IsDesembarque=true AND pass_lo_chegada= :codd) and id_dep!=0 and id_dep!=1";
                    }
                    else if (Classe.Atual.ID != 0 && Classe.Passageiro.passagem.Voo.LO_chegada.ID == 0)
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE atual=:cod and id_dep!=0 and id_dep!=1";
                    }
                    else if (Classe.ID != 0)
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE id_status = :dev";
                    }
                    else
                    {
                        sql = "SELECT bilhete.email , bilhete.bilhete_id , IsDesembarque, id_status, bilhete.nome , atual , Departamento.nome as dep_nome,atual, pass_id,pass_lo_chegada ,pass_lo_partida  ,avi_id  ,data_partida ,data_chegada, b.nome p_nome,c.nome c_nome, b.sigla p_sigla,c.sigla c_sigla FROM status join bilhete using(bilhete_id) join viagem using(viagem_id) join passagens using(pass_id) join aeroporto b on(b.aero_id=pass_lo_partida) join aeroporto c on(c.aero_id=pass_lo_chegada) join Departamento on(atual=id_dep) WHERE ((IsDesembarque=false AND pass_lo_partida=:codd) OR (IsDesembarque=true AND pass_lo_chegada= :codd))and atual=:cod and id_dep!=0 and id_dep!=1";
                    }
                }
                pst = new NpgsqlCommand();
                pst.CommandText = sql;
                parameters = new NpgsqlParameter[] { new NpgsqlParameter("dev", Classe.ID),  new NpgsqlParameter("cod",Classe.Atual.ID), new NpgsqlParameter("codd", Classe.Passageiro.passagem.Voo.LO_chegada.ID) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Status p;
                while (vai.Read())
                {
                    p = new Status();
                    p.ID = Convert.ToInt32(vai["id_status"]);
                    p.IsDesembarque = Convert.ToBoolean(vai["IsDesembarque"]);
                    p.Atual.ID = Convert.ToInt32(vai["atual"]);
                    p.Atual.Nome = (vai["dep_nome"].ToString());
                    p.Passageiro.ID = Convert.ToInt32(vai["bilhete_id"]);  
                    p.Passageiro.Nome = (vai["nome"].ToString());
                    p.Passageiro.Email = (vai["email"].ToString());
                    p.Passageiro.passagem.Voo.ID = Convert.ToInt32(vai["pass_id"]);
                    p.Passageiro.passagem.Voo.DT_chegada = Convert.ToDateTime(vai["data_chegada"]);
                    p.Passageiro.passagem.Voo.DT_partida = Convert.ToDateTime(vai["data_partida"]);
                    p.Passageiro.passagem.Voo.LO_chegada.ID = Convert.ToInt32(vai["pass_lo_chegada"].ToString());
                    p.Passageiro.passagem.Voo.LO_partida.ID = Convert.ToInt32(vai["pass_lo_partida"].ToString());
                    p.Passageiro.passagem.Voo.Aviao_v.ID = Convert.ToInt32(vai["avi_id"]);
                    if (Classe.ID == 0)
                    {
                        p.Passageiro.passagem.Voo.LO_chegada.Nome = (vai["c_nome"].ToString());
                        p.Passageiro.passagem.Voo.LO_partida.Nome = (vai["p_nome"].ToString());
                        p.Passageiro.passagem.Voo.LO_chegada.sigla = (vai["c_sigla"].ToString());
                        p.Passageiro.passagem.Voo.LO_partida.sigla = (vai["p_sigla"].ToString());
                    }
                    Classes.Add(p);
                }
                vai.Close();
                connection.Close();
                return Classes;
            }
            catch (NpgsqlException ora)
            {
                vai.Close();
                connection.Close();
                throw ora;

            }
        }

        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Status Classe = (Status)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "insert into Status ( bilhete_id , atual  ,  IsDesembarque ) values (  :nome , :nom, :no )";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.Passageiro.ID),
                        new NpgsqlParameter("nom",Classe.Atual.ID),
                        new NpgsqlParameter("no",Classe.IsDesembarque)
                    };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
        }
    }
}
