using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var larutaContext = _context.Services.Include(s => s.Community).Include(s => s.UserHistory);
            return View(await larutaContext.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Community)
                .Include(s => s.UserHistory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Id");
            ViewData["UserHistoryId"] = new SelectList(_context.UserFitnessHistories, "Id", "Id");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Active,CreatedAt,DeletedAt,Description,Name,Type,CommunityId,UserHistoryId")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Id", service.CommunityId);
            ViewData["UserHistoryId"] = new SelectList(_context.UserFitnessHistories, "Id", "Id", service.UserHistoryId);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Id", service.CommunityId);
            ViewData["UserHistoryId"] = new SelectList(_context.UserFitnessHistories, "Id", "Id", service.UserHistoryId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Active,CreatedAt,DeletedAt,Description,Name,Type,CommunityId,UserHistoryId")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Id", service.CommunityId);
            ViewData["UserHistoryId"] = new SelectList(_context.UserFitnessHistories, "Id", "Id", service.UserHistoryId);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Community)
                .Include(s => s.UserHistory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(long id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
