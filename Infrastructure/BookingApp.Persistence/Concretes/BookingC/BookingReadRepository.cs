using BookingApp.Application.Abstracts.BookingA;
using BookingApp.Domain.Entities;
using BookingApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistence.Concretes.BookingC
{
    public class BookingReadRepository : ReadRepository<Booking>, IBookingReadRepository
    {
        public BookingReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
