using _240923_JobPortal.Data;
using _240923_JobPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _240923_JobPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ValuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Seed")]
        public IActionResult Seed()
        {
            JobPosting jobPosting1 = new JobPosting();
            jobPosting1.JobTitle = "Software Developer";
            jobPosting1.JobLocation = "St.Johann";

            JobPosting jobPosting2 = new();
            jobPosting2.JobTitle = "Software Architekt";
            jobPosting2.JobLocation = "Salzburg";

            JobPosting jobPosting3 = new() 
            { 
                JobTitle = "Software Tester", 
                JobLocation = "Wien" 
            };

            _context.JobPostings.Add(jobPosting1);
            _context.JobPostings.Add(jobPosting2);
            _context.JobPostings.Add(jobPosting3);

            List<JobPosting> list = new()
            {
                new()
                {
                    JobTitle = "Software Developer 2",
                    JobLocation = "St. Johann"
                },
                new()
                {
                    JobTitle = "Software Tester 2",
                    JobLocation = "Wien"
                },
                new()
                {
                    JobTitle = "Software Architekt Solutions",
                    JobLocation = "Salzburg"
                }

            };
            _context.JobPostings.AddRange(list);

            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var allJobs = _context.JobPostings.ToArray();
            return Ok(allJobs);
        }

        [HttpPost("DeleteAll")]
        public IActionResult DeleteAll()
        {
            _context.JobPostings.ExecuteDelete();
            return Ok("Deleted");
        }

    }
}
