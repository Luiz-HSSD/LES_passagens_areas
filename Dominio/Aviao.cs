using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Aviao:EntidadeDominio
    {
        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public Aviao()
        {
            nome = "";
        }
    }
}
