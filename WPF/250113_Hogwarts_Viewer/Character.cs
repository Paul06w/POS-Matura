using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace _250113_Hogwarts_Viewer
{
    public class Character
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("alternate_names")]
        public List<string> AlternateNames { get; set; }

        [JsonPropertyName("species")]
        public string Species { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("house")]
        public string House { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonPropertyName("yearOfBirth")]
        public int? YearOfBirth { get; set; }

        [JsonPropertyName("wizard")]
        public bool Wizard { get; set; }

        [JsonPropertyName("ancestry")]
        public string Ancestry { get; set; }

        [JsonPropertyName("eyeColour")]
        public string EyeColour { get; set; }

        [JsonPropertyName("hairColour")]
        public string HairColour { get; set; }

        [JsonPropertyName("wand")]
        public Wand Wand { get; set; }

        [JsonPropertyName("patronus")]
        public string Patronus { get; set; }

        [JsonPropertyName("hogwartsStudent")]
        public bool HogwartsStudent { get; set; }

        [JsonPropertyName("hogwartsStaff")]
        public bool HogwartsStaff { get; set; }

        [JsonPropertyName("actor")]
        public string Actor { get; set; }

        [JsonPropertyName("alternate_actors")]
        public List<string> AlternateActors { get; set; }

        [JsonPropertyName("alive")]
        public bool Alive { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        //Convert the image URL to a BitmapImage
        public BitmapImage ImageSource
        {
            get
            {
                if (string.IsNullOrEmpty(Image))
                    return new BitmapImage(new Uri("https://icon-library.com/images/anonymous-person-icon/anonymous-person-icon-23.jpg"));

                try
                {
                    return new BitmapImage(new Uri(Image));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading the Image: {ex.Message}");
                    return null;
                }
            }
        }
    }

    public class Wand
    {
        [JsonPropertyName("wood")]
        public string Wood { get; set; }

        [JsonPropertyName("core")]
        public string Core { get; set; }

        [JsonPropertyName("length")]
        public double? Length { get; set; }
    }
}
