using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Viagem : EntidadeDominio
    {
        private Cliente _titular;

        public Cliente Titular
        {
            get { return _titular; }
            set { _titular = value; }
        }
        private double _preco_unitario;

        public double Preco_unitario
        {
            get { return _preco_unitario; }
            set { _preco_unitario = value; }
        }

        private int _qtd;

        public int qtd
        {
            get { return _qtd; }
            set { _qtd = value; }
        }


        private Passagens _voo;

        public Passagens Voo
        {
            get { return _voo; }
            set { _voo = value; }
        }

        private Classe _tipo;

        public Classe Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }


        public Viagem():base()
        {
            _titular = new Cliente();
            _preco_unitario = 0.0;
            _qtd = 0;
            _voo = new Passagens();
            _tipo = new Classe();
        }
    }
}
