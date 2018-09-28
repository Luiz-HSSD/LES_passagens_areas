using System;
using System.Collections.Generic;
using System.Text;
using Dominio;

namespace Core.DAO
{
    public class Check_inDAO : AbstractDAO
    {
        public Check_inDAO() : base("check_in", "check_in_id")
        {
        }

        public override void alterar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override List<EntidadeDominio> consultar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }

        public override void salvar(EntidadeDominio entidade)
        {
            throw new NotImplementedException();
        }
    }
}
