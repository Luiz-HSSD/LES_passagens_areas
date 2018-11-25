using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;
using Dominio;

namespace Core.Negocio
{
    public class ValidarCpf : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Pessoa_Fisica cli= (Pessoa_Fisica)entidade;
            string cpf = cli.cpf;
            int i = 0, j = 0, somatorio = 0;
            try
            {
                for (j = 9; j < 11; j++)
                {
                    for (i = j; i > 0; i--)
                    {
                        somatorio += (i + 1) * (Convert.ToInt32(cpf.Substring(j - i, 1)));
                        if (i == 1)
                        {
                            somatorio = (somatorio * 10) % (11);
                            if (somatorio == 10 || somatorio == 10) somatorio = 0;
                            if (Convert.ToInt32(cpf.Substring(j, 1)) != somatorio)
                                return "cpf invalido";
                        }


                    }
                    somatorio = 0;
                }
                /*
                ClienteDAO daao = new ClienteDAO();
                List<EntidadeDominio> cliente = daao.consultar(entidade);
                for (i = 0; i < cliente.Count; i++)
                {
                    if (((Cliente)cliente.ElementAt(i)).Cpf == cpf && ((Cliente)cliente.ElementAt(i)).Id!=cli.Id) return "esse cpf já existe no banco";
                }*/
                return null;
                
            }
            catch
            {
                return "cpf não está no formato certo";
            }
        }
    }
}

