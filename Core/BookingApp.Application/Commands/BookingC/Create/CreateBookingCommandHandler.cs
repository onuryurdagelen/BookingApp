using BookingApp.Application.Abstracts.BookingA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.BookingC.Create
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommandRequest, CreateBookingCommandResponse>
    {
        private readonly IBookingWriteRepository _bookingWriteRepository;

        public CreateBookingCommandHandler(IBookingWriteRepository bookingWriteRepository)
        {
            _bookingWriteRepository = bookingWriteRepository;
        }

        public async Task<CreateBookingCommandResponse> Handle(CreateBookingCommandRequest request, CancellationToken cancellationToken)
        {
           var result = await _bookingWriteRepository.AddAsync(new Domain.Entities.Booking 
            { 
                CreatedDate = DateTime.UtcNow,
                DateTime = request.Date,
                EmailAddress = request.EmailAddress,
                GuestCount = request.GuestCount,
                IsActive = true,
                Phone = request.Phone,
                Time = request.Time
            });
            if (result)
                await _bookingWriteRepository.SaveAsync();

            var response = new CreateBookingCommandResponse();
            response.Success = true; 

            return response;
        }
    }
}
