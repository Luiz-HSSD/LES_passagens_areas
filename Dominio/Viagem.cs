using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Viagem : EntidadeDominio
    {
        private double valor;
        private double valor_unidade;

        public double Valor_Unidade
        {
            get { return valor_unidade; }
            set { valor_unidade = value; }
        }


       
        public double Valor
        {
            get { return valor; }
            set { valor = value; }
        }

       
        private Cliente _titular;

        public Cliente Titular
        {
            get { return _titular; }
            set { _titular = value; }
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

        private List<Bilhete> _passageiros;

        public List<Bilhete> Passageiros
        {
            get { return _passageiros; }
            set { _passageiros = value; }
        }


        public Viagem():base()
        {
            _titular = new Cliente();
            _passageiros = new List<Bilhete>();
            _qtd = 0;
            valor = 0.0;
            valor_unidade = 0.0;
            _voo = new Passagens();
            _tipo = new Classe();
        }
    }
}
