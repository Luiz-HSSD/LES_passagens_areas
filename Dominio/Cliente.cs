using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente:EntidadeDominio
    { 


        private string nome;
        private string rg;
        private string cpf;
        private char sexo;
        private DateTime dt_nas;
        private Endereco endereco;
        private List<Cartao_Credito> cartoes;
        private Usuarios _usuario;

        public Usuarios usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        

        public List<Cartao_Credito> Cartoes
        {
            get { return cartoes; }
            set { cartoes = value; }
        }

        public Endereco Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        public DateTime Dt_Nas
        {
            get { return dt_nas; }
            set { dt_nas = value; }
        }

        

        public char Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        public string Cpf
        {
            get { return cpf; }
            set { cpf = value; }
        }

        public string Rg
        {
            get { return rg; }
            set { rg = value; }
        }

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public Cliente() : base()
        {
            Nome = "";
            Rg = "";
            Cpf = "";
            Sexo = 'M';
            _usuario = new Usuarios();
            Dt_Nas = Convert.ToDateTime("01/01/1995 03:30");
            Endereco = new Endereco();
            Cartoes = new List<Cartao_Credito>();
            
        }


    }
}
