using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Bilhete:Pessoa_Fisica
    {
        private bool _sexo;

        public bool Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }


        private string _passaporte;

        public string passaporte
        {
            get { return _passaporte; }
            set { _passaporte = value; }
        }


        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }


        private Viagem _passagem;

        public Viagem passagem
        {
            get { return _passagem; }
            set { _passagem = value; }
        }



        public Bilhete() : base()
        {
            _passaporte = "";
            _sexo = true;
            _email = "";
            passagem= new Viagem();
        }
    }
}
