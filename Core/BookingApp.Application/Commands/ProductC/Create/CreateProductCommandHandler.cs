using BookingApp.Application.Abstracts.ProductA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.ProductC.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
          bool result = await _productWriteRepository.AddAsync(new Domain.Entities.Product 
           {    Name = request.Name,
               IsActive = true,
               IsDeleted = false,
               Price = request.Price,
               ImageLink = request.ImageLink,
               CategoryId = Guid.Parse(request.CategoryId),
               CreatedDate = DateTime.UtcNow,
               IngredientsText = request.IngredientsText,
               
           });
            if (result)
                await _productWriteRepository.SaveAsync();

            var response = new CreateProductCommandResponse();
            response.Success = true;

            return response;
        }
    }
}
