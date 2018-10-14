using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Passagem_Venda:EntidadeDominio
    {
        private List<Passagens> pass;

        public List<Passagens> Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public Passagem_Venda()
        {
            pass = new List<Passagens>();
        }
    }
}
