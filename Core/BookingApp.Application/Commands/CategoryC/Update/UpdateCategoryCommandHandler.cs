using BookingApp.Application.Abstracts.CategoryA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.CategoryC.Update
{
	public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, UpdateCategoryCommandResponse>
	{
		private ICategoryWriteRepository _categoryWriteRepository;
		private ICategoryReadRepository _categoryReadRepository;

		public UpdateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository, ICategoryReadRepository categoryReadRepository)
		{
			_categoryWriteRepository = categoryWriteRepository;
			_categoryReadRepository = categoryReadRepository;
		}

		public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			var currentCategory = await _categoryReadRepository.GetByIdAsync(request.Id);
			currentCategory.Name = request.Name;
			currentCategory.UpdatedDate = DateTime.UtcNow;
			bool result = _categoryWriteRepository.Update(currentCategory);
			if (result)
				await _categoryWriteRepository.SaveAsync();

			var response = new UpdateCategoryCommandResponse();
			response.Success = result;

			return response;
		}
	}
}
