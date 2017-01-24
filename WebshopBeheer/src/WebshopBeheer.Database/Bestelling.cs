using System.Collections.Generic;

namespace WebshopBeheer.Database
{
    public class Bestelling
    {
        public int Status;

        public List<Artikel> Artikelen { get; set; }
        public int Id { get; set; }
        public Klant Klant { get; set; }
    }
}