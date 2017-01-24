using System;
using System.Collections.Generic;
using FactuurService;

namespace FactuurService.Database
{
    public class Factuur
    {
        public List<Artikel> Artikelen { get; set; }
        public int Id { get; set; }
        public Klant Klant { get; set; }
        public DateTime HuidigeDatum { get; set; }
        public decimal Totaal { get; set; }
    }
}