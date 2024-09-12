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

namespace Levres.Controllers
{
    public class NarudzbaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NarudzbaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Narudzba
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Narudzba.ToListAsync());
        }

        // GET: Narudzba/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .FirstOrDefaultAsync(m => m.id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // GET: Narudzba/Create

        // POST: Narudzba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Zaposlenik,Kupac")]
        public async Task<IActionResult> Create([Bind("id,automobilID,kupacID,datum,Tip")] Narudzba narudzba)
        {
            var automobil = _context.Automobil.Find(narudzba.automobilID);
            if (ModelState.IsValid)
            {
                if(automobil == null)
                {
                    return View("NeuspjesnaNarudzba");
                }
                narudzba.id = Guid.NewGuid();
                narudzba.kupacID = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(narudzba);
                _context.Automobil.Remove(automobil);
                await _context.SaveChangesAsync();
                return View("UspjesnaNarudzba", narudzba);
            }
            return View("NeuspjesnaNarudzba");
        }

        // GET: Narudzba/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba == null)
            {
                return NotFound();
            }
            return View(narudzba);
        }

        // POST: Narudzba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,automobilID,kupacID,datum,Tip")] Narudzba narudzba)
        {
            if (id != narudzba.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.id))
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
            return View(narudzba);
        }

        // GET: Narudzba/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .FirstOrDefaultAsync(m => m.id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // POST: Narudzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba != null)
            {
                _context.Narudzba.Remove(narudzba);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudzbaExists(Guid id)
        {
            return _context.Narudzba.Any(e => e.id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Zaposlenik,Kupac")]
        public async Task<IActionResult> UspjesnaNarudzba(Narudzba narudzba)
        {
            return View(narudzba);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Zaposlenik,Kupac")]
        public async Task<IActionResult> NeuspjesnaNarudzba()
        {
            return View();
        }
    }
    
}
