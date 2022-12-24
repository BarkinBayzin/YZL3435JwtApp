using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Commands
{
    public class UpdateCategoryCommandRequest:IRequest
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
