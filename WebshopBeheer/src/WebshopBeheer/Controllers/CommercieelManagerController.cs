using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebshopBeheer.Controllers
{
    public class CommercieelManagerController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Commercieel Manager";

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
