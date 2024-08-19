﻿using BookingApp.Domain.Entities;
using BookingApp.Infrastructure.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Abstracts.BookingA
{
    public interface IBookingReadRepository:IReadRepository<Booking>
    {
    }
}
