using System.Collections.Generic;

namespace Webshop.API
{
    public class Bestelling
    {
        public List<BestellingArtikel> Artikelen { get; set; }
        public int Id { get; set; }
        public Klant Klant { get; set; }
    }
}