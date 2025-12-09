using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaRutaNet.Models;

namespace LaRutaNet.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private readonly LarutaContext _context;

        public ServicesController(LarutaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Services
                .Include(s => s.Community)
                .Include(s => s.UserHistory)
                .ToListAsync();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            ModelState.Remove("Community");
            ModelState.Remove("UserHistory");


            var community = await _context.Communities
                .FirstOrDefaultAsync(c => c.Id == 7);

            if (community == null)
            {
                TempData["ErrorMessage"] = "No existe la comunidad requerida. Cree una comunidad antes de registrar un servicio.";
                return RedirectToAction(nameof(Create));
            }

            if (ModelState.IsValid)
            {
                service.Active = 1;
                service.CreatedAt = DateTime.UtcNow;

                service.CommunityId = 7;
                service.UserHistoryId = 1;

                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }


        public async Task<IActionResult> Edit(long id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Service service)
        {
            ModelState.Remove("Community");
            ModelState.Remove("UserHistory");

            if (ModelState.IsValid)
            {
                
                service.CommunityId = 2;   
                service.UserHistoryId = 1; 


                _context.Services.Update(service);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }

        public async Task<IActionResult> Details(long id)
        {
            var service = await _context.Services
                .Include(s => s.Community)
                .Include(s => s.UserHistory)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        public async Task<IActionResult> Delete(long id)
        {
            var service = await _context.Services
                .Include(s => s.Community)
                .Include(s => s.UserHistory)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}