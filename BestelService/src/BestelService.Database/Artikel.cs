namespace BestelService.Database
{
    public class Artikel
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }
        public string Leverancier { get; set; }
        public int Voorraad { get; set; }
    }
}
