using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using SimpleWebApi.Features.Users;
using Serilog;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetUserResponse> GetUser([FromQuery] GetUserQuery query, CancellationToken token)
        {
            return await _mediator.Send(query, token);
        }

        [HttpPost]
        public async Task<Guid> CreateUser([FromBody] CreateUserCommand command, CancellationToken token)
        {
            return await _mediator.Send(command, token);
        }
    }
}
