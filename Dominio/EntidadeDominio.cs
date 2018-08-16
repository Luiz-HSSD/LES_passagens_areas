using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class EntidadeDominio
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public EntidadeDominio()
        {
            _id = 0;
        }
    }
}
