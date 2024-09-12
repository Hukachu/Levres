using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Levres.Data;
using Levres.Models;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Levres.Controllers
{
    public class NoviAutomobilController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _uploadFolderPath;

        public NoviAutomobilController(ApplicationDbContext context)
        {
            _context = context;
            _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");


            if (!Directory.Exists(_uploadFolderPath))
            {
                Directory.CreateDirectory(_uploadFolderPath);
            }
        }


        // GET: NoviAutomobil
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.NoviAutomobil.ToListAsync());
        }

        // GET: NoviAutomobil/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noviAutomobil = await _context.NoviAutomobil
                .FirstOrDefaultAsync(m => m.id == id);
            if (noviAutomobil == null)
            {
                return NotFound();
            }

            return View(noviAutomobil);
        }

        // GET: NoviAutomobil/Create
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: NoviAutomobil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("garancija,id,model,godinaProizvodnje,gorivo,transmisija,brojVrata,boja,pogon,felge,emisioniStandard,sjedecaMjesta,masaTezina,vrstaInterijera,svjetla,cijena,slike,motor")] NoviAutomobil noviAutomobil)
        {
            noviAutomobil.id = Guid.NewGuid();
            IFormFile slika = Request.Form.Files[0];
            if (slika != null && slika.Length > 0)
            {
                string uniqueFileName = noviAutomobil.id.ToString()+"_"+slika.FileName;
                string filePath = Path.Combine(_uploadFolderPath, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await slika.CopyToAsync(stream);
                }
                noviAutomobil.slike = uniqueFileName;
                _context.Add(noviAutomobil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            else
            {
                ModelState.AddModelError("slika", "The image file is required.");
            }

            return View(noviAutomobil);
        }

        // GET: NoviAutomobil/Edit/5
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noviAutomobil = await _context.NoviAutomobil.FindAsync(id);
            if (noviAutomobil == null)
            {
                return NotFound();
            }
            return View(noviAutomobil);
        }

        // POST: NoviAutomobil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("garancija,id,model,godinaProizvodnje,gorivo,transmisija,brojVrata,boja,pogon,felge,emisioniStandard,sjedecaMjesta,masaTezina,vrstaInterijera,svjetla,cijena,slika,motor")] NoviAutomobil noviAutomobil)
        {
            if (id != noviAutomobil.id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noviAutomobil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoviAutomobilExists(noviAutomobil.id))
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
            return View(noviAutomobil);
        }

        // GET: NoviAutomobil/Delete/
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noviAutomobil = await _context.NoviAutomobil
                .FirstOrDefaultAsync(m => m.id == id);
            if (noviAutomobil == null)
            {
                return NotFound();
            }

            return View(noviAutomobil);
        }

        // POST: NoviAutomobil/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var noviAutomobil = await _context.NoviAutomobil.FindAsync(id);
            if (noviAutomobil != null)
            {
                _context.NoviAutomobil.Remove(noviAutomobil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoviAutomobilExists(Guid id)
        {
            return _context.NoviAutomobil.Any(e => e.id == id);
        }
    }
}
