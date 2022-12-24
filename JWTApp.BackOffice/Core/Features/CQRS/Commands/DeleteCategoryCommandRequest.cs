using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Commands
{
    public class DeleteCategoryCommandRequest:IRequest
    {
        public int Id { get; set; }

        public DeleteCategoryCommandRequest(int id)
        {
            Id = id;
        }
    }
}
