using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace _250317_3PLF_Anmeldesystem.Models
{
    public class Registration
    {
        public int Id { get; set; }
       
        [MaxLength(20)]
        public string Firstname { get; set; }

        [MaxLength(20)]
        public string Lastname { get; set; }

        [EmailAddress()]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime? AdmittedDate { get; set; }

        public int? DepartmentId { get; set; }

        [JsonIgnore]
        public Department? Department { get; set; }
    }
}
