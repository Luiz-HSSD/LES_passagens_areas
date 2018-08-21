using System;
using System.Collections.Generic;
using Dominio;


namespace Core.Aplicacao
{
    public class Resultado:Entidadeaplicacao
    {

        private string msg;
        private List<EntidadeDominio> entidades;

        public List<EntidadeDominio> Entidades
        {
            get { return entidades; }
            set { entidades = value; }
        }

        public string Msg
        {
            get { return msg; }
            set { msg = value; }
        }



        public Resultado()
        {
            Msg="";
            Entidades = new List<EntidadeDominio>();
        }
    

    }
}
