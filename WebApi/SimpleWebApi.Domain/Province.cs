namespace SimpleWebApi.Domain
{
    public class Province
    {
        public Guid ProvinceId { get; private set; }
        public string ProvinceName { get; private set; }
        public Guid CountryId { get; private set; }
        public Country Country { get; private set; }

        public Province(string provinceName, Guid countryId)
        {
            ProvinceName = provinceName;
            CountryId = countryId;
        }
    }
}
