using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using System.IO;
using System;
using MyPart3.Models;
using MyPart3.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPart3.Services;

namespace MyPart3.Controllers
{


    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBlobService _blobService;

        public VenueController(ApplicationDbContext context, IBlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }



        // GET: Venue
        public async Task<IActionResult> Index()
        {
            var venues = _context.Venues.Include(v => v.EventType);
            return View(await venues.ToListAsync());
        }

        // GET: Venue/Create
        // GET: Venue/Create
        public IActionResult Create()
        {
            // Seed EventTypes only if none exist
            if (!_context.EventTypes.Any())
            {
                _context.EventTypes.AddRange(
                    new EventType { Name = "Conference" },
                    new EventType { Name = "Wedding" },
                    new EventType { Name = "Concert" },
                    new EventType { Name = "Workshop" }
                );
                _context.SaveChanges();
            }

            // Pass the list to the view for dropdown
            ViewData["EventTypes"] = _context.EventTypes.ToList();

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venue venue, IFormFile image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (image != null && image.Length > 0)
                    {
                        try
                        {
                            // Upload image to Azure Blob Storage
                            venue.ImageUrl = await _blobService.UploadFileAsync(image);
                        }
                        catch (Exception ex)
                        {
                            // Log error or show image-specific error
                            ModelState.AddModelError("", $"Image upload failed: {ex.Message}");
                        }
                    }

                    // Save venue to database
                    _context.Add(venue);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // Log the error and display to user
                ModelState.AddModelError("", $"Something went wrong: {ex.Message}");
            }

            // Reload EventTypes in case of error
            ViewData["EventTypes"] = _context.EventTypes.ToList();
            return View(venue);
        }
    }
}





      