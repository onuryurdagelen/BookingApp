using BookingApp.Application.Abstracts.BookingA;
using BookingApp.UI.Areas.Dashboard.Models.Booking;
using Microsoft.AspNetCore.SignalR;
using System.Globalization;

namespace BookingApp.UI.Hubs
{
    public class BookingHub:Hub
    {
        private int CurrentBookingCount { get; set; }
        private readonly IBookingReadRepository _bookingReadRepository;

		public BookingHub(IBookingReadRepository bookingReadRepository)
		{
			_bookingReadRepository = bookingReadRepository;
		}

		// Define methods to be called by clients
		public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("OnConnected", "Hello from Booking Hub!");
        }
        public async Task SendBookingList()
        {
            var bookings = _bookingReadRepository.GetAll().OrderByDescending(x => x.CreatedDate).ToList();

            var model = bookings.Select(x => new BookingViewModel
            {
                DateTime = x.DateTime,
                EmailAddress = x.EmailAddress,
                GuestCount = x.GuestCount,
                Id = x.Id.ToString(),
                Phone = x.Phone,
                Status = x.Status,
                Time = x.Time
                
            }).ToList();

            await Clients.All.SendAsync("ReceiveBookingList", model);
        }
       
    }
}
