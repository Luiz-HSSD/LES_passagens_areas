using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Aeroporto:EntidadeDominio
    {
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        private double _lat;

        public double lat
        {
            get { return _lat; }
            set { _lat = value; }
        }

        private double _lng;

        public double lng
        {
            get { return _lng; }
            set { _lng = value; }
        }

        private string _sigla;

        public string sigla
        {
            get { return _sigla; }
            set { _sigla = value; }
        }

        public Aeroporto():base()
        {
            Nome = "";
            sigla = "";
            lat = 0;
            lng = 0;
        }

    }
}
