using Dominio;
using Core.Aplicacao;

namespace LES_passagens_areas.Command
{
    public class ConsultarCommand : AbstractCommand
    {
        public override Resultado execute(EntidadeDominio entidade)
        {
            return fachada.consultar(entidade);
        }
    }
}