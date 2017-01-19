namespace WebshopBeheer.Database
{
    public class Bestelling
    {
        public string Beschrijving { get; set; }
        public string Categorieen { get; set; }
        public int Id { get; set; }
        public string Leverancier { get; set; }
        public string LeverancierCode { get; set; }
        public string LeverbaarTot { get; set; }
        public string LeverbaarVanaf { get; set; }
        public string Naam { get; set; }
        public string Prijs { get; set; }
    }
}