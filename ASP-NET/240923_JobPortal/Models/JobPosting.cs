namespace _240923_JobPortal.Models
{
    public class JobPosting
    {
        public int Id { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string? JobLocation { get; set; }
        public string? UserName { get; set; }
        public byte[]? CompanyImage { get; set; }

    }
}
