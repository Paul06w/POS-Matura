namespace _241125_Speisekarte.Models
{
    public class Speise
    {
        public int Id { get; set; } // Primärschlüssel
        public string Titel { get; set; }
        public string? Notiz { get; set; }
        public int Bewertung { get; set; }

        // Navigation Property
        public List<Zutat> Zutaten { get; set; } = new();
    }
}
