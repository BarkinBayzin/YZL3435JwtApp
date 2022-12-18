using JWTApp.BackOffice.Core.Application.Interfaces;
using JWTApp.BackOffice.Core.Domain;
using JWTApp.BackOffice.Core.Features.CQRS.Commands;
using MediatR;

namespace JWTApp.BackOffice.Core.Features.CQRS.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IRepository<Product> _repository;

        public UpdateProductCommandHandler(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var updatedProduct = await _repository.GetByIdAsync(request.Id);
            if (updatedProduct != null)
            {
                updatedProduct.Name = request.Name;
                updatedProduct.Price = request.Price;
                updatedProduct.Stock= request.Stock;
                updatedProduct.CategoryId= request.CategoryId;
                await _repository.UpdateAsync(updatedProduct);
            }

            return Unit.Value;
        }
    }
}
