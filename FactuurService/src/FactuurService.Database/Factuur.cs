using System;
using System.Collections.Generic;

namespace FactuurService.Database
{
    public class Factuur
    {
        public List<Artikel> Artikelen { get; set; }
        public int Id { get; set; }
        public Klant Klant { get; set; }
        public DateTime BetalenVoorDatum { get; set; }
        public decimal Totaal;
    }
}