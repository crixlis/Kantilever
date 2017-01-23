using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactuurService
{
    public class Factuur : FactuurAanmaken
    {
        public DateTime BetalenVoorDatum { get; set; }
        public decimal Totaal;

        public decimal BerekenTotaal()
        {
            decimal totaal = 0;
            foreach (Artikel item in Artikelen)
            {
                totaal += item.Prijs;
            }

            return totaal;
        }
    }
}
