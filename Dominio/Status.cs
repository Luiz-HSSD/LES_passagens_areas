using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Status:EntidadeDominio
    {
        private bool flg_completo;

        public bool Flg_completo
        {
            get { return flg_completo; }
            set { flg_completo = value; }
        }

        private Departamento atual;

        public Departamento Atual
        {
            get { return atual; }
            set { atual = value; }
        }
        private bool isDesembarque;

        public bool IsDesembarque
        {
            get { return isDesembarque; }
            set { isDesembarque = value; }
        }

        private Bilhete passageiro;

        public Bilhete Passageiro
        {
            get { return passageiro; }
            set { passageiro = value; }
        }


        public Status()
        {
            flg_completo = false;
            passageiro = new Bilhete();
            atual = new Departamento();
            isDesembarque = false;

        }

    }
}
