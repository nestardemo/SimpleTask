using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Domain;

namespace SimpleWebApi.Infrastructure.Database.Extensions
{
    internal static class ModelBuilderExtension
    {
        public static void DataSeeding(this ModelBuilder builder)
        {
            for (int i = 1; i < 4; i++)
            {
                string countryName = "Country " + i;
                Guid countryId = Guid.NewGuid();
                var countryrow = new { CountryId = countryId, CountryName = countryName };
                builder.Entity<Country>().HasData(countryrow);
                for (int j = 1; j < 4; j++)
                {
                    string provinceName = countryName + " Province " + j;
                    Guid provinceId = Guid.NewGuid();
                    var provincerow = new { ProvinceId= provinceId, ProvinceName= provinceName, CountryId = countryId };
                    builder.Entity<Province>().HasData(provincerow);
                }
            }
        }
    }
}
