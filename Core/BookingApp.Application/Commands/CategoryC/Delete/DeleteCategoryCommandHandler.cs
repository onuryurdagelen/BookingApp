using BookingApp.Application.Abstracts.CategoryA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.CategoryC.Delete
{
	public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, DeleteCategoryCommandResponse>
	{

		private readonly ICategoryWriteRepository _categoryWriteRepository;
		private readonly ICategoryReadRepository _categoryReadRepository;

		public DeleteCategoryCommandHandler(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
		{
			_categoryReadRepository = categoryReadRepository;
			_categoryWriteRepository = categoryWriteRepository;
		}

		public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			var currentCategory = await _categoryReadRepository.GetByIdAsync(request.Id);

			bool result = _categoryWriteRepository.Remove(currentCategory);
			if (result)
				await _categoryWriteRepository.SaveAsync();

			var response = new DeleteCategoryCommandResponse();
			response.Success = result;

			return response;
		}
	}
}
