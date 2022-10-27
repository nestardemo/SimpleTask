using SimpleWebApi.Features.Users;
using SimpleWebApi.Tests.Context;

namespace SimpleWebApi.Tests.Fauters.Users
{
    public class CreateUserCommandTests : TestBase
    {
        [Fact(DisplayName = "Create user empty params exception")]
        public async Task CreateUserCommandEmptyParams_Exception()
        {
            var handler = new CreateUserCommandHandler(Context);

            await Assert.ThrowsAsync<ArgumentNullException>(
                  () => handler.Handle(new CreateUserCommand(), CancellationToken.None));
        }

        [Fact(DisplayName = "Create user null login exception")]
        public async Task CreateUserCommandNullLogin_Exception()
        {
            var handler = new CreateUserCommandHandler(Context);


            await Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(
                  () => handler.Handle(new CreateUserCommand
                  {
                      Password = "pass",
                      ProvinceId = Guid.Parse("90819ADA-1131-47F3-B407-5719C8ACFBD4")
                  }, CancellationToken.None));
        }

        [Theory(DisplayName = "Create user null params exception")]
        [InlineData("login2@mail.com", null, "65ED79DA-B2CF-447F-93A1-2B27B5CADDE0")]
        [InlineData("login2@mail.com", "pass123", null)]
        public async Task CreateUserCommandNullParams_Exception(string login, string password, string provinceId)
        {
            var handler = new CreateUserCommandHandler(Context);


            await Assert.ThrowsAsync<ArgumentNullException>(
                  () => handler.Handle(new CreateUserCommand
                  {
                      Login = login,
                      Password = password,
                      ProvinceId = Guid.Parse(provinceId)
                  }, CancellationToken.None));
        }

        [Fact(DisplayName = "Create user successfully")]
        public async Task CreateUserCommand_Success()
        {
            var handler = new CreateUserCommandHandler(Context);

            var result = await handler.Handle(new CreateUserCommand
            {
                Login = "test@mail.com",
                Password = "pass",
                ProvinceId = Guid.Parse("90819ADA-1131-47F3-B407-5719C8ACFBD4")
            }, CancellationToken.None);

            Assert.NotNull(result);
        }

    }
}
