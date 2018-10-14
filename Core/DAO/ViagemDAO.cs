using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using Npgsql;

namespace Core.DAO
{
    class ViagemDAO : AbstractDAO
    {
        public ViagemDAO() : base("viagem", "viagem_id")
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
                Viagem Classe = (Viagem)entidade;
                string sql = null;
                if (Classe.ID == 0)
                {
                    sql = "SELECT * FROM Viagem ";
                }
                else
                {
                    sql = "SELECT * FROM Viagem WHERE viagem_id= :co";
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
                Viagem p;
                while (vai.Read())
                {
                    p = new Viagem();
                    p.ID = Convert.ToInt32(vai["viagem_id"]);
                    p.qtd = Convert.ToInt32(vai["qtd"]);
                    p.Valor_Unidade = Convert.ToDouble(vai["preco_unit"]);
                    p.Voo.ID = Convert.ToInt32(vai["pass_id"]);
                    p.Tipo.ID = Convert.ToInt32(vai["class_id"]); 
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
            Viagem Classe = (Viagem)entidade;
            pst.Dispose();
            pst = new NpgsqlCommand();
            pst.CommandText = "insert into viagem ( qtd ,preco_unit,pass_id,class_id ) values (  :no,:nomm,:nome,:noo )";
            parameters = new NpgsqlParameter[]
                    {
                        new NpgsqlParameter("no",Classe.qtd),
                        new NpgsqlParameter("nomm",Classe.Valor_Unidade),
                        new NpgsqlParameter("nome",Classe.Voo.ID),
                        new NpgsqlParameter("noo",Classe.Tipo.ID)
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
