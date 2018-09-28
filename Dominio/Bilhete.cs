using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Bilhete:EntidadeDominio
    {
        private bool _sexo;

        public bool Sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }


        private string _RG;

        public string RG
        {
            get { return _RG; }
            set { _RG = value; }
        }

        private string _cpf;

        public string cpf
        {
            get { return _cpf; }
            set { _cpf = value; }
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
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }


        public Bilhete() : base()
        {
            _nome = "";
            _RG = "";
            _cpf = "";
            _passaporte = "";
            _sexo = true;
            _email = "";
            passagem= new Viagem();
        }
    }
}
