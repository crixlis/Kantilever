using System;
using System.Collections.Generic;

public class Bestelling 
{
    public List<Artikel> Artikelen { get; set; }
    public int Id { get; set; }
    public Klant Klant { get; set; }
}