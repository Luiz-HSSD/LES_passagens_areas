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

namespace Core.Controle
{
    public sealed class Fachada : IFachada
    {


        /** 
         * Mapa de DAOS, será indexado pelo nome da entidade 
         * O valor é uma instância do DAO para uma dada entidade; 
         */
        private Dictionary<string, IDAO> daos;

        /**
         * Mapa para conter as regras de negócio de todas operações por entidade;
         * O valor é um mapa que de regras de negócio indexado pela operação
         */
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
            daos = new Dictionary<string, IDAO>();
            /* Intânciando o Map de Regras de Negócio */
            rns = new Dictionary<string, Dictionary<string, List<IStrategy>>>();
            
            //_gerar_log = new Gerar_log();
            parametro_excluir para_ex = new parametro_excluir();
            ClasseDAO claDAO = new ClasseDAO();
            daos.Add(typeof(Classe).Name, claDAO);
            List<IStrategy> rnsSalvarClasse = new List<IStrategy>();
            List<IStrategy> rnsAlterarClasse = new List<IStrategy>();
            List<IStrategy> rnsExcluirClasse = new List<IStrategy>();
            rnsExcluirClasse.Add(para_ex);
            List<IStrategy> rnsConsultarClasse = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsClasse = new Dictionary<string, List<IStrategy>>();
            rnsClasse.Add("SALVAR", rnsSalvarClasse);
            rnsClasse.Add("ALTERAR", rnsAlterarClasse);
            rnsClasse.Add("EXCLUIR", rnsExcluirClasse);
            rnsClasse.Add("CONSULTAR", rnsConsultarClasse);
            rns.Add(typeof(Classe).Name, rnsClasse);

            Validar_passagens val_pass = new Validar_passagens();
            PassagensDAO passDAO = new PassagensDAO();
            daos.Add(typeof(Passagens).Name, passDAO);
            List<IStrategy> rnsSalvarPassagens = new List<IStrategy>();
            rnsSalvarPassagens.Add(val_pass);
            List<IStrategy> rnsAlterarPassagens = new List<IStrategy>();
            rnsAlterarPassagens.Add(val_pass);
            List<IStrategy> rnsExcluirPassagens = new List<IStrategy>();
            rnsExcluirPassagens.Add(para_ex);
            List<IStrategy> rnsConsultarPassagens = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsPassagens = new Dictionary<string, List<IStrategy>>();
            rnsPassagens.Add("SALVAR", rnsSalvarPassagens);
            rnsPassagens.Add("ALTERAR", rnsAlterarPassagens);
            rnsPassagens.Add("EXCLUIR", rnsExcluirPassagens);
            rnsPassagens.Add("CONSULTAR", rnsConsultarPassagens);
            rns.Add(typeof(Passagens).Name, rnsPassagens);

            AviaoDAO aviaoDAO = new AviaoDAO();
            daos.Add(typeof(Aviao).Name, aviaoDAO);
            List<IStrategy> rnsSalvaraviao = new List<IStrategy>();
            List<IStrategy> rnsAlteraraviao = new List<IStrategy>();
            List<IStrategy> rnsExcluiraviao = new List<IStrategy>();
            rnsExcluiraviao.Add(para_ex);
            List<IStrategy> rnsConsultaraviao = new List<IStrategy>();
            Dictionary<string, List<IStrategy>> rnsaviao = new Dictionary<string, List<IStrategy>>();
            rnsaviao.Add("SALVAR", rnsSalvaraviao);
            rnsaviao.Add("ALTERAR", rnsAlteraraviao);
            rnsaviao.Add("EXCLUIR", rnsExcluiraviao);
            rnsaviao.Add("CONSULTAR", rnsConsultaraviao);
            rns.Add(typeof(Aviao).Name, rnsaviao);
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
                catch
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
        public Resultado WebService(EntidadeDominio entidade)
        {
            resultado = new Resultado();
            resultado.Msg = "";
            string nmClasse = entidade.GetType().Name;
            IStrategy Ws;
            switch (nmClasse)
            {
                case "Endereco":
                            //Ws = new WS_cep_json();
                           // resultado.Msg = Ws.processar(entidade); 
                            break;
                default:
                    resultado.Msg = "erro de solitação de serviço!";
                    return resultado;
            }

            List<EntidadeDominio> entidades = new List<EntidadeDominio>();
            entidades.Add(entidade);
            resultado.Entidades = entidades;
            resultado.Msg = "";
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

