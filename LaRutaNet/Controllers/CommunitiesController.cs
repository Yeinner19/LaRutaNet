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
    public class CommunitiesController : Controller
    {
        private readonly LarutaContext _context;

        public CommunitiesController(LarutaContext context)
        {
            _context = context;
        }

        // GET: Communities
        public async Task<IActionResult> Index()
        {
            var larutaContext = _context.Communities.Include(c => c.Creator);
            return View(await larutaContext.ToListAsync());
        }

        // GET: Communities/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities
                .Include(c => c.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // GET: Communities/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Communities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Category,Description,Name")] Community community)
        {
            if (ModelState.IsValid)
            {
                community.Active = 1;
                community.DateOfCreation = DateTime.UtcNow;
                community.CreatorId = 1;
                community.AllowsPosts = 1;

                _context.Add(community);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(community);
        }


        // GET: Communities/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }
            return View(community);
        }


        // POST: Communities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Category,Description,Name")] Community data)
        {
            if (!ModelState.IsValid)
                return View(data);

            var community = await _context.Communities.FindAsync(id);

            if (community == null)
                return NotFound();

            community.Name = data.Name;
            community.Description = data.Description;
            community.Category = data.Category;
            community.Active = 1;
            community.AllowsPosts = 1;
            community.PostRules = data.PostRules;
            community.BannerPublicId = data.BannerPublicId;
            community.BannerUrl = data.BannerUrl;
            community.LogoPublicId = data.LogoPublicId;
            community.LogoUrl = data.LogoUrl;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Communities/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities
                .Include(c => c.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        // POST: Communities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var community = await _context.Communities.FindAsync(id);
            if (community != null)
            {
                _context.Communities.Remove(community);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunityExists(long id)
        {
            return _context.Communities.Any(e => e.Id == id);
        }
    }
}
