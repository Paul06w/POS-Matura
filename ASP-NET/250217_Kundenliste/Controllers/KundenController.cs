using _250217_Kundenliste.Data;
using _250217_Kundenliste.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace _250217_Kundenliste.Controllers
{
    [Route("api/Values")]
    public class KundenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public KundenController(ApplicationDbContext context) => _context = context;

        [HttpPost("Init")]
        public IActionResult Init()
        {
            var jsonData = System.IO.File.ReadAllText("kunden-artikel.json");
            var kunden = JsonSerializer.Deserialize<List<Kunde>>(jsonData);
            _context.Kunden.AddRange(kunden);
            _context.SaveChanges();
            return Ok("Initialized");
        }

        [HttpGet]
        public IActionResult GetKunden()
        {
            var kunden = _context.Kunden.Include(k => k.ArtikelListe).Take(10).ToList();
            return Ok(kunden);
        }
    }
}
