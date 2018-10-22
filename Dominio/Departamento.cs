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


        public Departamento() : base()
        {
            nome = "";

        }
    }
}
