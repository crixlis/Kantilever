using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuurService.Database
{
    public class Bestelling
    {
        public int Id { get; set; }
        public List<Artikel> Artikelen { get; set; }
        public Klant Klant { get; set; }
    }
}
