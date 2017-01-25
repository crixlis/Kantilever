using System;

namespace CommercieelManager.Database
{
    public class Bestelling
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime BestelDatum { get; set; }
        public Klant Klant { get; set; }
    }
}