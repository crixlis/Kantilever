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
        public IActionResult Index()
        {
            ViewData["Title"] = "Magazijnmedewerker";





            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
