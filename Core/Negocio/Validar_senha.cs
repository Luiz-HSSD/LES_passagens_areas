using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;
using Dominio;

namespace Core.Negocio
{
    class Validar_senha : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            string cli = ((Cliente)entidade).usuario.Password;
            string dev = "";
            if (cli.Length < 8)
                dev+= "senha muito pequena";
            if (!(cli.Any(char.IsLower) && cli.Any(char.IsUpper)))
                dev += "senha deve ter caracteres maiusculos e minusculos";
            if (!cli.Any(ch => !char.IsLetterOrDigit(ch)))
                dev += "senha deve ter caracteres especiais";
            if (!cli.Any(char.IsDigit))
                dev += "senha deve ter números";
            if (string.IsNullOrEmpty(dev))
                return null;
            else
                return dev;
        }
    }
}
