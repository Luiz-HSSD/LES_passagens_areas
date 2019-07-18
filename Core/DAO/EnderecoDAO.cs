using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dominio;
using MySql.Data.MySqlClient;

namespace Core.DAO
{
    public class EnderecoDAO : AbstractDAO
    {
        public EnderecoDAO() : base("endereco", "id_end")
        {
        }

        public override void alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override void salvar(EntidadeDominio entidade)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            Endereco endereco = (Endereco)entidade;
            pst.CommandText = "insert into endereco ( numero , logradouro , bairro , cidade ,  complemento , cep , uf )   values ( @nomee, @nome, @nom , @cpf, @rg , @dt , @dtt ) returning id_end";
            parameters = new MySqlParameter[]
            {
                new MySqlParameter("nomee" , endereco.Numero),
                new MySqlParameter("nome" ,  endereco.Logradouro),
                new MySqlParameter("nom"  ,  endereco.Bairro),
                new MySqlParameter("cpf"  ,  endereco.Cidade),
                new MySqlParameter("rg"  ,   endereco.Complemento),
                new MySqlParameter("dt"  ,   endereco.Cep),
                new MySqlParameter("dtt" ,   endereco.UF)
            };
            pst.Parameters.Clear();
            pst.Parameters.AddRange(parameters);
            pst.Connection = connection;
            pst.CommandType = CommandType.Text;
            endereco.ID = (int)pst.ExecuteScalar();
            pst.CommandText = "commit work";
            pst.ExecuteNonQuery();
            connection.Close();
            return;
        }
    }
}
