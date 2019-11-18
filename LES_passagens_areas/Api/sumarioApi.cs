using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LES_passagens_areas.Api
{
    public class sumarioApi:ApiGenerico
    {
        public string codd;
        public IEnumerable<EntidadeDominio> lt = new List<EntidadeDominio>();
        public void OnGet(int cod)
        {
            if (!autenticar(2))
                return;
            if (cod != 0)
                codd = cod.ToString();
            SumarioVoo sv = new SumarioVoo() { Codd = cod };
            commands["CONSULTAR"].execute(sv);
            if (cod != 0)
                lt = sv.ls;
        }
    }
}
