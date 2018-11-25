using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Departamento:EntidadeDominio
    {
        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private Passagens pass;

        public Passagens Pass
        {
            get { return pass; }
            set { pass = value; }
        }



        public Departamento() : base()
        {
            pass = new Passagens();
            nome = "";

        }
    }
}
