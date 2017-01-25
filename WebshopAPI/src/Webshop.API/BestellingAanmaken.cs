using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.API
{
    public class BestellingAanmaken
    {
        public List<Artikel> Artikelen { get; set; }
        public Klant Klant { get; set; }
        public DateTime BestelDatum { get; set; }

    }
}
