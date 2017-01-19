using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebshopBeheer.Controllers
{
    public class SalesManagerController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Sales Manager";

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
