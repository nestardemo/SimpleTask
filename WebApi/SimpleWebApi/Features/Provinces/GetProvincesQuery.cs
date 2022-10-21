using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Features.Сountries;
using SimpleWebApi.Infrastructure.Database;

namespace SimpleWebApi.Features.Provinces
{
    public class GetProvincesResponce
    {
        public Guid ProvinceId { get; set; }
        public string ProvinceName { get; set; }
    }
    
    public class GetProvincesQuery : IRequest<IReadOnlyCollection<GetProvincesResponce>>
    {
        public Guid CountryId { get; set; }
    }

    public class GetProvincesQueryHandler : IRequestHandler<GetProvincesQuery, IReadOnlyCollection<GetProvincesResponce>>
    {
        private readonly DatabaseContext _databaseContext;

        public GetProvincesQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IReadOnlyCollection<GetProvincesResponce>> Handle(GetProvincesQuery request, CancellationToken cancellationToken)
        {
            var query = _databaseContext.Provinces.AsQueryable();

            if (request.CountryId != null)
            {
                query = query.Where(province => province.CountryId.Equals(request.CountryId));
            }

            return await query.Select(province => new GetProvincesResponce
            {
                ProvinceId = province.ProvinceId,
                ProvinceName = province.ProvinceName
            }).ToListAsync(cancellationToken);
        }
    }
}
