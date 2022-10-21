namespace SimpleWebApi.Domain
{
    public class Country
    {
        public Guid CountryId { get; private set; }
        public string CountryName { get; private set; }

        public Country(string countryName)
        {
            CountryName = countryName;
        }
    }
}
