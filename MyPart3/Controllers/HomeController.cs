using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPart3.Data;

namespace MyPart3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.VenueCount = await _context.Venues.CountAsync();
            ViewBag.EventCount = await _context.Events.CountAsync();
            ViewBag.BookingCount = await _context.Bookings.CountAsync();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
