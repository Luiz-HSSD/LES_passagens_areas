using Core.Aplicacao;
using Dominio;
namespace LES_passagens_areas.ICommand
{
    public interface ICommand
    {
         Resultado execute(EntidadeDominio entidade) ;
    }
}
