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
    public class KonfiguracijaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KonfiguracijaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Konfiguracija
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Konfiguracija.ToListAsync());
        }

        // GET: Konfiguracija/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konfiguracija = await _context.Konfiguracija
                .FirstOrDefaultAsync(m => m.id == id);
            if (konfiguracija == null)
            {
                return NotFound();
            }

            return View(konfiguracija);
        }

        // GET: Konfiguracija/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Konfiguracija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,model,boja,felge,vrsta_interijera,svjetla,motor,cijena")] Konfiguracija konfiguracija)
        {
            if (ModelState.IsValid)
            {
                konfiguracija.id = Guid.NewGuid();
                _context.Add(konfiguracija);
                await _context.SaveChangesAsync();
                return View("Details",konfiguracija);
            }
            return View("NeuspješnaKonfiguracija");
        }

        // GET: Konfiguracija/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konfiguracija = await _context.Konfiguracija.FindAsync(id);
            if (konfiguracija == null)
            {
                return NotFound();
            }
            return View(konfiguracija);
        }

        // POST: Konfiguracija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,model,boja,felge,vrsta_interijera,svjetla,motor,cijena")] Konfiguracija konfiguracija)
        {
            if (id != konfiguracija.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konfiguracija);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KonfiguracijaExists(konfiguracija.id))
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
            return View(konfiguracija);
        }

        // GET: Konfiguracija/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var konfiguracija = await _context.Konfiguracija
                .FirstOrDefaultAsync(m => m.id == id);
            if (konfiguracija == null)
            {
                return NotFound();
            }

            return View(konfiguracija);
        }

        // POST: Konfiguracija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var konfiguracija = await _context.Konfiguracija.FindAsync(id);
            if (konfiguracija != null)
            {
                _context.Konfiguracija.Remove(konfiguracija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KonfiguracijaExists(Guid id)
        {
            return _context.Konfiguracija.Any(e => e.id == id);
        }
        [HttpGet]
        public IActionResult CreateKonfiguracija()
        {
            return View(new KonfiguracijaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKonfiguracija(KonfiguracijaViewModel konfiguracijaViewModel)
        {
            if (ModelState.IsValid)
            {
                Enum.TryParse(konfiguracijaViewModel.boja, out Boja boja);
                Enum.TryParse(konfiguracijaViewModel.model, out Model model);
                Enum.TryParse(konfiguracijaViewModel.felge, out Felge felge);
                Enum.TryParse(konfiguracijaViewModel.vrsta_interijera, out VrstaInterijera vrstaInterijera);
                Enum.TryParse(konfiguracijaViewModel.svjetla, out Svjetla svjetla);
                Enum.TryParse(konfiguracijaViewModel.motor, out Motor motor);
                var konfiguracija = new Konfiguracija
                {
                    id = Guid.NewGuid(),
                    model = model,
                    boja = boja,
                    felge = felge,
                    vrsta_interijera = vrstaInterijera,
                    svjetla = svjetla,
                    motor = motor
                };

                
                konfiguracijaViewModel.cijena = CalculatePrice(konfiguracijaViewModel);

                _context.Add(konfiguracija);
                await _context.SaveChangesAsync();
                return View("Details", konfiguracija);
            }
            return View("NeuspješnaKonfiguracija");
        }

        public IActionResult NeuspješnaKonfiguracija()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetPrice([FromBody] KonfiguracijaViewModel konfiguracijaViewModel)
        {
            if (konfiguracijaViewModel == null)
            {
                return BadRequest("Invalid data.");
            }

            var price = CalculatePrice(konfiguracijaViewModel);
            return Json(new { price });
        }

        private double CalculatePrice(KonfiguracijaViewModel konfiguracijaViewModel) { 

            double price = 0;
            if (!string.IsNullOrEmpty(konfiguracijaViewModel.model))
                switch (konfiguracijaViewModel.model)
                {
                    case "XC60":
                    {
                        price+= 55000;
                        break;
                    }
                    case "TOK12":
                        {
                            price += 35000;
                            break;
                        }
                    case "Belovan":
                        {
                            price += 22700;
                            break;
                        }
                    case "DY6":
                        {
                            price += 48500;
                            break;
                        }
                    case "Tokan":
                        {
                            price += 33500;
                            break;
                        }
                }

            if (!string.IsNullOrEmpty(konfiguracijaViewModel.boja))
                price += konfiguracijaViewModel.boja == "MontenegroCrna" ? 500 : 300;

            if (!string.IsNullOrEmpty(konfiguracijaViewModel.felge))
                price += konfiguracijaViewModel.felge == "Zephyr17" ? 700 : 400;

            if (!string.IsNullOrEmpty(konfiguracijaViewModel.vrsta_interijera))
                price += konfiguracijaViewModel.vrsta_interijera == "Koza" ? 800 : 600;

            if (!string.IsNullOrEmpty(konfiguracijaViewModel.svjetla))
                price += konfiguracijaViewModel.svjetla == "LED" ? 1200 : 900;

            if (!string.IsNullOrEmpty(konfiguracijaViewModel.motor))
                price += konfiguracijaViewModel.motor == "NA08" ? 1500 : 1000;

            return price;
        }
    }
}
