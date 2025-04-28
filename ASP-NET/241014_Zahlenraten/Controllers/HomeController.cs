using _241014_Zahlenraten.Data;
using _241014_Zahlenraten.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _241014_Zahlenraten.Controllers
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
            HttpContext.Session.Clear();                        //Session löschen, damit es nacher wieder von vorne anfängt
            var highScores = _context.HighScores.ToList();
            var sortedScores = highScores.OrderBy(x => x.Tries).ToList();
            return View(sortedScores);
        }

        public IActionResult Privacy()
        {
            HttpContext.Session.Clear();                        //Session löschen, damit es nacher wieder von vorne anfängt
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
