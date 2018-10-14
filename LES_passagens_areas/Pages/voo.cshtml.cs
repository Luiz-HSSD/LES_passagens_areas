using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Globalization;
using GeoCoordinatePortable;

namespace LES_passagens_areas.Pages
{
    public class vooModel : viewgenerico
    {
        public List<EntidadeDominio> Getvoo(int partida,int chegada,string date)
        {

            date = Encoding.UTF8.GetString(Convert.FromBase64String(date));
            DateTime e = DateTime.Now;
            DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("pt-BR"), DateTimeStyles.None, out e);
            var roles = commands["CONSULTAR"].execute(new Dominio.Passagens() {DT_partida=e, LO_partida =new Aeroporto() {ID=partida }, LO_chegada = new Aeroporto() { ID = chegada } }).Entidades;
            Passagem_Venda pass = new Passagem_Venda() { Pass = roles.Cast<Passagens>().ToList() };
            commands["CONSULTAR"].execute(pass);

            return pass.Pass.Cast<EntidadeDominio>().ToList();
        }
        public List<EntidadeDominio> GetRoles=new List<EntidadeDominio>();
        public string dist = "";
        public static double ConvertToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
        public static float distFrom(double lat1, double lng1, double lat2, double lng2)
        {
            double earthRadius = 6371000; //meters
            double dLat = ConvertToRadians(lat2 - lat1);
            double dLng = ConvertToRadians(lng2 - lng1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ConvertToRadians(lat1)) * Math.Cos(ConvertToRadians(lat2)) *
                       Math.Sin(dLng / 2) * Math.Sin(dLng / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            float dist = (float)(earthRadius * c);

            return dist;
        }
        public void OnGet(int partida, int chegada, string date)
        {
            if (partida != 0 && chegada != 0)
            {
                GetRoles = Getvoo(partida, chegada, date);
                Passagens gr = ((Passagens)GetRoles.ElementAt(0));
                var sCoord = new GeoCoordinate(gr.LO_partida.lat, gr.LO_partida.lng);
                var eCoord = new GeoCoordinate(gr.LO_chegada.lat, gr.LO_partida.lng);

                dist = eCoord.GetDistanceTo(sCoord).ToString(); //distFrom(gr.LO_chegada.lat, gr.LO_chegada.lng, gr.LO_partida.lat, gr.LO_partida.lng).ToString();
            }

        }
    }
}