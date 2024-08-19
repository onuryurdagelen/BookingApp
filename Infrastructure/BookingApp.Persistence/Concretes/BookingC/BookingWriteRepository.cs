using BookingApp.Application.Abstracts.BookingA;
using BookingApp.Domain.Entities;
using BookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistence.Concretes.BookingC
{
    public class BookingWriteRepository : WriteRepository<Booking>, IBookingWriteRepository
    {
        public BookingWriteRepository(AppDbContext context) : base(context)
        {
        }

		public async Task<bool> ChangeStatusAsync(string id, BookingStatus status)
		{
			var currentUser = await _context.Bookings.SingleOrDefaultAsync(x => x.Id == Guid.Parse(id));
			currentUser!.Status = status;
			EntityEntry entityEntry = Table.Update(currentUser);
			return entityEntry.State == EntityState.Modified;
		}
	}
}
