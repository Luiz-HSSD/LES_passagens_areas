using System;
using System.Collections.Generic;
using System.Text;
using Core.Core;
using Dominio;

namespace Core.Negocio
{
    public class inteligencia : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Analise analise = (Analise)entidade;
            if (analise.ID == 0)
            {
                Dominio.data asd = new Dominio.data()
                {
                    labels = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },
                    datasets = new Dominio.datasets[]
                    {
                    new Dominio.datasets() {label="machester", backgroundColor= "rgb(255, 99, 132)",borderColor= "rgb(255, 99, 132)",fill=false,data=new double[]{ 10, 20, 40, 50, 60, 60, 70, 60, 50, 40, 45, 50 }},
                    new Dominio.datasets() {label="chesea", backgroundColor= "rgb(54, 162, 235)",borderColor= "rgb(54, 162, 235)",fill=false,data=new double[]{ 100, 95, 90, 80, 60, 60, 65, 70, 75, 80, 90, 85 }}
                    }
                };
                Dominio.options asdf = new Dominio.options()
                {
                    responsive = true,
                    title= new title() { display = true, text = "campeonato inglês" },
                    tooltips = new tooltips() {intersect=false,mode= "index" },
                    hover =new hover() {intersect=true,mode= "nearest" },
                    scales =new scales()
                    {
                        xAxes=new xAxes[] { new xAxes() { display = true, scaleLabel = new scaleLabel() { display = true, labelString = "mês" } } },
                        yAxes = new yAxes[] { new yAxes() { display = true, scaleLabel = new scaleLabel() { display = true, labelString = "gols" } } }
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
