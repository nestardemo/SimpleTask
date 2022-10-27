using SimpleWebApi.Features.Provinces;
using SimpleWebApi.Features.Сountries;
using SimpleWebApi.Tests.Context;

namespace SimpleWebApi.Tests.Fauters.Province
{
    public class GetProvincesQueryTests : TestBase
    {
        [Fact(DisplayName = "Get province list by country id success")]
        public async Task GetProvincesQuery_Success()
        {

            var handler = new GetProvincesQueryHandler(Context);
            var country = Context.Countries.Where(a=>a.CountryName=="Country 1").FirstOrDefault();

            var result = await handler.Handle(new GetProvincesQuery
            {
                CountryId = country.CountryId
            }, CancellationToken.None);

            Assert.Equal(3, result.Count());
        }

    }
}