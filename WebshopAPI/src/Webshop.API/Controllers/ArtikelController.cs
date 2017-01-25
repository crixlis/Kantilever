using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using rabbitmq_demo;

using Webshop.Database;
using DatabaseArtikel = Webshop.Database.Artikel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webshop.API.Controllers
{
    [Route("api/[controller]")]
    public class ArtikelController : Controller
    {

        private ISender _sender;
        private IWebshopContext _context;

        public ArtikelController(ISender sender, IWebshopContext context)
        {
            _sender = sender;
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public List<DatabaseArtikel> Get()
        {
           return _context.Artikelen.ToList();          
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public DatabaseArtikel Get(int id)
        {
            return _context.Artikelen.Where(a => a.Id == id).Single();
        }
    }
}
