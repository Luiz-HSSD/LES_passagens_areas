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
        private Cartao_Credito forma_pagamento;

        public Cartao_Credito Forma_pagamento
        {
            get { return forma_pagamento; }
            set { forma_pagamento = value; }
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

        public Venda() : base()
        {
            forma_pagamento = new Cartao_Credito();
            cliente = new Cliente();
            viagems = new List<Viagem>();
            total = 0;
        }
    }
}
