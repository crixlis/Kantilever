using MagazijnMedewerker.Database;
using System;
using System.Collections.Generic;

namespace WebshopBeheer.Models.MagazijnMedewerkerModels
{
    public class BestellingViewModel
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime BestelDatum { get; set; }
        public Klant klant { get; set; }

        public IEnumerable<Artikel> Artikelen { get; set; }
    }

    public class Artikel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<string> Categorieen { get; set; }
        public int Aantal { get; set; }
    }
}
