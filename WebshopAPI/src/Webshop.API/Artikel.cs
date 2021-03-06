﻿using System;
using System.Collections.Generic;

namespace Webshop.API
{
    public class Artikel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        public DateTime LeverbaarVanaf { get; set; }
        public DateTime LeverbaarTot { get; set; }
        public string Leverancier { get; set; }
        public List<string> Categorieen { get; set; }
        //TODO: Hoeveelheid aanpassen naar aantal. Ook in frontend!!!
        public int Aantal { get; set; }
        public int Voorraad { get; set; }
        public string ImagePath { get; set; }
    }
}