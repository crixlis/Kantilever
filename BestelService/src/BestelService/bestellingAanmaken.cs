﻿using BestelService.Database;
using System;
using System.Collections.Generic;

namespace BestelService
{
    public class BestellingAanmaken
    {
        public List<Artikel> Artikelen { get; set; }
        public Klant Klant { get; set; }
        public DateTime BestelDatum { get; set; }
    }
}