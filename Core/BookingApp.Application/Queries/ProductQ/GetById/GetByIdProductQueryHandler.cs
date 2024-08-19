using BookingApp.Application.Abstracts.CategoryA;
using BookingApp.Application.Abstracts.ProductA;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.ProductQ.GetById
{
	public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly ICategoryReadRepository _categoryReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository, ICategoryReadRepository categoryReadRepository)
        {
            _productReadRepository = productReadRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
		{
			var product = await _productReadRepository.GetByIdAsync(request.Id);
			var categories = await _categoryReadRepository.GetAll().ToListAsync();
			var response = new GetByIdProductQueryResponse { Product = product,Categories = categories };

			return response;
		}
	}
}
