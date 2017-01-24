using System.Collections.Generic;

namespace BestelService
{
    public class BestellingGoedgekeurd
    {
        public List<Artikel> Artikelen { get; set; }
        public Klant Klant { get; set; }
        public int Id { get; set; }
    }
}
