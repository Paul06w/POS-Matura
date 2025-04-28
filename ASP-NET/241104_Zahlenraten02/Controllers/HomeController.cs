using _241104_Zahlenraten02.Data;
using _241104_Zahlenraten02.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _241104_Zahlenraten02.Controllers
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
            GetHighScores();
            return View(new Score());
        }

        public IActionResult Check(Score score)
        {
            if(score is not null)
            {
                if(score.ZZ <= 0)
                {
                    //New random Number
                    Random rnd = new Random();
                    int nr = rnd.Next(1, 101);
                    Console.WriteLine($"Number = {nr}");
                    score.ZZ = nr;
                    score.Tries = 0;
                }

                if(score.Number > score.ZZ)
                {
                    score.Message = "Too high";
                    score.Tries++;
                }
                else if(score.Number < score.ZZ)
                {
                    score.Message = "Too low";
                    score.Tries++;
                }
                else
                {
                    score.Message = "Hurra, gefunden!";
                    score.Tries++;
                    _context.Scores.Add(score);
                    _context.SaveChanges();
                }
                GetHighScores();
                return View(nameof(Index), score);
            }
            return BadRequest();
        }

        private void GetHighScores()
        {
            List<Score> scores = _context.Scores.ToList();
            scores = _context.Scores.OrderBy(x => x.Tries).ToList();
            var res = scores.Take(5).ToList();
            ViewData["ListOf5"] = res;          //Wrap it in ViewData Object
        }

        public IActionResult Reset(int id)
        {
            return RedirectToAction(nameof(Index));
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
