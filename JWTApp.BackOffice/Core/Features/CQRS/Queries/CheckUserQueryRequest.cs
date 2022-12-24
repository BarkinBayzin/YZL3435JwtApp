using JWTApp.BackOffice.Core.DTOs;
using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Queries
{
    public class CheckUserQueryRequest : IRequest<CheckUserResponseDTO>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
