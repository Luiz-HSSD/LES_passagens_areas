using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Core.Utils
{
    public class Conexao
    {
        
        public Conexao()
        {
             
        }
        public static string conx = ConexaoSettings.conexao;// "Server=127.0.0.1;Port=5432;User Id = postgres; Password=123;Database=lab;";
        public static NpgsqlConnection getconnection()
        {
            NpgsqlConnection go = new NpgsqlConnection(conx);
            return go;
        }
    }
}
