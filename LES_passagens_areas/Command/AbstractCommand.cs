using Core.Aplicacao;
using Dominio;
using Core.Core;
using Core.Controle;

namespace LES_passagens_areas.Command
{
    public abstract class AbstractCommand : ICommand.ICommand
    {
        protected IFachada fachada = Fachada.UniqueInstance;

        public abstract Resultado execute(EntidadeDominio entidade);
       
    }
}