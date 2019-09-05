using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SWAPIStop.Models
{
    public class Starship
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("consumables")]
        public string Consumables { get; set; }

        [JsonProperty("MGLT")]
        public string MGLT { get; set; }
    }
}
