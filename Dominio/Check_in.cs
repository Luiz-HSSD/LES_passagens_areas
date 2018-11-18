using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Check_in:EntidadeDominio
    {


        private List<Bagagem> _Bagagem;

        public List<Bagagem> Bagagem
        {
            get { return _Bagagem; }
            set { _Bagagem = value; }
        }

        private Assento _ocupante;

        public Assento Ocupante
        {
            get { return _ocupante; }
            set { _ocupante = value; }
        }
        private Viagem _passagem;

        public Viagem Passagem
        {
            get { return _passagem; }
            set { _passagem = value; }
        }
        private Bilhete _entrada;

        public Bilhete Entrada
        {
            get { return _entrada; }
            set { _entrada = value; }
        }

        private bool flg_ana;

        public bool Flg
        {
            get { return flg_ana; }
            set { flg_ana = value; }
        }


        public Check_in() : base()
        {
            flg_ana = false;
            _Bagagem = new List<Bagagem>();
            _ocupante = new Assento(this);
            _passagem = new Viagem();
            _entrada = new Bilhete();

        }
    }
}
