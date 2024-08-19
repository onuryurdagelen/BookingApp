using BookingApp.Application.Queries.BookingQ.GetAll;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.Booking.GetAll
{
    public class GetAllBookingQueryRequest:IRequest<GetAllBookingQueryResponse>
    {
    }
}
