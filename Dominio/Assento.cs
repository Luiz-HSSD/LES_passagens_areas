using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Assento:EntidadeDominio
    {
        private Check_in _ocupante;

        public Check_in ocupante
        {
            get { return _ocupante; }
            set { _ocupante = value; }
        }
        private Passagens _viagem;

        public Passagens viagem
        {
            get { return _viagem; }
            set { _viagem = value; }
        }
        private string _Tag;

        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }
        private Classe _tipo;

        public Classe tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }


        public Assento(Check_in ocupante)
        {
            _viagem = new Passagens();
            Tag = "";
            _ocupante = ocupante;
            _tipo = new Classe();
        }
    }
}
