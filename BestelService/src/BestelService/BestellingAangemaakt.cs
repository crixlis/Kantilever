using BestelService.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestelService
{
    public class BestellingAangemaakt
    {
        public int Id { get; set; }
        public List<Artikel> Artikelen { get; set; }
        public Klant Klant { get; set; }
        public DateTime BestelDatum { get; set; }
    }
}
