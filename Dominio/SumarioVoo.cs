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

        private List<Sumario> _ls;

        public List<Sumario> ls
        {
            get { return _ls; }
            set { _ls = value; }
        }


        public SumarioVoo():base()
        {
            ls = new List<Sumario>();
        }
    }
}
