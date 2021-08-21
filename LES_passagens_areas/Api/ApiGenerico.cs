using Core.Aplicacao;
using Dominio;
using LES_passagens_areas.Command;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jose;
using Newtonsoft.Json;
using System.Text;

namespace LES_passagens_areas.Api
{

    public class ApiGenerico: Controller
    {
        protected Resultado res { get; set; } = new Resultado();
        protected Dictionary<string, ICommand.ICommand> commands { get; set; } = new Dictionary<string, ICommand.ICommand>();

        public static new byte[] keyCrypt = Encoding.UTF8.GetBytes("theDevilinI#666123");//new byte[] { 164, 33, 194, 12, 161, 189, 41, 38, 130, 89, 142  };
        public ApiGenerico()
        {
            commands.Add("SALVAR", new SalvarCommand());
            commands.Add("ALTERAR", new AlterarCommand());
            commands.Add("EXCLUIR", new ExcluirCommand());
            commands.Add("CONSULTAR", new ConsultarCommand());
            commands.Add("VISUALIZAR", new VisualizarCommand());
        }

        public bool autenticar(int permisão)
        {
            string token= Jose.JWT.Decode(HttpContext.Request.Headers["Authorization"].ToString(), keyCrypt, JwsAlgorithm.HS512);
            var usu = JsonConvert.DeserializeObject<Usuarios>(token);
            if (usu == null)
            {
                Response.Redirect(("./"));
                return false;
            }
            if (usu.Permisao < permisão)
            {
                Response.Redirect(("./"));
                return false;
            }
            return true;
        }
    }
}
