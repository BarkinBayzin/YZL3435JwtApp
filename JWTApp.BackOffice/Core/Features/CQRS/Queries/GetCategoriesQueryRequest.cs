using JWTApp.BackOffice.Core.DTOs;
using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Queries
{
    public class GetCategoriesQueryRequest:IRequest<List<CategoryListDTO>>
    {
    }
}
