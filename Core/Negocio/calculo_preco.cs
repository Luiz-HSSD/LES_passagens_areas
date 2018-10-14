using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Core.DAO;
using Dominio;
using GeoCoordinatePortable;
using System.Linq;
namespace Core.Negocio
{
    public class calculo_preco : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Passagem_Venda pass = (Passagem_Venda)entidade;
            foreach(Passagens pa in  pass.Pass)
            {
                if(pa.Tipo.Peso==0)
                {
                    ClasseDAO clssdao = new ClasseDAO();
                    pa.Tipo=(Classe) clssdao.consultar(pa.Tipo).ElementAt(0);
                    
                }
                if (string.IsNullOrEmpty(pa.LO_partida.Nome))
                {
                    AeroportoDAO clssdao = new AeroportoDAO();
                    pa.LO_partida = (Aeroporto)clssdao.consultar(pa.LO_partida).ElementAt(0);

                }
                if (string.IsNullOrEmpty(pa.LO_chegada.Nome))
                {
                    AeroportoDAO clssdao = new AeroportoDAO();
                    pa.LO_chegada = (Aeroporto)clssdao.consultar(pa.LO_chegada).ElementAt(0);
                }
                var sCoord = new GeoCoordinate(pa.LO_partida.lat, pa.LO_partida.lng);
                var eCoord = new GeoCoordinate(pa.LO_chegada.lat, pa.LO_chegada.lng);
                pa.Preco_uni =Math.Round( ((sCoord.GetDistanceTo(eCoord)/1000)*(1+pa.Tipo.Peso)), 2 ); //distFrom(pa.LO_partida.lat, pa.LO_partida.lng, pa.LO_chegada.lat, pa.LO_partida.lng);
            }
            //entidade = pass;
            return "sucesso!";
        }
    }
}
