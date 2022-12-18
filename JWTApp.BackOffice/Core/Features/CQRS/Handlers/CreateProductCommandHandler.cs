using JWTApp.BackOffice.Core.Application.Interfaces;
using JWTApp.BackOffice.Core.Domain;
using JWTApp.BackOffice.Core.Features.CQRS.Commands;
using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public CreateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Product
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
            });

            return Unit.Value;
        }
    }
}
