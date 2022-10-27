using SimpleWebApi.Features.Users;
using SimpleWebApi.Features.Сountries;
using SimpleWebApi.Tests.Context;

namespace SimpleWebApi.Tests.Fauters.Countries
{
    public class GetCountriesQueryTests : TestBase
    {
        [Fact(DisplayName = "Get country list success")]
        public async Task GetCountriesQuery_Success()
        {

            var handler = new GetCountriesQueryHandler(Context);

            var result = await handler.Handle(new GetCountriesQuery(), CancellationToken.None);

            Assert.Equal(4, result.Count());
        }
    }
}
