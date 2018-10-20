using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Core;
using Dominio;

namespace Core.Negocio
{
    public class fillchartjs : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Analise analise = (Analise)entidade;
            if (analise.ID == 0)
            {
                Dominio.data asd = new Dominio.data()
                {
                    labels = analise.generic_labels,//new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                    
                };
               var fdsa=  new List< Dominio.datasets>() { };
                for (int i = 0; i < analise.resultado.Keys.Count; i++)
                {
                    List<EntidadeDominio> b = analise.resultado.Values.ElementAt(i);
                    Random rnd = new Random();
                    string color= "rgb("+ rnd.Next(0, 255) + ","+ rnd.Next(0, 255) + " , "+ rnd.Next(0,255)+")";
                    var goLuiz= new datasets() {label= analise.resultado.Keys.ElementAt(i), backgroundColor = color, borderColor = color, fill =false };
                    var grr =new List<double>(){ };
                    foreach(Bagagem sl in b)
                    {
                        grr.Add(sl.peso);
                    }
                    goLuiz.data = grr.ToArray();
                    fdsa.Add(goLuiz);
                }
                asd.datasets = fdsa.ToArray();

                Dominio.options asdf = new Dominio.options()
                {
                    responsive = true,
                    title= new title() { display = true, text = "bagegem por voo em tempo" },
                    tooltips = new tooltips() {intersect=false,mode= "index" },
                    hover =new hover() {intersect=true,mode= "nearest" },
                    scales =new scales()
                    {
                        xAxes=new xAxes[] { new xAxes() { display = true, scaleLabel = new scaleLabel() { display = true, labelString = "mês" } } },
                        yAxes = new yAxes[] { new yAxes() { display = true, scaleLabel = new scaleLabel() { display = true, labelString = "peso em Kg" } } }
                    }
                };
                analise.chartsjs.type = "line";
                analise.chartsjs.data = asd;
                analise.chartsjs.options = asdf;
            }
            
            return "sucesso!";
        }
    }
}
