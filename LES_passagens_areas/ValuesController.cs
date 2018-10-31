using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aplicacao;
using LES_passagens_areas.Command;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using Dominio;
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
        [Route("analise/{code}/{codd}/{codee}/{codde}/{co}/{cod}")]
        [HttpGet]
        public string analise(int code, int codd, int codee, int codde,int co, int cod)
        {
            if (codd < 12)
                codd++;
            else
            {
                code++;
                codd = 1;
            }
            DateTime dt_max = new DateTime(code , codd  , 1);
            DateTime dt_min = new DateTime(codee, codde , 1);
            Dominio.Analise analise = new Dominio.Analise() {Data_max=dt_max,Data_min=dt_min  };
                analise.ID = cod;
            commands["CONSULTAR"].execute(analise);
            if (analise.ID == 0)
            {
                Dominio.data asd = new Dominio.data()
                {
                    labels = analise.generic_labels,//new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" },

                };
                var fdsa = new List<Dominio.datasets>() { };
                for (int i = 0; i < analise.resultado.Keys.Count; i++)
                {
                    if (co == 0)
                    {
                        List<EntidadeDominio> b = analise.resultado.Values.ElementAt(i);
                        Random rnd = new Random();
                        string color = "rgb(" + rnd.Next(0, 255) + "," + rnd.Next(0, 255) + " , " + rnd.Next(0, 255) + ")";
                        var goLuiz = new datasets() { label = analise.resultado.Keys.ElementAt(i), backgroundColor = color, borderColor = color, fill = false };
                        var grr = new List<double>() { };
                        foreach (Bagagem sl in b)
                        {
                            grr.Add(sl.peso);
                        }
                        goLuiz.data = grr.ToArray();
                        fdsa.Add(goLuiz);
                    }else if (co == i+1){
                        List<EntidadeDominio> b = analise.resultado.Values.ElementAt(i);
                        Random rnd = new Random();
                        string color = "rgb(" + rnd.Next(0, 255) + "," + rnd.Next(0, 255) + " , " + rnd.Next(0, 255) + ")";
                        var goLuiz = new datasets() { label = analise.resultado.Keys.ElementAt(i), backgroundColor = color, borderColor = color, fill = false };
                        var grr = new List<double>() { };
                        foreach (Bagagem sl in b)
                        {
                            grr.Add(sl.peso);
                        }
                        goLuiz.data = grr.ToArray();
                        fdsa.Add(goLuiz);

                    }
                }
                asd.datasets = fdsa.ToArray();

                Dominio.options asdf = new Dominio.options()
                {
                    responsive = true,
                    title = new title() { display = true, text = "bagagem por voo em tempo (ano " + analise.Data_max.Year + ")" },
                    tooltips = new tooltips() { intersect = false, mode = "index" },
                    hover = new hover() { intersect = true, mode = "nearest" },
                    scales = new scales()
                    {
                        xAxes = new xAxes[] { new xAxes() { display = true, scaleLabel = new scaleLabel() { display = true, labelString = "mês" } } },
                        yAxes = new yAxes[] { new yAxes() { display = true, scaleLabel = new scaleLabel() { display = true, labelString = "peso em Kg" } } }
                    }
                };
                analise.chartsjs.type = "line";
                analise.chartsjs.data = asd;
                analise.chartsjs.options = asdf;
            }
            return JsonConvert.SerializeObject(analise.chartsjs, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });//System.IO.File.ReadAllText("./analise.json");
        }

        [Route("cartao/{cod}")]
        [HttpGet]
        public string cartao(int cod)
        {
            res = commands["CONSULTAR"].execute(new Dominio.Cliente() { ID=cod   });
            if(res.Entidades.Count>0)
            return JsonConvert.SerializeObject(((Cliente)res.Entidades.ElementAt(0)).Cartoes);
            return JsonConvert.SerializeObject(null); 
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
