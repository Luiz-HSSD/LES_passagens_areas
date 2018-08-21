using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Core.Utils
{
    public class getmotivos
    {
        public List<string> vai(int cat)
        {
            List<string> retorno=new List<string>();
            string[] bora;
            if (cat == 10)
                 bora= File.ReadAllLines("./mt");
            else
                bora = File.ReadAllLines("./mtd");

            return retorno;
        }
    }
}
