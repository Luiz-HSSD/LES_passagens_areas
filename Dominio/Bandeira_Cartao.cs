using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Bandeira_Cartao : EntidadeDominio
    {
        private string nome;
        private int tipo;

        public Bandeira_Cartao() : base()
        {
            Nome = "";
            Tipo = 0;

        }
        
        public int Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }


        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

    }
}
