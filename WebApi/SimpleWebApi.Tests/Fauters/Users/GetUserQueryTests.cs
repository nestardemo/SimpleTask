using SimpleWebApi.Features.Users;
using SimpleWebApi.Tests.Context;

namespace SimpleWebApi.Tests.Fauters.Users
{
    public class GetUserQueryTests : TestBase
    {

        [Fact(DisplayName = "Find user by login success")]
        public async Task GetUserQueryHandlerByLogin_Success()
        {

            var handler = new GetUseQueryHandler(Context);

            var result = await handler.Handle(
                new GetUserQuery { Keywords = "login@mail.com" }, CancellationToken.None);

            Assert.Equal("login@mail.com", result?.Login);
        }

        [Fact(DisplayName = "Find user by fake login success")]
        public async Task GetUserQueryHandlerByFakeLogin_Success()
        {

            var handler = new GetUseQueryHandler(Context);

            var result = await handler.Handle(
                new GetUserQuery { Keywords = "fake@mail.com" }, CancellationToken.None);

            Assert.Null(result);
        }

        [Fact(DisplayName = "Find user without keyword exception")]
        public async Task GetUserQueryHandlerByLoginNoKeyword_Exception()
        {

            var handler = new GetUseQueryHandler(Context);

            await Assert.ThrowsAsync<System.Exception>(
                    () => handler.Handle(new GetUserQuery(), CancellationToken.None));
        }

    }
}
