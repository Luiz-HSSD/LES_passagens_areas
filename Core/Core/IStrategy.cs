using Dominio;

namespace Core.Core
{
    public interface IStrategy
    {
         string processar(EntidadeDominio entidade);
    }
}
