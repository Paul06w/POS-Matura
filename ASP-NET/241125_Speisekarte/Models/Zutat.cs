namespace _241125_Speisekarte.Models
{
    public class Zutat
    {
        public int Id { get; set; } // Primärschlüssel

        public string Beschreibung { get; set; }
        public int Menge { get; set; }
        public string Einheit { get; set; }

        // Fremdschlüssel und Navigation Property
        public int SpeiseId { get; set; }
        public Speise Speise { get; set; }
    }
}
