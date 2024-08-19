using BookingApp.Application.Abstracts.BookingA;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.BookingC.ChangeStatus
{
	public class ChangeStatusBookingCommandHandler : IRequestHandler<ChangeStatusBookingCommandRequest, ChangeStatusBookingCommandResponse>
	{
		private readonly IBookingWriteRepository _bookingWriteRepository;

		public ChangeStatusBookingCommandHandler(IBookingWriteRepository bookingWriteRepository)
		{
			_bookingWriteRepository = bookingWriteRepository;
		}

		public async Task<ChangeStatusBookingCommandResponse> Handle(ChangeStatusBookingCommandRequest request, CancellationToken cancellationToken)
		{
			var result = await _bookingWriteRepository.ChangeStatusAsync(request.Id, request.Status);

			if (result)
				await _bookingWriteRepository.SaveAsync();

			var response = new ChangeStatusBookingCommandResponse();
			response.Success = result;

			return response;
		}
	}
}
