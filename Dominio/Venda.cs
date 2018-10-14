using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Venda:EntidadeDominio
    {
        private List<Viagem> viagems;
        private Cliente cliente;
        private decimal total;

        public Venda() : base()
        {
            cliente = new Cliente();
            viagems = new List<Viagem>();
            total = 0;
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        

        public Cliente Cliente_prop
        {
            get { return cliente; }
            set { cliente = value; }
        }


        public List<Viagem> Viagems
        {
            get { return viagems; }
            set { viagems = value; }
        }
    }
}
