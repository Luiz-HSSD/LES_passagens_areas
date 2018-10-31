using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Core;
using Dominio;

namespace Core.Negocio
{
    public class GerarBandeira : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Cartao_Credito car = (Cartao_Credito)entidade;
            if (!string.IsNullOrEmpty( car.Numero))
            {
                if (car.Numero.StartsWith("509048") || car.Numero.StartsWith("509067") ||
                   car.Numero.StartsWith("509049") || car.Numero.StartsWith("509069") ||
                   car.Numero.StartsWith("509050") || car.Numero.StartsWith("509074") ||
                   car.Numero.StartsWith("509068") || car.Numero.StartsWith("509040") ||
                   car.Numero.StartsWith("509045") || car.Numero.StartsWith("509051") ||
                   car.Numero.StartsWith("509046") || car.Numero.StartsWith("509066") ||
                   car.Numero.StartsWith("509047") || car.Numero.StartsWith("509042") ||
                   car.Numero.StartsWith("509052") || car.Numero.StartsWith("509043") ||
                   car.Numero.StartsWith("509064") || car.Numero.StartsWith("509040"))
                {
                    return "bandeira invalida";
                }
                else if (car.Numero.StartsWith("5"))
                {
                    car.Bandeira.ID = 1;
                }
                else if (car.Numero.StartsWith("4"))
                {
                    car.Bandeira.ID = 2;
                }
                else
                    return "bandeira invalida";
            }
            else
            {
                return "numero nulo ou invalida";
            }
            return null;
        }
    }
}
