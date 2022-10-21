using FluentValidation;
using MediatR;
using SimpleWebApi.Domain;
using SimpleWebApi.Infrastructure.Database;

namespace SimpleWebApi.Features.Users
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Login { get; set; }
        public Guid ProvinceId { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Login).NotEmpty().EmailAddress();
            RuleFor(c => c.ProvinceId).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly DatabaseContext _databaseContext;

        public CreateUserCommandHandler(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken token)
        {
            var user = new User(request.Login, request.ProvinceId, request.Password);
            await _databaseContext.AddAsync(user, token);
            await _databaseContext.SaveChangesAsync(token);

            return user.UserId;
        }
    }
}
