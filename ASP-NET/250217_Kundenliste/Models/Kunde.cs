using System.ComponentModel.DataAnnotations;

namespace _250217_Kundenliste.Models
{
    public class Kunde
    {
        public int Id { get; set; }
        [MaxLength(20)] public string Vorname { get; set; }
        [MaxLength(20)] public string Nachname { get; set; }
        [EmailAddress] public string Email { get; set; }
        [Phone] public string Telefonnummer { get; set; }
        [MaxLength(100)] public string Adresse { get; set; }
        [DataType(DataType.Date)] public DateTime Geburtsdatum { get; set; }
        public List<Artikel> ArtikelListe { get; set; }
    }
}
