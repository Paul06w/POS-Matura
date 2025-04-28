using System.Reflection.Metadata.Ecma335;

namespace _241104_Zahlenraten02.Models
{
    public class Score
    {
        public int Id { get; set; }
        public string? User { get; set; }
        public int Number { get; set; }
        public int Tries { get; set; }
        public string? Message { get; set; }
        public int ZZ { get; set; }
    }
}
