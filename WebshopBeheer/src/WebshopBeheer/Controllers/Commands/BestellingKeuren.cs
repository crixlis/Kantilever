using System.Collections.Generic;
using WebshopBeheer.Database;

namespace WebshopBeheer.Controllers.Commands
{
    public class BestellingKeuren 
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        GoedGekeurd,
        Afgekeurd
    }
}