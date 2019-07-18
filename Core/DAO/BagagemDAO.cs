using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using MySql.Data.MySqlClient;

namespace Core.DAO
{
    class BagagemDAO : AbstractDAO
    {
        public BagagemDAO() : base("Bagagem", "Bagagem_id")
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
                Bagagem Classe = (Bagagem)entidade;
                string sql = null;
                if (Classe.ID == 0 && !Classe.Flg && Classe.dono.ID==0)
                {
                    sql = "SELECT * FROM Bagagem ";
                }
                else if (Classe.Flg)
                {
                    sql= "select bagagem.bagagem_id, bagagem.peso , passagens.data_partida , c.sigla as c_sigla  ,b.sigla as p_sigla   from bagagem inner join check_in using (chck_in_id) inner join viagem using (viagem_id) inner join passagens using (pass_id) join aeroporto b on(b.aero_id= pass_lo_partida) join aeroporto c on(c.aero_id= pass_lo_chegada) where passagens.data_partida >= @dat AND passagens.data_partida < @datt order by passagens.data_partida asc";
                }
                else if(Classe.dono.ID == 0)
                {
                    sql = "SELECT * FROM Bagagem WHERE Bagagem_id= @co";
                }
                else
                {
                    sql = "SELECT * FROM Bagagem WHERE chck_in_id = @cod";
                }
                pst = new MySqlCommand();

                pst.CommandText = sql;
                parameters = new MySqlParameter[] { new MySqlParameter("co", Classe.ID), new MySqlParameter("cod", Classe.dono.ID), new MySqlParameter("dat", Classe.dono.Passagem.Voo.DT_partida), new MySqlParameter("datt", Classe.dono.Passagem.Voo.DT_chegada) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Check_in chk = new Check_in();
                Bagagem p;
                while (vai.Read())
                {
                    p = new Bagagem(new Check_in());
                    p.ID = Convert.ToInt32(vai["bagagem_id"]);
                    p.peso = Convert.ToDouble(vai["peso"]);
                    if (Classe.Flg)
                    {
                        p.dono.Passagem.Voo.LO_partida.sigla = (vai["p_sigla"].ToString());
                        p.dono.Passagem.Voo.LO_chegada.sigla = (vai["c_sigla"].ToString());
                        p.dono.Passagem.Voo.DT_partida = Convert.ToDateTime(vai["data_partida"]);
                        Classes.Add(p);
                        continue;
                    }
                    p.comprimento = Convert.ToInt32(vai["comprimento"]);
                    p.largura = Convert.ToInt32(vai["largura"]);
                    p.altura = Convert.ToInt32(vai["altura"]);                   
                    p.dono.ID = Convert.ToInt32(vai["chck_in_id"]);                    
                    
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
            Bagagem Classe = (Bagagem)entidade;
            pst.Dispose();
            pst = new MySqlCommand();
            pst.CommandText = "insert into Bagagem ( comprimento, largura,altura,peso,chck_in_id ) values (  :nome,:nom,:no,:nod,:node )";
            parameters = new MySqlParameter[]
                    {
                        new MySqlParameter("nome",Classe.comprimento),
                        new MySqlParameter("nom",Classe.largura),
                        new MySqlParameter("no",Classe.altura),
                        new MySqlParameter("nod",Classe.peso),
                        new MySqlParameter("node",Classe.dono.ID)
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
