using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using MySql.Data.MySqlClient;

namespace Core.DAO
{
    public class Check_inDAO : AbstractDAO
    {
        public Check_inDAO() : base("check_in", "chck_in_id")
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
                Check_in Classe = (Check_in)entidade;
                string sql = null;
                


                if (Classe.ID == 0 && !Classe.Flg)
                {
                    sql = "SELECT * FROM check_in ";
                }
                else if (Classe.Flg)
                {
                    sql = "select  passagens.data_partida , c.sigla as c_sigla  ,b.sigla as p_sigla   from  check_in inner join viagem using (viagem_id) inner join passagens using (pass_id) join aeroporto b on(b.aero_id= pass_lo_partida) join aeroporto c on(c.aero_id= pass_lo_chegada) where passagens.data_partida >= @dat AND passagens.data_partida < @datt order by passagens.data_partida asc";
                }
                else
                {
                    sql = "SELECT * FROM check_in WHERE chck_in_id= @co";
                }
                pst = new MySqlCommand();

                pst.CommandText = sql;
                parameters = new MySqlParameter[] { new MySqlParameter("co", Classe.ID), new MySqlParameter("dat", Classe.Passagem.Voo.DT_partida), new MySqlParameter("datt", Classe.Passagem.Voo.DT_chegada) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Check_in p;
                while (vai.Read())
                {
                    p = new Check_in();
                    if (Classe.Flg)
                    {
                        p.Passagem.Voo.LO_partida.sigla = (vai["p_sigla"].ToString());
                        p.Passagem.Voo.LO_chegada.sigla = (vai["c_sigla"].ToString());
                        p.Passagem.Voo.DT_partida = Convert.ToDateTime(vai["data_partida"]);
                        Classes.Add(p);
                        continue;
                    }
                    p.ID = Convert.ToInt32(vai["chck_in_id"]);
                    if (vai["viagem_id"] != DBNull.Value)
                        p.Passagem.ID = Convert.ToInt32(vai["viagem_id"]);
                    if (vai["bilhete_id"] != DBNull.Value)
                        p.Entrada.ID = Convert.ToInt32(vai["bilhete_id"]);
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
            Check_in Classe = (Check_in)entidade;
            pst.CommandText = "insert into check_in ( viagem_id ,bilhete_id ) values (  @nome , @nom ) ";
            parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("nome",Classe.Passagem.ID),
                        new MySqlParameter("nom",Classe.Entrada.ID),

                    };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            MySqlParameter Out = new MySqlParameter("cod", Classe.ID);
            Out.Direction = ParameterDirection.ReturnValue;
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            pst.ExecuteNonQuery();
            pst.CommandText = "SELECT CURRVAL(pg_get_serial_sequence('check_in', 'chck_in_id'))";
            vai= pst.ExecuteReader();
            while (vai.Read())
            {
                Classe.ID= Convert.ToInt32(vai[0]);
            }
            vai.Close();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            
            connection.Close();
            return;
        }
    }
}
