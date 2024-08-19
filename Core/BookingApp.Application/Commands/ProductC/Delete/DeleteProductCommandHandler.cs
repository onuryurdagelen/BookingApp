using BookingApp.Application.Abstracts.ProductA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.ProductC.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var currentProduct = await _productReadRepository.GetByIdAsync(request.Id);
            var result = _productWriteRepository.Remove(currentProduct);

            if (result)
                await _productWriteRepository.SaveAsync();

            var response = new DeleteProductCommandResponse { Success = result };

            return response;
        }
    }
}
