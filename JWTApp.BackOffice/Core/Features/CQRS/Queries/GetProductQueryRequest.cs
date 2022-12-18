using JWTApp.BackOffice.Core.DTOs;
using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Queries
{
    public class GetProductQueryRequest:IRequest<ProductListDto>
    {
        public int Id { get; set; }

        public GetProductQueryRequest(int id)
        {
            Id = id;
        }
    }
}
