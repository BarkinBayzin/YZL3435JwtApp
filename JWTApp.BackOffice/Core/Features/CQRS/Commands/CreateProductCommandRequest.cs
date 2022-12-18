using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Commands
{
    public class CreateProductCommandRequest:IRequest
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
