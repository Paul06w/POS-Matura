using _250317_3PLF_Anmeldesystem.Data;
using _250317_3PLF_Anmeldesystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace _250317_3PLF_Anmeldesystem.Controllers
{
    [Route("api/Anmeldung")]
    [ApiController]
    public class AnmeldungController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AnmeldungController(ApplicationDbContext context) => _context = context;

        [HttpPost("Seed")]
        public IActionResult Seed()
        {
            if (!_context.Departments.Any())
            {
                var departments = new List<Department>
                {
                    new() { Name = "INF", Longname = "Die tolle Informatik-Abteilung" },
                    new() { Name = "ET", Longname = "Elektrotechnik ist immer cool" },
                    new() { Name = "BT", Longname = "Bautechnik geht auch" }
                };
                _context.Departments.AddRange(departments);
                _context.SaveChanges();
            }

            var departmentDict = _context.Departments.ToDictionary(d => d.Name, d => d.Id);

            AddRegistrationsFromJson("./Data/INF-Registrations.json", departmentDict["INF"]);
            AddRegistrationsFromJson("./Data/ET-Registrations.json", departmentDict["ET"]);
            AddRegistrationsFromJson("./Data/BT-Registrations.json", departmentDict["BT"]);

            return Ok("Datenbank initialisiert");
        }

        private void AddRegistrationsFromJson(string filePath, int departmentId)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return;
            }

            var jsonData = System.IO.File.ReadAllText(filePath);
            var registrations = JsonSerializer.Deserialize<List<Registration>>(jsonData);

            foreach (var reg in registrations)
            {
                reg.RegistrationDate = DateTime.Now;
                reg.DepartmentId = departmentId;
            }

            _context.Registrations.AddRange(registrations);
            _context.SaveChanges();
        }

        [HttpGet("GetAllFromDepartments")]
        public IActionResult GetAllFromDepartments([FromBody] List<string> departmentNames)
        {
            if (departmentNames == null)
            {
                return BadRequest("Keine Abteilungen erhalten!");
            }

            var departments = _context.Departments
                .Where(d => departmentNames.Contains(d.Name))
                .Include(d => d.Registrations)
                .ToList();

            if (!departments.Any())
            {
                return NotFound("Keine passenden Abteilungen gefunden.");
            }

            return Ok(departments);
        }

    }
}
