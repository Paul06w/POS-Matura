using System.Diagnostics;
using _250217_Kundenliste.Data;
using _250217_Kundenliste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _250217_Kundenliste.Controllers
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
            var kunden = _context.Kunden.Include(k => k.ArtikelListe).Take(10).ToList();
            return View(kunden);
        }

        [HttpPost]
        public IActionResult AddKunde(string vorname, string nachname, string email, string telefonnummer, string adresse, DateTime geburtsdatum)
        {
            var kunde = new Kunde
            {
                Vorname = vorname,
                Nachname = nachname,
                Email = email,
                Telefonnummer = telefonnummer,
                Adresse = adresse,
                Geburtsdatum = geburtsdatum
            };
            _context.Kunden.Add(kunde);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (id != 0)
            {
                var kunde = _context.Kunden.Include(k => k.ArtikelListe).SingleOrDefault(x => x.Id == id);
                if (kunde != null)
                {
                    return View(kunde);
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult EditKunde(Kunde kunde, List<Artikel> artikel)
        {
            // Lade den Kunden aus der Datenbank inkl. Artikel
            var kundeInDb = _context.Kunden.Include(k => k.ArtikelListe).FirstOrDefault(k => k.Id == kunde.Id);

            if (kundeInDb != null)
            {
                // Aktualisiere Kundendaten
                kundeInDb.Vorname = kunde.Vorname;
                kundeInDb.Nachname = kunde.Nachname;
                kundeInDb.Email = kunde.Email;
                kundeInDb.Telefonnummer = kunde.Telefonnummer;
                kundeInDb.Adresse = kunde.Adresse;
                kundeInDb.Geburtsdatum = kunde.Geburtsdatum;

                // Aktualisiere die Artikel
                foreach (var art in artikel)
                {
                    if (art.Id > 0) // Existierender Artikel
                    {
                        var artikelInDb = kundeInDb.ArtikelListe.FirstOrDefault(a => a.Id == art.Id);
                        if (artikelInDb != null)
                        {
                            artikelInDb.Name = art.Name;
                            artikelInDb.Beschreibung = art.Beschreibung;
                            artikelInDb.Preis = art.Preis;
                            artikelInDb.Lagerbestand = art.Lagerbestand;
                            artikelInDb.Erstellungsdatum = art.Erstellungsdatum;
                        }
                    }
                    else // Neuer Artikel
                    {
                        kundeInDb.ArtikelListe.Add(new Artikel
                        {
                            Name = art.Name,
                            Beschreibung = art.Beschreibung,
                            Preis = art.Preis,
                            Lagerbestand = art.Lagerbestand,
                            Erstellungsdatum = art.Erstellungsdatum
                        });
                    }
                }

                // Entfernte Artikel ermitteln
                var zuEntfernendeArtikel = kundeInDb.ArtikelListe
                    .Where(a => !artikel.Any(at => at.Id == a.Id))
                    .ToList();
                _context.Artikel.RemoveRange(zuEntfernendeArtikel);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Lade den Kunden inkl. Artikel
            var kunde = _context.Kunden.Include(k => k.ArtikelListe).FirstOrDefault(k => k.Id == id);

            if (kunde != null)
            {
                // Entferne alle Artikel
                _context.Artikel.RemoveRange(kunde.ArtikelListe);

                // Kunden löschen
                _context.Kunden.Remove(kunde);
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
