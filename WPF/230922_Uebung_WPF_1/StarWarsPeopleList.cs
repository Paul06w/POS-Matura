using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _230922_Uebung_WPF_1
{
    internal class StarWarsPeopleList
    {
        [JsonPropertyName("next")]
        public List<StarWarsPeople> Peoples { get; set; }
        public string? Next { get; set; }
        public string? getNext()
        {
            return Next;
        }
    }
}
