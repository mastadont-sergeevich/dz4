namespace CountryCatalogAPI.Models
{
    public class Country
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
        public required string Alpha2Code { get; set; }
    }
}