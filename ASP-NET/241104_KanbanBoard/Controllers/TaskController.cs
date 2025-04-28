using _241104_KanbanBoard.Data;
using _241104_KanbanBoard.Models;
using Microsoft.AspNetCore.Mvc;

namespace _241104_KanbanBoard.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tasks = _context.Tasks.Where(x => x.UserName == User.Identity.Name).ToList();
            return View(tasks);
        }

        public IActionResult CreateOrEditTask(int id)
        {
            if(id == 0)
            {
				var task = _context.Tasks.SingleOrDefault(x => x.Id == id);
                if(task == null)
                {
					return View(task);
				}
                else
                {
                    return NotFound();
                }
			}

            return View(); 
        }

        public IActionResult EditTask(KanbanTask task)
        {
            var ktask = _context.Tasks.SingleOrDefault(x => x.Id == task.Id);

            if(ktask != null)
            {
                ktask.Todo = task.Todo;
                ktask.Responsible = task.Responsible;
                ktask.DueDate = task.DueDate;                
            }
            else
            {
                task.UserName = User.Identity.Name;
                task.Status = "todo";
                _context.Tasks.Add(task);
            }

            _context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult DeleteTask(int id)
        {
            if(id != 0)
            {
                var task = _context.Tasks.SingleOrDefault(x => x.Id == id);
                if(task != null)
                {
                    _context.Tasks.Remove(task);
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
