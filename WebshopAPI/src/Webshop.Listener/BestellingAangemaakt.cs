﻿using System;
using System.Collections.Generic;

namespace Webshop.Listener
{
    public class BestellingAangemaakt
    {
        public int Id { get; set; }
        public List<Artikel> Artikelen { get; set; }
        public Klant Klant { get; set; }
        public DateTime BestelDatum { get; set; }
    }
}