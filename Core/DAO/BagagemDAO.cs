using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

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
                if (Classe.ID == 0)
                {
                    sql = "SELECT * FROM Bagagem ";
                }
                else
                {
                    sql = "SELECT * FROM Bagagem WHERE Bagagem_id= :co";
                }
                pst = new NpgsqlCommand();

                pst.CommandText = sql;
                parameters = new NpgsqlParameter[] { new NpgsqlParameter("co", Classe.ID) };
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
                    p = new Bagagem(chk);
                    p.ID = Convert.ToInt32(vai["baga_id"]);
                    p.comprimento = Convert.ToInt32(vai["comprimento"]);
                    p.largura = Convert.ToInt32(vai["largura"]);
                    p.altura = Convert.ToInt32(vai["altura"]);
                    p.peso = (vai["peso"].ToString());                   
                    p.dono.ID = Convert.ToInt32(vai["chck_in_id"]);
                    {
                        p.dono.Passagem.Voo
                    }
                    chk = new Check_in() { ID= p.dono.ID };
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
            Bagagem Classe = (Bagagem)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "insert into Bagagem ( comprimento, largura,altura,peso ) values (  :nome,:nom,:no,:nod,:node )";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("nome",Classe.comprimento),
                        new NpgsqlParameter("nom",Classe.largura),
                        new NpgsqlParameter("no",Classe.altura),
                        new NpgsqlParameter("nod",Classe.peso),
                        new NpgsqlParameter("node",Classe.dono.ID)
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
