using _241104_KanbanBoard.Data;
using _241104_KanbanBoard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _241104_KanbanBoard.Controllers
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
            var kanbantasks = _context.Tasks.ToList();
            return View(kanbantasks);
        }

        //public IActionResult ChangeStatus(KanbanTask task)
        //{
        //    //switch (task.Status)
        //    //{
        //    //    case "todo": task.Status = "progress";
        //    //}

        //    if(task.Status == "todo")
        //    {
        //        task.Status = "progress";
        //    }
        //    else if(task.Status == "progress")
        //    {
        //        task.Status = "done";
        //    }
        //    else if (task.Status == "done")
        //    {
        //        task.Status = "todo";
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        public IActionResult ChangeStatus(int id)
        {
            if (id != 0)
            {
                var task = _context.Tasks.SingleOrDefault(x => x.Id == id);
                if (task != null)
                {
                    if (task.Status == "todo")
                    {
                        task.Status = "progress";
                    }
                    else if (task.Status == "progress")
                    {
                        task.Status = "done";
                        task.DoneDate = DateTime.Now;
                    }
                    else if (task.Status == "done")
                    {
                        task.Status = "todo";
                        task.DoneDate = null;
                    }

                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
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
