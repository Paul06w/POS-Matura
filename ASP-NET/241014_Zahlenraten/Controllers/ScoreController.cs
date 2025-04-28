using _241014_Zahlenraten.Data;
using _241014_Zahlenraten.Models;
using Microsoft.AspNetCore.Mvc;

namespace _241014_Zahlenraten.Controllers
{
    public class ScoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var highScores = _context.HighScores.Where(x => x.UserName == User.Identity.Name).ToList();       //Nur Ausgabe des angemeldeten Benutzers
            var highScores = _context.HighScores.ToList();                                                      //Ausgabe aller Benutzer --> danach einfärben
            var sortedScores = highScores.OrderBy(x => x.Tries).ToList();

            int attempts = HttpContext.Session.GetInt32("Attempts") ?? 0;                                       //Sessions für Speicherung der Daten 
            ViewBag.Attempts = attempts;

            int randomNumber = HttpContext.Session.GetInt32("RandomNumber") ?? new Random().Next(1, 101);
            HttpContext.Session.SetInt32("RandomNumber", randomNumber);
            ViewBag.RandomNumber = randomNumber;

            string message = HttpContext.Session.GetString("Message") ?? "";
            ViewBag.Message = message;

            return View(sortedScores);
        }

        public IActionResult MakeGuess(int guess)
        {
            int attempts = HttpContext.Session.GetInt32("Attempts") ?? 0;
            attempts++;
            HttpContext.Session.SetInt32("Attempts", attempts);

            int randomNumber = HttpContext.Session.GetInt32("RandomNumber") ?? new Random().Next(1, 101);

            string message = HttpContext.Session.GetString("Message") ?? "";

            if (guess == randomNumber)
            {
                message = $"Richtig! Du hast es in {attempts} Versuchen geschafft. Nächste Runde!";

                Score score = new Score();
                score.Tries = attempts;
                score.Target = randomNumber;
                score.UserName = User.Identity.Name;
                score.Date = DateTime.Now;

                _context.HighScores.Add(score);
                _context.SaveChanges();

                HttpContext.Session.Clear();
            }
            else if(guess < randomNumber)
            {
                message = "Zu niedrig! Versuch es nochmal.";
            }
            else if(guess > randomNumber)
            {
                message = "Zu hoch! Versuch es nochmal.";
            }

            HttpContext.Session.SetString("Message", message);
            

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Restart()
        {
            HttpContext.Session.Clear();                            //Session wird gecleared um neue Runde zu starten
            return RedirectToAction(nameof(Index));
        }
    }
}
