using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Database
{
    public class BestelArtikel
    {
        public int Id { get; set; }
        public Bestelling Bestelling { get; set; }
        public Artikel Artikelen { get; set; }
        public int Aantal { get; set; }
    }
}
