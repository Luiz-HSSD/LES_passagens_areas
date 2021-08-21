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
        private readonly ConexaoSettings _appSettings;

        public Conexao(IOptions<ConexaoSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public static string conx = "Server=127.0.0.1;Port=5432;User Id = postgres; Password=123;Database=lab;";
        public static NpgsqlConnection getconnection()
        {
            NpgsqlConnection go = new NpgsqlConnection(conx);
            return go;
        }
    }
}
