using Core.Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LES_passagens_areas.Command;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;

namespace LES_passagens_areas
{
    public class viewgenerico : PageModel
    {
        protected  Resultado res { get; set; } = new Resultado();
        protected  Dictionary<string, ICommand.ICommand> commands { get; set; } = new Dictionary<string, ICommand.ICommand>();

        public viewgenerico()
        {
            commands.Add("SALVAR", new SalvarCommand());
            commands.Add("ALTERAR", new AlterarCommand());
            commands.Add("EXCLUIR", new ExcluirCommand());
            commands.Add("CONSULTAR", new ConsultarCommand());
            commands.Add("VISUALIZAR", new VisualizarCommand());
        }
               
        public bool autenticar(int permisão)
        {
            var usu = HttpContext.Session.GetObjectFromJson<Usuarios>("login");
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