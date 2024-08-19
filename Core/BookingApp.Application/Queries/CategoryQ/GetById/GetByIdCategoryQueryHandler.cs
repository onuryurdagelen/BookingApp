using BookingApp.Application.Abstracts.CategoryA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.CategoryQ.GetById
{
	public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQueryRequest, GetByIdCategoryQueryResponse>
	{
		private readonly ICategoryReadRepository _categoryReadRepository;

		public GetByIdCategoryQueryHandler(ICategoryReadRepository categoryReadRepository)
		{
			_categoryReadRepository = categoryReadRepository;
		}

		public async Task<GetByIdCategoryQueryResponse> Handle(GetByIdCategoryQueryRequest request, CancellationToken cancellationToken)
		{
			var currentCategory = await _categoryReadRepository.GetByIdAsync(request.Id);

			var response = new GetByIdCategoryQueryResponse();
			response.Category = currentCategory;

			return response;
		}
	}
}
