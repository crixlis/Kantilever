using System;

namespace Webshop.Database
{
    public class Bestelling
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public DateTime BestelDatum { get; set; }
    }
}