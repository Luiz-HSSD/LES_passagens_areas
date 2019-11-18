using Dominio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Jose;

namespace LES_passagens_areas.Api
{
    [Route("api/login")]
    public class LoginApiController:ApiGenerico
    {
        [HttpPost]
        public dynamic Post([FromBody]Usuarios usu)
        {

            res = commands["CONSULTAR"].execute(usu);
            if (res.Entidades.Count > 0)
            {

                return Jose.JWT.Encode( JsonConvert.SerializeObject(res.Entidades.ElementAt(0)),keyCrypt, JwsAlgorithm.HS512);
            }
            else
                return new { Message = "autenticão invalida" };
            
        }
    }
}
