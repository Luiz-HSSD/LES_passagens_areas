using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Passagem : EntidadeDominio
    {
        private Cliente _titular;

        public Cliente Titular
        {
            get { return _titular; }
            set { _titular = value; }
        }
        private Classe tipo;

        public Classe Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public Passagem():base()
        {
            _titular = new Cliente();
        }
    }
}
