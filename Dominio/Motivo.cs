using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Motivo:EntidadeDominio
    {
        private string nome;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        private Departamento dep;

        public Departamento Dep
        {
            get { return dep; }
            set { dep = value; }
        }

        public Motivo()
        {
            nome = "";
            dep = new Departamento();
        }
    }
}
