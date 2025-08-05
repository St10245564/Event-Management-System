using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MyPart3.Models;
using MyPart3.Data;

namespace MyPart3.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var events = _context.Events.Include(e => e.EventType).Include(e => e.Venue);
            return View(await events.ToListAsync());
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            ViewData["Venues"] = _context.Venues.ToList();
            ViewData["EventTypes"] = _context.EventTypes.ToList();
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                // Ensure the Venue and EventType are selected
                if (@event.VenueId == 0 || @event.EventTypeId == 0)
                {
                    ModelState.AddModelError("", "Please select both Venue and Event Type.");
                    ViewData["Venues"] = _context.Venues.ToList();
                    ViewData["EventTypes"] = _context.EventTypes.ToList();
                    return View(@event);
                }

                // Add event to the database
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Return to the view if validation fails
            ViewData["Venues"] = _context.Venues.ToList();
            ViewData["EventTypes"] = _context.EventTypes.ToList();
            return View(@event);
        }
    }
}
