using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Dominio;

namespace Core.Negocio
{
    public class ValidarPessoa_Fisica : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            string result = "";
            Pessoa_Fisica Classe = (Pessoa_Fisica)entidade;
            Classe.RG = Classe.RG.Replace(".", "").Replace("-", "");
            Classe.cpf = Classe.cpf.Replace(".", "").Replace("-", "");
            ((Pessoa_Fisica)entidade).RG = Classe.RG;
            ((Pessoa_Fisica)entidade).cpf = Classe.cpf;
            if (Classe.RG.Length != 9 && Classe.RG.Length != 8)
                result+= "RG invalido\n";
            string vdcpf = new ValidarCpf().processar(entidade);
            if (!string.IsNullOrEmpty(vdcpf))
                result += vdcpf;
            if (result == "")
                return null;
            else
                return result;
        }
    }
}
