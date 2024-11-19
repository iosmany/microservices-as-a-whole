namespace Microservices.Core.Public.DTO
{
    public class CountryDTO : ICountry
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
