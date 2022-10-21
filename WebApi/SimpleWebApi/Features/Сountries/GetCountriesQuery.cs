using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Features.Users;
using SimpleWebApi.Infrastructure.Database;

namespace SimpleWebApi.Features.Сountries
{
    public class GetCountriesResponse 
    {
        public Guid CountryId { get; set; }
        public string CountryName { get; set; }
    }

    public class GetCountriesQuery : IRequest<IReadOnlyCollection<GetCountriesResponse>>
    {
        public string? Keywords { get; set; }
    }

    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, IReadOnlyCollection<GetCountriesResponse>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetCountriesQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IReadOnlyCollection<GetCountriesResponse>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var query = _databaseContext.Countries.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keywords))
            {
                query = query.Where(country => country.CountryName.ToLower().Contains(request.Keywords.ToLower()));
            }

            return await query.Select(country => new GetCountriesResponse
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName
            }).ToListAsync(cancellationToken);
        }
    }
}
