using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Check_in:EntidadeDominio
    {
        private string _rg;

        public string RG
        {
            get { return _rg; }
            set { _rg = value; }
        }
        private string _cpf;

        public string CPF
        {
            get { return _cpf; }
            set { _cpf = value; }
        }

        private List<Bagagem> _Bagagem;

        public List<Bagagem> Bagagem
        {
            get { return _Bagagem; }
            set { _Bagagem = value; }
        }

        private Assento _ocupante;

        public Assento Ocupante
        {
            get { return _ocupante; }
            set { _ocupante = value; }
        }

        public Check_in() : base()
        {
            _rg = "";
            _cpf = "";
            _Bagagem = new List<Bagagem>();
            _ocupante = new Assento(this);

        }
    }
}
