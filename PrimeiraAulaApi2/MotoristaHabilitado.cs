using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeiraAulaApi2
{
    internal class MotoristaHabilitado
    {
        [JsonProperty("penalidades_aplicadas")]
        public List<PenalidadesAplicadas> PenalidadesAplicadas { get; set; }
    }
}
