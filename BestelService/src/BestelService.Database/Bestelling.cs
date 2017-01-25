using System;
using System.Collections.Generic;

namespace BestelService.Database
{
    public class Bestelling
    {
        public List<Artikel> Artikelen { get; set; }
        public int Id { get; set; }
        public Klant Klant { get; set; }
        public DateTime BestelDatum { get; set; }
        public Status Status { get; set; }
    }
}
