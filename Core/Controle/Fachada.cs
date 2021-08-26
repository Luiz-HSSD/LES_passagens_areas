using Core.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Aplicacao;
using Dominio;
using Core.DAO;
using Core.Negocio;
using Core.Utils;
namespace Core.Controle
{
    public sealed class Fachada : IFachada
    {

        private Dictionary<string, IDAO> daos;

        private Dictionary<string, Dictionary<string, List<IStrategy>>> rns;

        /*
        private Gerar_log _gerar_log;

        public Gerar_log gerar_log
        {
            get { return _gerar_log; }
            set { _gerar_log = value; }
        }
        */

        private Resultado resultado;
        private Fachada()
        {
            new ClasseInjetoraFachada().Pega(out rns,out daos);

            //*/
        }
        private static readonly Fachada Instance = new Fachada();

        public static Fachada UniqueInstance
        {
            get { return Instance; }
        }
        public Resultado salvar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "SALVAR");


            if (string.IsNullOrEmpty(msg))
            {
                IDAO dao = daos[nmClasse];
                dao.salvar(entidade);
                //gerar_log.processar(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades=entidades;

            }
            else
            {
                resultado.Msg=msg;


            }

            return resultado;
        }

        public Resultado alterar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "ALTERAR");

            if (string.IsNullOrEmpty(msg))
            {
                IDAO dao = daos[nmClasse];
                dao.alterar(entidade);
                //gerar_log.processar(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades=entidades;
            }
            else
            {
                resultado.Msg=msg;


            }

            return resultado;

        }


        public Resultado excluir(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "EXCLUIR");


            if (string.IsNullOrEmpty(msg))
            {
                
                IDAO dao = daos[nmClasse];
                dao.excluir(entidade);
                //gerar_log.processar(entidade);
                List<EntidadeDominio> entidades = new List<EntidadeDominio>();
                entidades.Add(entidade);
                resultado.Entidades=entidades;
            }
            else
            {
                resultado.Msg=msg;
            }

            return resultado;

        }

        public Resultado consultar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            string nmClasse = entidade.GetType().Name;
            string msg = executarRegras(entidade, "CONSULTAR");


            if (string.IsNullOrEmpty( msg))
            {
                IDAO dao = daos[nmClasse];
                try
                {

                    resultado.Entidades=dao.consultar(entidade);
                }
                catch(Exception e)
                {

                }
            }
            else
            {
                resultado.Msg=msg;

            }

            return resultado;

        }

        public Resultado visualizar(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            resultado.Entidades=new List<EntidadeDominio>(1);
            resultado.Entidades.Add(entidade);
            return resultado;

        }


        private string executarRegras(EntidadeDominio entidade, string operacao)
        {
            string nmClasse = entidade.GetType().Name;
            StringBuilder msg = new StringBuilder();

            Dictionary<string, List<IStrategy>> regrasOperacao = rns[nmClasse];


            if (regrasOperacao != null)
            {
                List<IStrategy> regras = regrasOperacao[operacao];

                if (regras != null)
                {
                    foreach (IStrategy s in regras)
                    {
                        string m = s.processar(entidade);

                        if (m != null)
                        {
                            msg.Append(m);
                            msg.Append("\n");
                        }
                    }
                }

            }

            if (msg.Length > 0)
                return msg.ToString();
            else
                return null;


        }
    }
    
}