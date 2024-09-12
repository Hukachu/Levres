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
    public class PolovniAutomobilController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadFolderPath;

        public PolovniAutomobilController(ApplicationDbContext context)
        {
            _context = context;
            _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");


            if (!Directory.Exists(_uploadFolderPath))
            {
                Directory.CreateDirectory(_uploadFolderPath);
            }
        }

        // GET: PolovniAutomobil
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.PolovniAutomobil.ToListAsync());
        }

        // GET: PolovniAutomobil/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polovniAutomobil = await _context.PolovniAutomobil
                .FirstOrDefaultAsync(m => m.id == id);
            if (polovniAutomobil == null)
            {
                return NotFound();
            }

            return View(polovniAutomobil);
        }

        // GET: PolovniAutomobil/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: PolovniAutomobil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("kilometraza,stete,brojVlasnika,id,model,godinaProizvodnje,gorivo,transmisija,brojVrata,boja,pogon,felge,emisioniStandard,sjedecaMjesta,masaTezina,vrstaInterijera,svjetla,cijena,slike,motor")] PolovniAutomobil polovniAutomobil)
        {
            polovniAutomobil.id = Guid.NewGuid();
            IFormFile slika = Request.Form.Files[0];
            if (slika != null && slika.Length > 0)
            {
                string uniqueFileName = polovniAutomobil.id.ToString() + "_" + slika.FileName;
                string filePath = Path.Combine(_uploadFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await slika.CopyToAsync(stream);
                }

                polovniAutomobil.slike = uniqueFileName;
                _context.Add(polovniAutomobil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            else
            {
                ModelState.AddModelError("slika", "The image file is required.");
            }

            return View(polovniAutomobil);
        }

        // GET: PolovniAutomobil/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polovniAutomobil = await _context.PolovniAutomobil.FindAsync(id);
            if (polovniAutomobil == null)
            {
                return NotFound();
            }
            return View(polovniAutomobil);
        }

        // POST: PolovniAutomobil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("kilometraza,stete,brojVlasnika,id,model,godinaProizvodnje,gorivo,transmisija,brojVrata,boja,pogon,felge,emisioniStandard,sjedecaMjesta,masaTezina,vrstaInterijera,svjetla,cijena,slike,motor")] PolovniAutomobil polovniAutomobil)
        {
            if (id != polovniAutomobil.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(polovniAutomobil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolovniAutomobilExists(polovniAutomobil.id))
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
            return View(polovniAutomobil);
        }

        // GET: PolovniAutomobil/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polovniAutomobil = await _context.PolovniAutomobil
                .FirstOrDefaultAsync(m => m.id == id);
            if (polovniAutomobil == null)
            {
                return NotFound();
            }

            return View(polovniAutomobil);
        }

        // POST: PolovniAutomobil/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var polovniAutomobil = await _context.PolovniAutomobil.FindAsync(id);
            if (polovniAutomobil != null)
            {
                _context.PolovniAutomobil.Remove(polovniAutomobil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolovniAutomobilExists(Guid id)
        {
            return _context.PolovniAutomobil.Any(e => e.id == id);
        }
    }
}
