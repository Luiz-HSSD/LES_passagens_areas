using Dominio;
using Core.Aplicacao;

namespace Core.Core
{
    public interface IFachada
    {
         Resultado salvar(EntidadeDominio entidade);
         Resultado alterar(EntidadeDominio entidade);
         Resultado excluir(EntidadeDominio entidade);
         Resultado consultar(EntidadeDominio entidade);
         Resultado visualizar(EntidadeDominio entidade);
         Resultado WebService(EntidadeDominio entidade);


    }
}
