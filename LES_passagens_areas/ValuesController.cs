using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacao;
using LES_passagens_areas.Command;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LES_passagens_areas
{
    [Route("api/devil")]
    public class ValuesController : Controller
    {
        protected Resultado res { get; set; } = new Resultado();
        protected Dictionary<string, ICommand.ICommand> commands { get; set; } = new Dictionary<string, ICommand.ICommand>();

        public ValuesController()
        {
            commands.Add("SALVAR", new SalvarCommand());
            commands.Add("ALTERAR", new AlterarCommand());
            commands.Add("EXCLUIR", new ExcluirCommand());
            commands.Add("CONSULTAR", new ConsultarCommand());
            commands.Add("VISUALIZAR", new VisualizarCommand());
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        [HttpGet("{cod}")]
        public string aeroporto(int cod)
        {
            res = commands["CONSULTAR"].execute(new Dominio.Aeroporto() { ID = cod });
            
            return JsonConvert.SerializeObject(res.Entidades) ;
        }
        [Route("voo/{codd}")]
        [HttpGet]
        public string voo(int codd)
        {
            res = commands["CONSULTAR"].execute(new Dominio.Passagens() { LO_partida = new Dominio.Aeroporto() { ID = codd } });

            return JsonConvert.SerializeObject(res.Entidades);
        }

        [Route("bilhete/{codd}")]
        [HttpGet]
        public string bilhete(int codd)
        {
            res = commands["CONSULTAR"].execute(new Dominio.Bilhete() { passagem=new Dominio.Viagem() { Voo= new Dominio.Passagens() { ID=codd } } });

            return JsonConvert.SerializeObject(res.Entidades);
        }
        [Route("assento/{codd}/{cod}")]
        [HttpGet]
        public string assento(int codd,int cod)
        {
            res = commands["CONSULTAR"].execute(new Dominio.Assento(new Dominio.Check_in()) {viagem=new Dominio.Passagens() {ID=codd }, tipo=new Dominio.Classe() {ID=cod } });

            return JsonConvert.SerializeObject(res.Entidades, Formatting.None, new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
        [Route("analise/{code}/{codd}/{cod}")]
        [HttpGet]
        public string analise(int code, int codd, int cod)
        {
            if (codd < 12)
                codd++;
            else
            {
                code++;
                codd = 1;
            }
            DateTime dt_max = new DateTime(code,codd , 1);

            Dominio.Analise analise = new Dominio.Analise() {Data_max=dt_max };
                analise.ID = cod;
            commands["CONSULTAR"].execute(analise);
            
            return JsonConvert.SerializeObject(analise.chartsjs, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });//System.IO.File.ReadAllText("./analise.json");
        }
        //JsonConvert.SerializeObject(res.Entidades, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
