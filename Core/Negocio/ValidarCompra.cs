using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Dominio;

namespace Core.Negocio
{
    public class ValidarCompra : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            string result="";
            Venda Classe = (Venda)entidade;
            ValidarPessoa_Fisica vpf = new ValidarPessoa_Fisica();
            if(Classe.Forma_pagamento.ID==0)
                result += "por favor preecha o pagamento!\n";
            if (Classe.Viagems.Count == 0)
                result+= "por favor coloque viagens!\n";
            foreach(Viagem vi in Classe.Viagems)
            {
                if (vi.qtd != vi.Passageiros.Count)
                {
                    result += "por favor comfirme os bilhetes!\n";
                    continue;
                }
                    
                foreach (Bilhete bi in vi.Passageiros)
                {
                    string abc = vpf.processar(bi);
                    if (!string.IsNullOrEmpty(abc))
                        result += abc; 
                }
            }
            if (result == "")
                return null;
            else
                return result;
        }
    }
}
