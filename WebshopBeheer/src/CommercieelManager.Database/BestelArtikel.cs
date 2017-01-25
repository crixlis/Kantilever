using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommercieelManager.Database
{
    public class BestelArtikel
    {
        public int Id { get; set; }
        public Bestelling Bestelling { get; set; }
        public Artikel Artikel { get; set; }
        public int Aantal { get; set; }

    }
}
