using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Features.Users;
using SimpleWebApi.Features.Сountries;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<GetCountriesResponse>> GetCountries([FromQuery] GetCountriesQuery query, CancellationToken token)
        {
            return await _mediator.Send(query, token);
        }
    }

}
