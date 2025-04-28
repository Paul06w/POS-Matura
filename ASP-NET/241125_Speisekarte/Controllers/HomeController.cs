using _241125_Speisekarte.Data;
using _241125_Speisekarte.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _241125_Speisekarte.Controllers
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
            // Alle Speisen mit Zutaten aus der Datenbank laden
            var speisen = _context.Speisen.Include(s => s.Zutaten).ToList();
            return View(speisen);
        }

        [HttpPost]
        public IActionResult AddSpeise(string titel, string? notiz, int bewertung)
        {
            var speise = new Speise
            {
                Titel = titel,
                Notiz = notiz,
                Bewertung = bewertung
            };
            _context.Speisen.Add(speise);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteSpeise(int id)
        {
            var speise = _context.Speisen.Include(s => s.Zutaten).FirstOrDefault(s => s.Id == id);
            if (speise != null)
            {
                _context.Speisen.Remove(speise);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditSpeise(Speise speise, List<Zutat> zutaten)
        {
            // Speise aus der Datenbank laden
            var speiseInDb = _context.Speisen.Include(s => s.Zutaten).FirstOrDefault(s => s.Id == speise.Id);

            if (speiseInDb != null)
            {
                // Speise aktualisieren
                speiseInDb.Titel = speise.Titel;
                speiseInDb.Notiz = speise.Notiz;
                speiseInDb.Bewertung = speise.Bewertung;

                // Zutaten aktualisieren
                foreach (var zutat in zutaten)
                {
                    if (zutat.Id > 0) // Existierende Zutat
                    {
                        var zutatInDb = speiseInDb.Zutaten.FirstOrDefault(z => z.Id == zutat.Id);
                        if (zutatInDb != null)
                        {
                            zutatInDb.Beschreibung = zutat.Beschreibung;
                            zutatInDb.Menge = zutat.Menge;
                            zutatInDb.Einheit = zutat.Einheit;
                        }
                    }
                    else // Neue Zutat
                    {
                        speiseInDb.Zutaten.Add(new Zutat
                        {
                            Beschreibung = zutat.Beschreibung,
                            Menge = zutat.Menge,
                            Einheit = zutat.Einheit,
                            SpeiseId = speise.Id
                        });
                    }
                }

                // Entfernte Zutaten ermitteln
                var zuEntfernendeZutaten = speiseInDb.Zutaten
                    .Where(z => !zutaten.Any(zu => zu.Id == z.Id))
                    .ToList();
                _context.Zutaten.RemoveRange(zuEntfernendeZutaten);

                _context.SaveChanges();
            }

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
