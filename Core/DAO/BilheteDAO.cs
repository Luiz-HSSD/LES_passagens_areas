using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    public class BilheteDAO : AbstractDAO
    {
        public BilheteDAO() : base("bilhete", "bilhete_id")
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
                Bilhete Classe = (Bilhete)entidade;
                string sql = null;

                if (Classe.Nome == null)
                {
                    Classe.Nome = "";
                }


                if (Classe.ID == 0 && Classe.passagem.Voo.ID == 0 && string.IsNullOrEmpty(Classe.Nome))
                {
                    sql = "SELECT * FROM bilhete ";
                }
                else if (Classe.ID == 0 && string.IsNullOrEmpty(Classe.Nome))
                {
                    sql = "SELECT * FROM bilhete join viagem using(viagem_id ) join passagens using(pass_id) WHERE pass_id= :cod";
                }
                else if (string.IsNullOrEmpty(Classe.Nome))
                {
                    sql = "SELECT * FROM bilhete join viagem using(viagem_id ) join passagens using(pass_id) WHERE bilhete_id = :co";
                }
                else
                {
                    sql = "SELECT * FROM bilhete join viagem using(viagem_id ) join passagens using(pass_id) where nome ilike :codd ||'%'";
                }
                pst = new NpgsqlCommand();

                pst.CommandText = sql;
                parameters = new NpgsqlParameter[] { new NpgsqlParameter("co", Classe.ID), new NpgsqlParameter("cod", Classe.passagem.Voo.ID), new NpgsqlParameter("codd", Classe.Nome) };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Bilhete p;
                while (vai.Read())
                {
                    p = new Bilhete();
                    p.ID = Convert.ToInt32(vai["bilhete_id"]);
                    p.Nome = (vai["nome"].ToString());
                    p.cpf = (vai["cpf"].ToString());
                    p.RG = (vai["rg"].ToString());
                    p.Sexo = Convert.ToBoolean(vai["sexo"]);
                    p.passagem.ID = Convert.ToInt32(vai["viagem_id"]);
                    if (Classe.ID != 0 || (Classe.passagem.Voo.ID != 0 || !string.IsNullOrEmpty(Classe.Nome)))
                    {
                        p.passagem.Tipo.ID = Convert.ToInt32(vai["class_id"]);
                        p.passagem.Voo.ID = Convert.ToInt32(vai["pass_id"]);
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
            Bilhete Classe = (Bilhete)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "insert into bilhete (  nome , rg , cpf ,email , sexo , passaporte , viagem_id    ) values (  :no,:nome,:nomm,:nom,:sex,:passpor,:via ) returning bilhete_id";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("no",Classe.Nome),
                        new NpgsqlParameter("nome",Classe.RG),
                        new NpgsqlParameter("nomm",Classe.cpf),
                        new NpgsqlParameter("nom",Classe.Email),
                        new NpgsqlParameter("sex",Classe.Sexo),
                        new NpgsqlParameter("passpor",Classe.passaporte),
                        new NpgsqlParameter("via",Classe.passagem.ID),
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
