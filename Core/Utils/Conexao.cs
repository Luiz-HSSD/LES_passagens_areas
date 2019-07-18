using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public class Conexao
    {
        public static string conx = "Server=localhost;Database=lab;Uid=root;Pwd=1234;";
        public static MySqlConnection getconnection()
        {
            MySqlConnection go = new MySqlConnection(conx);
            return go;
        }  
    }
}
