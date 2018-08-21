using Dominio;

namespace Core.Core
{
    interface IStrategy
    {
         string processar(EntidadeDominio entidade);
    }
}
