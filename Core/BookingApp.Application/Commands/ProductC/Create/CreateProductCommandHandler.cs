using BookingApp.Application.Abstracts.ProductA;
using BookingApp.Application.Rules.ProductR;
using BookingApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IProductReadRepository _productReadRepository;
        private readonly ProductRules _productRules;
		public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ProductRules productRules)
		{
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
			_productRules = productRules;
		}

		public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {

            IList<Product> products = await _productReadRepository.GetAll().ToListAsync();

			await _productRules.ProductTitleMustNotBeSame(request.Name, products);



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
