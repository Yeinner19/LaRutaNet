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
    public class PostsController : Controller
    {
        private readonly LarutaContext _context;

        public PostsController(LarutaContext context)
        {
            _context = context;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var larutaContext = _context.Posts.Include(p => p.Author).Include(p => p.Community).Include(p => p.Service).Include(p => p.UsuarioDestino);
            return View(await larutaContext.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Community)
                .Include(p => p.Service)
                .Include(p => p.UsuarioDestino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Name");
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name");
            ViewData["UsuarioDestinoId"] = new SelectList(_context.Users, "Id", "Username");

            return View();
        }


        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Contenido,Type,CommunityId,ServiceId,UsuarioDestinoId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.AuthorId = 1;
                post.FechaCreacion = DateTime.UtcNow;

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Name", post.CommunityId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", post.ServiceId);
            ViewData["UsuarioDestinoId"] = new SelectList(_context.Users, "Id", "Username", post.UsuarioDestinoId);

            return View(post);
        }


        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
                return NotFound();

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Name", post.CommunityId);
            ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", post.ServiceId);
            ViewData["UsuarioDestinoId"] = new SelectList(_context.Users, "Id", "Username", post.UsuarioDestinoId);

            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            long id,
            [Bind("Id,Contenido,Type,CommunityId,ServiceId,UsuarioDestinoId")] Post post)
        {
            if (id != post.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["CommunityId"] = new SelectList(_context.Communities, "Id", "Name", post.CommunityId);
                ViewData["ServiceId"] = new SelectList(_context.Services, "Id", "Name", post.ServiceId);
                ViewData["UsuarioDestinoId"] = new SelectList(_context.Users, "Id", "Username", post.UsuarioDestinoId);
                return View(post);
            }

            try
            {
                var postDb = await _context.Posts.FindAsync(id);
                if (postDb == null)
                    return NotFound();

                postDb.Contenido = post.Contenido;
                postDb.Type = post.Type;
                postDb.CommunityId = post.CommunityId;
                postDb.ServiceId = post.ServiceId;
                postDb.UsuarioDestinoId = post.UsuarioDestinoId;
                postDb.FechaActualizacion = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Community)
                .Include(p => p.Service)
                .Include(p => p.UsuarioDestino)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(long id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}