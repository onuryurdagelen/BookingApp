using BookingApp.Application.Abstracts.BookingA;
using BookingApp.Application.Queries.BookingQ.GetAll;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.Booking.GetAll
{
    public class GetAllBookingQueryHandler: IRequestHandler<GetAllBookingQueryRequest, GetAllBookingQueryResponse>
    {
        private readonly IBookingReadRepository _bookingReadRepository;

        public GetAllBookingQueryHandler(IBookingReadRepository bookingReadRepository)
        {
            _bookingReadRepository = bookingReadRepository;
        }

        public async Task<GetAllBookingQueryResponse> Handle(GetAllBookingQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new GetAllBookingQueryResponse();

            response.Bookings = await _bookingReadRepository.GetAll().OrderByDescending(x => x.CreatedDate).ToListAsync();

            return response;
        }
    }
}
