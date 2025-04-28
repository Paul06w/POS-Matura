using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _231130_SWAPI_Browser
{
    internal class StarWarsPeople
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("birth_year")]
        public string Birthyear { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("starships")]
        public List<string>? Starships { get; set; }


    }
}
