using _240923_JobPortal.Data;
using _240923_JobPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace _240923_JobPortal.Controllers
{
    public class JobPostingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobPostingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string joke = "Witz";
            ViewData["Message"] = joke;
            ViewData["Today"] = DateTime.Now;

            var jobPostings = _context.JobPostings.Where(x => x.UserName == User.Identity.Name).ToList();

            ChuckDTO chuckDTO = new ChuckDTO();
            chuckDTO.JobPostingList = jobPostings;
            chuckDTO.JokeOfTheDay = joke;
            chuckDTO.Today = DateTime.Now;
            return View(chuckDTO);

            //return View(jobPostings);
        }

        public IActionResult CreateEditJobPosting(int id)
        {
            if(id != 0)
            {
                var jobFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);
                if(jobFromDb != null)
                {
                    return View(jobFromDb);
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }


        public IActionResult CreateEditJob(JobPosting jobPosting, IFormFile file)
        {
            var jobFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == jobPosting.Id);
            if(jobFromDb != null)
            {
                jobFromDb.JobTitle = jobPosting.JobTitle;
                jobFromDb.JobLocation = jobPosting.JobLocation;
                jobFromDb.CompanyImage = jobPosting.CompanyImage;
            }
            else
            {
                jobPosting.UserName = User.Identity.Name;
                //Hier wird gespeichert
                _context.JobPostings.Add(jobPosting);
            }

            if (file != null) 
            { 
                using (var ms = new MemoryStream()) 
                { 
                    file.CopyTo(ms); 
                    var bytes = ms.ToArray(); 
                    jobPosting.CompanyImage = bytes; 
                } 
            }

            _context.SaveChanges();

            //Dann wieder Hauptseite anzeigen
            return RedirectToAction(nameof(Index));
        }


        public IActionResult DeleteJobPosting(int id)
        {
            if (id != 0)
            {
                var jobFromDb = _context.JobPostings.SingleOrDefault(x => x.Id == id);
                if (jobFromDb != null)
                {
                    _context.JobPostings.Remove(jobFromDb);
                    _context.SaveChanges();
                    
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
    }
}
