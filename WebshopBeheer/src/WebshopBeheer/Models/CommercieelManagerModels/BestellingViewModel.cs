using CommercieelManager.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebshopBeheer.Models.CommercieelManagerModels
{
    public class BestellingenViewModel
    {
        public IEnumerable<Bestelling> Bestellingen { get; set; }
    }

    public class Bestelling
    {
        public int Id { get; set; }
        public Klant Klant { get; set; }
        public IEnumerable<Artikel> Artikelen { get; set; }
        public DateTime BestelDatum { get; set; }
    }

    public class Artikel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public decimal Prijs { get; set; }
        public int Aantal { get; set; }
    }
}
