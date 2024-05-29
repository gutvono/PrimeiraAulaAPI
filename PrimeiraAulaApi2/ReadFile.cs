using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiraAulaApi2
{
    public class ReadFile
    {
        public static List<PenalidadesAplicadas> GetData(string path)
        {
            StreamReader read = new(path);
            string JsonString = read.ReadToEnd();

            var obgGeral = JsonConvert.DeserializeObject<MotoristaHabilitado>(JsonString, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

            if (obgGeral != null) return obgGeral.PenalidadesAplicadas;
            return null;
        }
    }
}
