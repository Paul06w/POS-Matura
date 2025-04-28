using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace _230922_Uebung_WPF_1
{
    internal class StarWarsPeople
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public int height { get; set; }
        public int mass { get; set; }
        //public string hair_color { get; set; }
        //public string skin_color { get; set; }
        //public string eye_color { get; set; }
        //public string birth_year { get; set; }
        public int gender { get; set; }
		


        [JsonPropertyName("next")]
        public List<StarWarsPeople> Results { get; set; }
        public string? Next { get; set; }
        public string? getNext()
        {
            return Next;
        }



        /*{
			"name": "Luke Skywalker",
			"height": "172",
			"mass": "77",
			"hair_color": "blond",
			"skin_color": "fair",
			"eye_color": "blue",
			"birth_year": "19BBY",
			"gender": "male",
			"homeworld": "https://swapi.dev/api/planets/1/",
			"films": [
				"https://swapi.dev/api/films/1/",
				"https://swapi.dev/api/films/2/",
				"https://swapi.dev/api/films/3/",
				"https://swapi.dev/api/films/6/"
			],
			"species": [],
			"vehicles": [
				"https://swapi.dev/api/vehicles/14/",
				"https://swapi.dev/api/vehicles/30/"
			],
			"starships": [
				"https://swapi.dev/api/starships/12/",
				"https://swapi.dev/api/starships/22/"
			],
			"created": "2014-12-09T13:50:51.644000Z",
			"edited": "2014-12-20T21:17:56.891000Z",
			"url": "https://swapi.dev/api/people/1/"
		}*/





    }
}
