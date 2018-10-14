using System;

namespace Dominio
{
    public class Classe:EntidadeDominio
    {
        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private double _peso;

        public double Peso
        {
            get { return _peso; }
            set { _peso = value; }
        }

        public Classe()
        {

            _peso = 0.0;
            nome = "";
        }
    }
}
