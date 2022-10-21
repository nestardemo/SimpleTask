using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Features.Provinces;
using SimpleWebApi.Features.Сountries;

namespace SimpleWebApi.Controllers
{
    [ApiController]
    [Route("api/provinces")]
    public class ProvincesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvincesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IReadOnlyCollection<GetProvincesResponce>> GetProvinces([FromQuery] GetProvincesQuery query, CancellationToken token)
        {
            return await _mediator.Send(query, token);
        }
    }
}
