using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Pessoa_Fisica:EntidadeDominio
    {
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
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
        public Pessoa_Fisica()
        {
            _nome = "";
            _RG = "";
            _cpf = "";
        }
    }
}
