using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyPart3.Models;
using MyPart3.Data;

namespace MyPart3.Controllers
{
    public class EventTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventType
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventTypes.ToListAsync());
        }

        // GET: EventType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventType eventType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventType);
        }

        // Additional actions: Edit, Details, Delete
    }
}
