using BookingApp.Application.Abstracts.BookingA;
using BookingApp.Application.Abstracts.CategoryA;
using BookingApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.CategoryC.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse>
    {
        private readonly ICategoryWriteRepository _categoryWriteRepository;

        public CreateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new CreateCategoryCommandResponse();
            response.Success = await _categoryWriteRepository.AddAsync(new Category { Name = request.Name, CreatedDate = DateTime.UtcNow,IsActive = true,IsDeleted = false });

            if (response.Success)
                await _categoryWriteRepository.SaveAsync();

            return response;
        }
    }
}
