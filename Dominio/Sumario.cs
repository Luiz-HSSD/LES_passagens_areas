using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Sumario:EntidadeDominio
    {
        private int qtd;

        public int Qtd
        {
            get { return qtd; }
            set { qtd = value; }
        }

        private Departamento dep;

        public Departamento Dep
        {
            get { return dep; }
            set { dep = value; }
        }

        private Aeroporto aero;

        public Aeroporto Aero
        {
            get { return aero; }
            set { aero = value; }
        }

        public Sumario()
        {
            qtd = 0;
            dep = new Departamento();
            aero = new Aeroporto();
        }
    }
}
