namespace _250317_3PLF_Anmeldesystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Longname { get; set; }
        public List<Registration> Registrations { get; set; } = new List<Registration>();
    }
}
