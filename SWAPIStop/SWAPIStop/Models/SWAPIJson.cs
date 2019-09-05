using Newtonsoft.Json;
using System.Collections.Generic;

namespace SWAPIStop.Models
{
    public class SWAPIJson
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public List<Starship> Result { get; set; }
    }
}
