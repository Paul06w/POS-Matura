using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _231130_SWAPI_Browser
{
    internal class Starship
    {
        [JsonPropertyName("name")]
        public string Name {  get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("starship_class")]
        public string Starshipclass { get; set; }


    }
}
