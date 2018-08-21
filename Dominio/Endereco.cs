using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Endereco:EntidadeDominio
    {
        

        private string logradouro;
        private string numero;
        private string complemento;
        private string bairro;
        private string uf;
        private string cidade;
        private string cep;
        private int tipo;

        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }


        public string Cep
        {
            get { return cep; }
            set { cep = value; }
        }


        public string Cidade
        {
            get { return cidade; }
            set { cidade = value; }
        }


        public string UF
        {
            get { return uf; }
            set { uf = value; }
        }

        public string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        public string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }

        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        public string Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }

        public Endereco() : base()
        {
            cep = "";
            Complemento = "";
            Numero = "";
            Logradouro = "";
            Bairro = "";
            Cidade = "";
            UF = "";
            tipo = 0;
        }

        
    }
}
