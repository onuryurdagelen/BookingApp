﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Queries.BookingQ.GetAll
{
    public class GetAllBookingQueryResponse
    {
        public List<BookingApp.Domain.Entities.Booking> Bookings { get; set; }
    }
}
