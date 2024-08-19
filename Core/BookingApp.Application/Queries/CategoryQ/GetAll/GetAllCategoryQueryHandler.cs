using BookingApp.Application.Abstracts.CategoryA;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.CategoryQ.GetAll
{
	public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, GetAllCategoryQueryResponse>
	{
		private readonly ICategoryReadRepository _categoryReadRepository;

		public GetAllCategoryQueryHandler(ICategoryReadRepository categoryReadRepository)
		{
			_categoryReadRepository = categoryReadRepository;
		}

		public async Task<GetAllCategoryQueryResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
		{
			var response = new GetAllCategoryQueryResponse();
			var categories = await _categoryReadRepository.GetAll().ToListAsync();

			response.Categories = categories;

			return response;
			
		}
	}
}
