using FactuurService.Database;
using System.Collections.Generic;

namespace FactuurService
{
    public class FactuurAanmaken
    {
        public List<Artikel> Artikelen { get; set; }
        public int Id { get; set; }
        public Klant Klant { get; set; }
    }
}