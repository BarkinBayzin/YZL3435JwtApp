using JWTApp.BackOffice.Core.DTOs;
using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Queries
{
    public class GetCategoryQueryRequest:IRequest<CategoryListDTO>
    {
        public int Id { get; set; }

        public GetCategoryQueryRequest(int id)
        {
            Id = id;
        }
    }
}
