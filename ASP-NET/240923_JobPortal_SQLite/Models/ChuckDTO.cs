namespace _240923_JobPortal.Models
{
    public class ChuckDTO
    {
        public string JokeOfTheDay { get; set; }
        public DateTime? Today { get; set; }
        public List<JobPosting> JobPostingList { get; set; }
    }
}
