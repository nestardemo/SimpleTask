using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Infrastructure.Database;

namespace SimpleWebApi.Features.Users
{
    public class GetUserResponse 
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public Guid ProvinceId { get; set; }
    }

    public class GetUserQuery : IRequest<GetUserResponse?>
    {
        public string Keywords { get; set; }
    }

    public class GetUseQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse?>
    {
        private readonly DatabaseContext _databaseContext;

        public GetUseQueryHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<GetUserResponse?> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var query = _databaseContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(request.Keywords))
            {
                query = query.Where(user => user.Login.ToLower().Equals(request.Keywords.ToLower()));
            }

            return await query.Select(user => new GetUserResponse
            {
                UserId = user.UserId,
                Login = user.Login,
                ProvinceId = user.ProvinceId
            }).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
