using System;
using System.Data;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class Conexao
    {
        public static string conx = "Server=127.0.0.1;Port=5432;User Id = postgres; Password=123;Database=lab;";
        public static NpgsqlConnection getconnection()
        {
            NpgsqlConnection go = new NpgsqlConnection(conx);
            return go;
        }  
    }
}
