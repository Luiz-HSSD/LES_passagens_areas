using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class SumarioVoo : EntidadeDominio
    {
        private int codd;

        public int Codd
        {
            get { return codd; }
            set { codd = value; }
        }

        public List<Sumario> ls;

        public SumarioVoo():base()
        {
            ls = new List<Sumario>();
        }
    }
}
