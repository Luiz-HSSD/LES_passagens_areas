using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;
using System.Linq;

namespace Core.DAO
{
    public class ClienteDAO : AbstractDAO
    {
        public ClienteDAO() : base("clientes", "id_cli")
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
                Cliente Classe = (Cliente)entidade;
                string sql = null;

                if (Classe.Nome == null)
                {
                    Classe.Nome = "";
                }


                if (Classe.ID == 0)
                {
                    sql = "SELECT * FROM clientes ";
                }
                else
                {
                    sql = "SELECT * FROM clientes join car_cli using(id_cli) join cartao_credito using(id_car) WHERE id_cli = :co";
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
                Cliente p;
                while (vai.Read())
                {     
                    p = new Cliente();
                    p.ID = Convert.ToInt32(vai["id_cli"]);
                    p.Nome = (vai["nome_cli"].ToString());
                    p.Rg = (vai["rg"].ToString());
                    p.Cpf = (vai["cpf"].ToString());
                    p.Sexo = Convert.ToChar(vai["sexo"]);
                    p.Dt_Nas = Convert.ToDateTime(vai["dt_nas"]);
                    p.usuario.ID = Convert.ToInt32(vai["id_user"]);
                    p.Endereco.ID = Convert.ToInt32(vai["id_end"]);
                    if (Classe.ID != 0)
                    {
                        p.Cartoes.Add(new Cartao_Credito());
                        p.Cartoes.ElementAt(0).ID = Convert.ToInt32(vai["id_car"]);
                        p.Cartoes.ElementAt(0).Numero = (vai["numero"].ToString());
                        p.Cartoes.ElementAt(0).CCV = Convert.ToInt32(vai["ccv"]);
                        p.Cartoes.ElementAt(0).Nome_Titular = (vai["nome_car"].ToString());
                        p.Cartoes.ElementAt(0).Validade = (vai["validade"].ToString());
                        p.Cartoes.ElementAt(0).Bandeira.ID = Convert.ToInt32(vai["id_band"].ToString());
                        if (Classes.Count > 1)
                        { 
                            Cliente c = (Cliente)Classes.ElementAt(Classes.Count - 1);
                            if (c.ID == p.ID)
                            {
                                c.Cartoes.Add(p.Cartoes.ElementAt(0));
                                continue;
                            }
                        }
                    }
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
            Cliente Classe = (Cliente)entidade;
            

            pst.CommandText = "insert into clientes ( id_user, id_end ,nome_cli , sexo , cpf ,  rg ,  dt_nas ) values ( :nomee , :nomeee , :nome, :nom , :cpf, :rg ,:dt  ) returning id_cli";
            parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("nomee" , Classe.usuario.ID),
                new NpgsqlParameter("nomeee" , Classe.Endereco.ID),
                new NpgsqlParameter("nome" , Classe.Nome),
                new NpgsqlParameter("nom" , Classe.Sexo),
                new NpgsqlParameter("cpf" , Classe.Cpf),
                new NpgsqlParameter("rg" , Classe.Rg),
                new NpgsqlParameter("dt" , Classe.Dt_Nas)
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
