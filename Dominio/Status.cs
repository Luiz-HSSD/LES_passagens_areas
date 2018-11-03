using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Status:EntidadeDominio
    {
        private Departamento atual;

        public Departamento Atual
        {
            get { return atual; }
            set { atual = value; }
        }
        private Departamento prox;

        public Departamento Prox
        {
            get { return prox; }
            set { prox = value; }
        }

        private Bilhete passageiro;

        public Bilhete Passageiro
        {
            get { return passageiro; }
            set { passageiro = value; }
        }


        public Status()
        {
            passageiro = new Bilhete();
            atual = new Departamento();
            prox = new Departamento();

        }

    }
}
