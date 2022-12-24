using JWTApp.BackOffice.Core.Application.Enums;
using JWTApp.BackOffice.Core.Application.Interfaces;
using JWTApp.BackOffice.Core.Domain;
using JWTApp.BackOffice.Core.Features.CQRS.Commands;
using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommandRequest>
    {
        private readonly IRepository<AppUser> _repository;

        public RegisterUserCommandHandler(IRepository<AppUser> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RegisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new AppUser
            {
                AppRoleId = (int)RoleType.Member,
                Password = request.Password,
                Username = request.Username
            });

            return Unit.Value;
        }
    }
}
