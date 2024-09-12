using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Levres.Data;
using Levres.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace Levres.Controllers
{
    public class AutomobilController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AutomobilController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Automobil
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Automobil.ToListAsync());
        }

        // GET: Automobil
        public async Task<IActionResult> PregledAutaView(
            Model? model,
            double? cijenaOd,
            double? cijenaDo,
            DateOnly? godisteOd,
            DateOnly? godisteDo,
            string Tip)
        {
            var cars = _context.Automobil.AsQueryable();

            if (model!=null )
            {
                cars = cars.Where(c => c.model == model);
            }

            if (cijenaOd.HasValue)
            {
                cars = cars.Where(c => c.cijena >= cijenaOd.Value);
            }

            if (cijenaDo.HasValue)
            {
                cars = cars.Where(c => c.cijena <= cijenaDo.Value);
            }

            if (godisteOd.HasValue)
            {
                cars = cars.Where(c => c.godinaProizvodnje >= godisteOd.Value);
            }

            if (godisteDo.HasValue)
            {
                cars = cars.Where(c => c.godinaProizvodnje <= godisteDo.Value);
            }

            var filteredCars = cars.ToList();

            if (!string.IsNullOrEmpty(Tip))
            {
                if (Tip == "Nov")
                {
                    filteredCars = filteredCars.Where(c => c.GetType().Name == "NoviAutomobil").ToList();
                }
                else if (Tip == "Polovan")
                {
                    filteredCars = filteredCars.Where(c => c.GetType().Name == "PolovniAutomobil").ToList();
                }
            }

            ViewData["Model"] = model;
            ViewData["CijenaOd"] = cijenaOd;
            ViewData["CijenaDo"] = cijenaDo;
            ViewData["GodisteOd"] = godisteOd;
            ViewData["GodisteDo"] = godisteDo;
            ViewData["Tip"] = Tip;

            return View(filteredCars);
        }

    

        // GET: Automobil/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automobil = await _context.Automobil
                .FirstOrDefaultAsync(m => m.id == id);
            if (automobil == null)
            {
                return NotFound();
            }

            return View(automobil);
        }

        // GET: Automobil/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Automobil/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("id,model,godinaProizvodnje,gorivo,transmisija,brojVrata,boja,pogon,felge,emisioniStandard,sjedecaMjesta,masaTezina,vrstaInterijera,svjetla,cijena,slike,motor")] Automobil automobil)
        {
            if (ModelState.IsValid)
            {
                automobil.id = Guid.NewGuid();
                _context.Add(automobil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(automobil);
        }

        // GET: Automobil/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automobil = await _context.Automobil.FindAsync(id);
            if (automobil == null)
            {
                return NotFound();
            }
            return View(automobil);
        }

        // POST: Automobil/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,model,godinaProizvodnje,gorivo,transmisija,brojVrata,boja,pogon,felge,emisioniStandard,sjedecaMjesta,masaTezina,vrstaInterijera,svjetla,cijena,slike,motor")] Automobil automobil)
        {
            if (id != automobil.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(automobil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutomobilExists(automobil.id))
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
            return View(automobil);
        }

        // GET: Automobil/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var automobil = await _context.Automobil
                .FirstOrDefaultAsync(m => m.id == id);
            if (automobil == null)
            {
                return NotFound();
            }

            return View(automobil);
        }

        // POST: Automobil/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var automobil = await _context.Automobil.FindAsync(id);
            if (automobil != null)
            {
                _context.Automobil.Remove(automobil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public bool AutomobilExists(Guid id)
        {
            return _context.Automobil.Any(e => e.id == id);
        }
    }
}
