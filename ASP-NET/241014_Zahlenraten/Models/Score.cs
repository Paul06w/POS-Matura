namespace _241014_Zahlenraten.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int Tries { get; set; }
        public int Target { get; set; }
        public string? UserName { get; set; }
        public DateTime? Date { get; set; }
    }
}
