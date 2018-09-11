using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Passagens : EntidadeDominio
    {
        private Cliente _titular;

        public Cliente Titular
        {
            get { return _titular; }
            set { _titular = value; }
        }
        private Classe tipo;

        public Classe Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        private DateTime dt_chegada;    

        public DateTime DT_chegada
        {
            get { return dt_chegada; }
            set { dt_chegada = value; }
        }
        private DateTime dt_partida;

        public DateTime DT_partida
        {
            get { return dt_partida; }
            set { dt_partida = value; }
        }

        private string lo_partida;

        public string LO_partida
        {
            get { return lo_partida; }
            set { lo_partida = value; }
        }

        private string lo_chegada;  

        public string LO_chegada
        {
            get { return lo_chegada; }
            set { lo_chegada = value; }
        }
        private Aviao aviao_v;

        public Aviao Aviao_v
        {
            get { return aviao_v; }
            set { aviao_v = value; }
        }

        private int qtd;

        public int QTD
        {
            get { return qtd; }
            set { qtd = value; }
        }




        public Passagens():base()
        {
            _titular = new Cliente();
            aviao_v = new Aviao();
            tipo = new Classe();
            qtd = 0;
            dt_chegada = DateTime.Now;
            dt_partida = DateTime.Now;
            LO_partida = "";
            LO_chegada = "";
        }
    }
}
