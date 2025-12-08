using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaRutaNet.Models;

namespace LaRutaNet.Controllers
{
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

            if (ModelState.IsValid)
            {
                service.Active = 1;
                service.CreatedAt = DateTime.UtcNow;

                // 👉 Valores obligatorios para NO reventar por FK
                service.CommunityId = 2;    // FIJO
                service.UserHistoryId = 1;  // FIJO

                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(service);
        }


        // =============================
        // EDITAR GET
        // =============================
        public async Task<IActionResult> Edit(long id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null)
                return NotFound();

            return View(service);
        }

        // =============================
        // EDITAR POST (CORREGIDO)
        // =============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Service service)
        {
            ModelState.Remove("Community");
            ModelState.Remove("UserHistory");

            if (ModelState.IsValid)
            {
                // 🔥 IMPORTANTE:
                // Volver a asignar FK obligatorias
                service.CommunityId = 2;   // MISMO VALOR QUE CREATE
                service.UserHistoryId = 1; // MISMO VALOR QUE CREATE

                // No tienes UpdatedAt → no lo usamos
                // service.UpdatedAt = DateTime.UtcNow;

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
