 using Levres.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Levres.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles="Administrator,Zaposlenik")]
        public async Task<IActionResult> ProcjenaVrijednostiView()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Zaposlenik")]
        public async Task<IActionResult> ProcjenaVrijednostiView([Bind("kilometraza,stete,brojVlasnika,model,godinaProizvodnje,cijena")] PolovniAutomobil car)
        {
            double approximatedPrice = Calculate(car);
            ViewBag.ApproximatedPrice = approximatedPrice;
            return View(car);
        }

        public double Calculate(PolovniAutomobil car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car));
            }
            double basePrice=0;
            if (car.model == Model.Tokan) basePrice = 30000;
            else if (car.model == Model.Belovan) basePrice = 25000;
            else if (car.model == Model.XC60) basePrice = 45000;
            else if (car.model == Model.DY6) basePrice = 40000;
            else if (car.model == Model.TOK12) basePrice = 50000;

            if (car.kilometraza > 150000)
                basePrice -= basePrice * 0.2;
            else if (car.kilometraza > 100000)
                basePrice -= basePrice * 0.1;
            else if (car.kilometraza > 50000)
                basePrice -= basePrice * 0.05;

            if (!string.IsNullOrEmpty(car.stete))
                basePrice -= basePrice * 0.1;

            if (car.brojVlasnika > 1)
                basePrice -= basePrice * 0.05 * (car.brojVlasnika - 1);

            basePrice = basePrice * Math.Pow((1 - 0.05), (DateTime.Now.Year-car.godinaProizvodnje.Year));

            return Math.Round(basePrice,2);
        }
    }
}
