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

        private int qtd_passaram;

        public int Qtd_passaram
        {
            get { return qtd_passaram; }
            set { qtd_passaram = value; }
        }
        private int qtd_nao_passaram;

        public int Qtd_nao_passaram
        {
            get { return qtd_nao_passaram; }
            set { qtd_nao_passaram = value; }
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

        private Passagens voo;


        public Passagens Voo
        {
            get { return voo; }
            set { voo = value; }
        }

        private bool isDesembarque;

        public bool IsDesembarque
        {
            get { return isDesembarque; }
            set { isDesembarque = value; }
        }

        public Sumario()
        {
            IsDesembarque = false;
            qtd_passaram = 0;
            qtd_nao_passaram = 0;
            qtd = 0;
            dep = new Departamento();
            aero = new Aeroporto();
            voo = new Passagens();
        }
    }
}
