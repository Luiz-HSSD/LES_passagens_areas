using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Bagagem:EntidadeDominio
    {
        private int _comprimento;

        public int comprimento
        {
            get { return _comprimento; }
            set { _comprimento = value; }
        }
        private int _largura;

        public int largura
        {
            get { return _largura; }
            set { _largura = value; }
        }
        private int _altura;

        public int altura
        {
            get { return _altura; }
            set { _altura = value; }
        }
        private string _peso;

        public string peso
        {
            get { return _peso; }
            set { _peso = value; }
        }

        private Check_in _dono;

        public Check_in dono
        {
            get { return _dono; }
            set { _dono = value; }
        }

        public Bagagem(Check_in dono) : base()
        {
            _comprimento = 0;
            _largura = 0;
            _altura = 0;
            _peso = "";
            _dono = dono;
        }

    }
}
