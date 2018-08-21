using Core.Aplicacao;
using Dominio;

namespace LES_passagens_areas.Command
{
    public class VisualizarCommand : AbstractCommand
    {
        public override Resultado execute(EntidadeDominio entidade)
        {
            return fachada.visualizar(entidade);
        }
    }
}