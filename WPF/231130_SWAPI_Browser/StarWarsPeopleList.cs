using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _231130_SWAPI_Browser
{
    internal class StarWarsPeopleList
    {
        [JsonPropertyName("results")]
        public List<StarWarsPeople> Peoples {  get; set; }
        [JsonPropertyName("next")]
        public string? Next { get; set; }
    }
}
