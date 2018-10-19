﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{

    public class datasets
    {
        public string label;
        public string backgroundColor;
        public string borderColor;
        public double[] data;
        public bool fill;
    }
    public class data
    {
        public string[] labels;
        public datasets[] datasets;
    }
    public enum tipo_grafico
    {
        line,
        pizza,
        barras
    }

    public class title
    {
        public bool display;
        public string text;
        
    }
    public class tooltips
    {
        public bool intersect;
        public string mode;

    }
    public class hover 
    {
        public bool intersect;
        public string mode;

    }
    public class scaleLabel
    {
        public bool display;
        public string labelString;
    }
    public class xAxes
    {
        public bool display;
        public scaleLabel scaleLabel;
    }
    public class yAxes
    {
        public bool display;
        public scaleLabel scaleLabel;
    }
    public class scales
    {
        public xAxes[] xAxes;
        public yAxes[] yAxes;
    }
    public class options
    {
        public bool responsive= true;
        public title title;
        public tooltips tooltips;
        public hover hover;
        public scales scales;
    }
    public class chartsjs
    {
        public string type;
        public data data;
        public options options;
        public chartsjs()
        {
            data = new data();
            type = Enum.GetName(typeof(tipo_grafico), tipo_grafico.line);
        }
    }
    public class Analise :EntidadeDominio
    {
        public chartsjs chartsjs;

        public Analise():base()
        {
            chartsjs = new chartsjs();

        }
    }
}