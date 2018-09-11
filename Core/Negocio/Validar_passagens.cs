using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Dominio;

namespace Core.Negocio
{
    class Validar_passagens : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            string resposta = "";
            Passagens pass = (Passagens)entidade;
            if (pass.QTD <= 0)
                resposta += "quantidade inválida\n";
            if (string.IsNullOrEmpty(pass.LO_chegada))
                resposta += "aeroporto de chegada inválido\n";
            if (string.IsNullOrEmpty(pass.LO_partida))
                resposta += "aeroporto de partida inválido\n";
            if (pass.DT_partida <= DateTime.Now)
                resposta += "partida não pode ser antes do cadastro\n";
            if (pass.DT_chegada <= DateTime.Now)
                resposta += "chegada não pode ser antes do cadastro\n";
            if ((pass.DT_chegada-pass.DT_partida).Hours <= 1)
                resposta += "a viagem não pode durar menos que uma hora\n";
            if (!string.IsNullOrEmpty(resposta))
                return resposta;
            else
                return null;
        }
    }
}
