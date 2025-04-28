using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _250217_Kundenliste.Models
{
    public class Artikel
    {
        public int Id { get; set; }
        [Required, MaxLength(100)] public string Name { get; set; }
        [MaxLength(500)] public string Beschreibung { get; set; }
        [Range(0.01, 10000.00)] public decimal Preis { get; set; }
        [Range(0, 10000)] public int Lagerbestand { get; set; }
        [DataType(DataType.Date)] public DateTime Erstellungsdatum { get; set; }

        public int KundeId { get; set; }
        [JsonIgnore]
        public Kunde Kunde { get; set; }
    }
}
