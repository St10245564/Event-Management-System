using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using System.Threading.Tasks;
using MyPart3.Models;
using MyPart3.Data;
using Microsoft.EntityFrameworkCore;

namespace MyPart3.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Booking
        public async Task<IActionResult> Index(string search, string eventType, DateTime? startDate, DateTime? endDate, bool? available)
        {
            var bookings = _context.Bookings
                .Include(b => b.Event)
                    .ThenInclude(e => e.EventType)
                .Include(b => b.Venue)
                    .ThenInclude(v => v.EventType)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                bookings = bookings.Where(b => b.Event.Name.Contains(search));
            }

            if (!string.IsNullOrEmpty(eventType))
            {
                bookings = bookings.Where(b => b.Event.EventType.Name == eventType);
            }

            if (startDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                bookings = bookings.Where(b => b.BookingDate <= endDate.Value);
            }

            if (available.HasValue)
            {
                bookings = bookings.Where(b => b.Venue.IsAvailable == available.Value);
            }

            return View(await bookings.ToListAsync());
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            ViewData["Events"] = _context.Events.Include(e => e.EventType).ToList();
            ViewData["Venues"] = _context.Venues.Include(v => v.EventType).ToList();
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Check if the venue is available on the selected date
                var existingBooking = await _context.Bookings
                    .AnyAsync(b => b.VenueId == booking.VenueId && b.BookingDate == booking.BookingDate);

                if (existingBooking)
                {
                    ModelState.AddModelError("", "The selected venue is already booked on this date.");
                    ViewData["Events"] = _context.Events.Include(e => e.EventType).ToList();
                    ViewData["Venues"] = _context.Venues.Include(v => v.EventType).ToList();
                    return View(booking);
                }

                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Events"] = _context.Events.Include(e => e.EventType).ToList();
            ViewData["Venues"] = _context.Venues.Include(v => v.EventType).ToList();
            return View(booking);
        }

        // GET: Booking/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            ViewData["Events"] = _context.Events.Include(e => e.EventType).ToList();
            ViewData["Venues"] = _context.Venues.Include(v => v.EventType).ToList();
            return View(booking);
        }

        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Check if the venue is available on the selected date, excluding the current booking
                var existingBooking = await _context.Bookings
                    .AnyAsync(b => b.VenueId == booking.VenueId && b.BookingDate == booking.BookingDate && b.Id != booking.Id);

                if (existingBooking)
                {
                    ModelState.AddModelError("", "The selected venue is already booked on this date.");
                    ViewData["Events"] = _context.Events.Include(e => e.EventType).ToList();
                    ViewData["Venues"] = _context.Venues.Include(v => v.EventType).ToList();
                    return View(booking);
                }

                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Events"] = _context.Events.Include(e => e.EventType).ToList();
            ViewData["Venues"] = _context.Venues.Include(v => v.EventType).ToList();
            return View(booking);
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Event)
                    .ThenInclude(e => e.EventType)
                .Include(b => b.Venue)
                    .ThenInclude(v => v.EventType)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }
    }
}
