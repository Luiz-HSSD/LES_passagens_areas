using Core.Core;
using Core.DAO;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core.Negocio
{
    public class Exclusao_admin : IStrategy
    {
        public string processar(EntidadeDominio entidade)
        {
            Usuarios classe = (Usuarios)entidade;
            classe=(Usuarios)new UsuariosDAO().consultar(classe).ElementAt(0); 
            if (classe.Permisao == 3)
                return "não se pode excluir o administrador";

            return null;
        }
    }
}
