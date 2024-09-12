using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Levres.Data;
using Levres.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Configuration;

namespace Levres.Controllers
{
    public class ServisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Servis
        [Authorize(Roles = "Administrator,Zaposlenik")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Servis.ToListAsync());
        }

        // GET: Servis/Details/5
        [Authorize(Roles = "Administrator,Zaposlenik,Kupac")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servis = await _context.Servis
                .FirstOrDefaultAsync(m => m.id == id);
            if (servis == null)
            {
                return NotFound();
            }

            return View(servis);
        }

        // GET: Servis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Zaposlenik,Kupac")]
        public async Task<IActionResult> Create([Bind("id,kupacID,model,registracijskeTablice,opis")] Servis servis)
        {
            servis.kupacID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                servis.id = Guid.NewGuid();
                
                _context.Add(servis);
                await _context.SaveChangesAsync();
                return View("Details", servis);
            }
            return View(servis);
        }

        // GET: Servis/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servis = await _context.Servis.FindAsync(id);
            if (servis == null)
            {
                return NotFound();
            }
            return View(servis);
        }

        // POST: Servis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,kupacID,model,registracijskeTablice,opis")] Servis servis)
        {
            if (id != servis.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServisExists(servis.id))
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
            return View(servis);
        }

        // GET: Servis/Delete/5
        [Authorize(Roles = "Administrator,Zaposlenik")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servis = await _context.Servis
                .FirstOrDefaultAsync(m => m.id == id);
            if (servis == null)
            {
                return NotFound();
            }

            return View(servis);
        }

        // POST: Servis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Zaposlenik")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var servis = await _context.Servis.FindAsync(id);
            if (servis != null)
            {
                _context.Servis.Remove(servis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServisExists(Guid id)
        {
            return _context.Servis.Any(e => e.id == id);
        }
    }
}
