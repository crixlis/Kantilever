using CommercieelManager.Database;
using System;
using System.Collections.Generic;

namespace WebshopBeheer.Models.CommercieelManagerModels
{
    public class BestellingViewModel
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
