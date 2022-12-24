using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Commands
{
    public class RegisterUserCommandRequest : IRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
