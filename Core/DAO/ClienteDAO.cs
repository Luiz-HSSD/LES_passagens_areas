using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using MySql.Data.MySqlClient;
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


                if (Classe.ID == 0 && string.IsNullOrEmpty(Classe.usuario.Login))
                {
                    sql = "SELECT * FROM clientes ";
                }
                else if (!string.IsNullOrEmpty(Classe.usuario.Login))
                {
                    sql = "SELECT * FROM clientes join usuarios using(id_user) WHERE login = :lo and password_user = :pas ";
                }
                else
                {
                    sql = "SELECT * FROM clientes join car_cli using(id_cli) join cartao_credito using(id_car) WHERE id_cli = :co";
                }
                pst = new MySqlCommand();

                pst.CommandText = sql;
                parameters = new MySqlParameter[] 
                {
                    new MySqlParameter("co", Classe.ID),
                    new MySqlParameter("lo", Classe.usuario.Login),
                    new MySqlParameter("pas", Classe.usuario.Password)
                };
                pst.Parameters.Clear();
                pst.Parameters.AddRange(parameters);
                pst.Connection = connection;
                pst.CommandType = CommandType.Text;
                //pst.ExecuteNonQuery();
                vai = pst.ExecuteReader();
                List<EntidadeDominio> Classes = new List<EntidadeDominio>();
                Cliente p =new Cliente();
                while (vai.Read())
                {     
                    if(Classe.ID ==0)
                    p = new Cliente();
                    p.ID = Convert.ToInt32(vai["id_cli"]);
                    p.Nome = (vai["nome_cli"].ToString());
                    p.RG = (vai["rg"].ToString());
                    p.Cpf = (vai["cpf"].ToString());
                    p.Sexo = Convert.ToChar(vai["sexo"]);
                    p.Dt_Nas = Convert.ToDateTime(vai["dt_nas"]);
                    p.usuario.ID = Convert.ToInt32(vai["id_user"]);
                    p.Endereco.ID = Convert.ToInt32(vai["id_end"]);
                    if (Classe.ID != 0)
                    {
                        var c = new Cartao_Credito();
                        p.Cartoes.Add(c);
                        c.ID = Convert.ToInt32(vai["id_car"]);
                        c.Numero = (vai["numero"].ToString());
                        c.CCV = Convert.ToInt32(vai["ccv"]);
                        c.Nome_Titular = (vai["nome_car"].ToString());
                        c.Validade = (vai["validade"].ToString());
                        c.Bandeira.ID = Convert.ToInt32(vai["id_band"].ToString());
                        
                    }
                    if (Classe.ID == 0 || Classes.Count==0)
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
            Cliente Classe = (Cliente)entidade;
            

            pst.CommandText = "insert into clientes ( id_user, id_end ,nome_cli , sexo , cpf ,  rg ,  dt_nas ) values ( :nomee , :nomeee , :nome, :nom , :cpf, :rg ,:dt  ) returning id_cli";
            parameters = new MySqlParameter[]
            {
                new MySqlParameter("nomee" , Classe.usuario.ID),
                new MySqlParameter("nomeee" , Classe.Endereco.ID),
                new MySqlParameter("nome" , Classe.Nome),
                new MySqlParameter("nom" , Classe.Sexo),
                new MySqlParameter("cpf" , Classe.Cpf),
                new MySqlParameter("rg" , Classe.RG),
                new MySqlParameter("dt" , Classe.Dt_Nas)
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
