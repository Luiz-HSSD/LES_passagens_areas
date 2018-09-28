using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Aviao:EntidadeDominio
    {
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        private int _lugares;

        public int Lugares
        {
            get { return _lugares; }
            set { _lugares = value; }
        }
        private string _marca;

        public string Marca
        {
            get { return _marca; }
            set { _marca = value; }
        }
        private string _serie;

        public string Serie
        {
            get { return _serie; }
            set { _serie = value; }
        }



        public Aviao()
        {
            _nome = "";
            _lugares = 0;
            _marca = "";
            _serie = "";
        }
    }
}
