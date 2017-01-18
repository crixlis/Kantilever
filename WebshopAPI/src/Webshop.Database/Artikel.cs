﻿using System;
using System.Collections.Generic;

public class Artikel
{
    public int Id { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public decimal Prijs { get; set; }
    public DateTime LeverbaarVanaf { get; set; }
    public DateTime? LeverbaarTot { get; set; }
    public string LeverancierCode { get; set; }
    public string Leverancier { get; set; }

    public List<string> Categorieen = new List<string>();
}