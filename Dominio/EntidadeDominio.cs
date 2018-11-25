using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class EntidadeDominio:IEntidade
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        /*
        private DateTime dt_op;

        public DateTime DT_op
        {
            get { return dt_op; }
            set { dt_op = value; }
        }
        */
        public EntidadeDominio()
        {
            _id = 0;
            //dt_op= Convert.ToDateTime("01/01/1995 03:30");
        }
    }
}
