using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Barrado:EntidadeDominio
    {
        private string causa;

        public string Causa
        {
            get { return causa; }
            set { causa = value; }
        }

        public Barrado():base()
        {
            causa = "";
        }

    }
}
