using System.Diagnostics;
using _250317_3PLF_Anmeldesystem.Data;
using _250317_3PLF_Anmeldesystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _250317_3PLF_Anmeldesystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var registartions = _context.Registrations.Include(r => r.Department).ToList();
            return View(registartions);
        }

        public IActionResult Anmeldung()
        {
            var departments = new List<SelectListItem>
            {
                new SelectListItem { Value = "INF", Text = "Informatik (INF)" },
                new SelectListItem { Value = "ET", Text = "Elektrotechnik (ET)" },
                new SelectListItem { Value = "BT", Text = "Bautechnik (BT)" }
            };

            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        public IActionResult AnmeldungHTL(Registration registration, string Department)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Name == Department);
            if (department == null)
            {
                return View("Anmeldung", registration);
            }
            registration.RegistrationDate = DateTime.Now;
            registration.DepartmentId = department.Id;
            _context.Registrations.Add(registration);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
