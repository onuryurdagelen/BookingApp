using BookingApp.Application.Abstracts.ProductA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.ProductC.Update
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
	{
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IProductReadRepository _productReadRepository;

		public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
		{
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
		}

		public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
		{
			var currentProduct = await _productReadRepository.GetByIdAsync(request.Id);

			currentProduct.IngredientsText = request.IngredientsText;
			currentProduct.UpdatedDate = DateTime.UtcNow;
			currentProduct.ImageLink = request.ImageLink;
			currentProduct.Price = request.Price;
			currentProduct.Name = request.Name;
			currentProduct.CategoryId = Guid.Parse(request.CategoryId);

			bool result = _productWriteRepository.Update(currentProduct);

			if (result)
				await _productWriteRepository.SaveAsync();

			var response = new UpdateProductCommandResponse() { Success = true};

			return response;
		}
	}
}
