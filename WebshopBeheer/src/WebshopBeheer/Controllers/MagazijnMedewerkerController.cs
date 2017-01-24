using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebshopBeheer.Database;

namespace WebshopBeheer.Controllers
{
    public class MagazijnMedewerkerController : Controller
    {
        private WebshopBeheerContext _context;

        public MagazijnMedewerkerController(WebshopBeheerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "Magazijnmedewerker";

            var bestellingen = _context
                .Bestellingen
                .Where(b => b.Status == 0);


            return View(bestellingen);
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
