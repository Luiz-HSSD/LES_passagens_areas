using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Barrado:EntidadeDominio
    {
        private Motivo motivo;

        public Motivo Categoria
        {
            get { return motivo; }
            set { motivo = value; }
        }

        private Status passageiro;

        public Status Passageiro
        {
            get { return passageiro; }
            set { passageiro = value; }
        }

        private string causa;

        public string Causa
        {
            get { return causa; }
            set { causa = value; }
        }

        public Barrado():base()
        {
            causa = "";
            passageiro = new Status();
            motivo = new Motivo();
        }

    }
}
