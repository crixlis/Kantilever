using System;
using System.Collections.Generic;

namespace WebshopBeheer.Listener
{
    public class Bestelling
    {
        public int Status { get; set; }

        public List<Artikel> Artikelen { get; set; }
        public int Id { get; set; }
        public Klant Klant { get; set; }

        public DateTime BestelDatum { get; set; }
    }
}