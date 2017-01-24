using System;
using System.Collections.Generic;

namespace WebshopBeheer.Database
{
    public class Artikel
    {
        public int Id { get; set; }
        public int ArtikelId { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        public DateTime LeverbaarVanaf { get; set; }
        public DateTime LeverbaarTot { get; set; }
        public string Leverancier { get; set; }
        public List<string> Categorieen = new List<string>();
        public int Voorraad { get; set; }

    }
}