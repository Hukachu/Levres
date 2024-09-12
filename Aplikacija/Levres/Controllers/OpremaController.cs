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

namespace Levres.Controllers
{
    public class OpremaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OpremaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Oprema
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Oprema.ToListAsync());
        }

        [Authorize(Roles = "Administrator,Zaposlenik")]
        public async Task<IActionResult> PregledOpremeView(Model? model, string? pretraga)
        {
            var opremaList = _context.Oprema.AsQueryable();

            if (model!=null)
            {
                opremaList = opremaList.Where(o => o.model == model);
            }

            if (!string.IsNullOrEmpty(pretraga))
            {
                opremaList = opremaList.Where(o => o.nazivDijela.Contains(pretraga));
            }
            ViewData["Model"] = model;
            ViewData["Pretraga"] = pretraga;

            return View(opremaList.ToList());
        }

        // GET: Oprema/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema
                .FirstOrDefaultAsync(m => m.id == id);
            if (oprema == null)
            {
                return NotFound();
            }

            return View(oprema);
        }

        // GET: Oprema/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Oprema/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("id,nazivDijela,model,kolicina,cijena")] Oprema oprema)
        {
            if (ModelState.IsValid)
            {
                oprema.id = Guid.NewGuid();
                _context.Add(oprema);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oprema);
        }

        // GET: Oprema/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema.FindAsync(id);
            if (oprema == null)
            {
                return NotFound();
            }
            return View(oprema);
        }

        // POST: Oprema/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,nazivDijela,model,kolicina,cijena")] Oprema oprema)
        {
            if (id != oprema.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oprema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpremaExists(oprema.id))
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
            return View(oprema);
        }

        // GET: Oprema/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oprema = await _context.Oprema
                .FirstOrDefaultAsync(m => m.id == id);
            if (oprema == null)
            {
                return NotFound();
            }

            return View(oprema);
        }

        // POST: Oprema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var oprema = await _context.Oprema.FindAsync(id);
            if (oprema != null)
            {
                _context.Oprema.Remove(oprema);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpremaExists(Guid id)
        {
            return _context.Oprema.Any(e => e.id == id);
        }
    }
}
