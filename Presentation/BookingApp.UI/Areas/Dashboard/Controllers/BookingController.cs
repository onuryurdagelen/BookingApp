using BookingApp.Application.Commands.BookingC.ChangeStatus;
using BookingApp.Application.Queries.Booking.GetAll;
using BookingApp.Domain.Entities;
using BookingApp.UI.Areas.Dashboard.Models.Booking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookingApp.UI.Areas.Dashboard.Controllers
{
	[Area("Dashboard")]
	public class BookingController : Controller
	{
		private readonly IMediator _mediator;

		public BookingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IActionResult> Index()
		{
			var response = await _mediator.Send(new GetAllBookingQueryRequest());

			var model = response.Bookings.Select(x => new BookingViewModel
			{
				Id = x.Id.ToString(),
				DateTime = x.DateTime,
				EmailAddress = x.EmailAddress,
				GuestCount = x.GuestCount,
				Phone = x.Phone,
				Time = x.Time,
				Status = x.Status
			}).ToList();

			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> ChangeStatus(string id,BookingStatus status) 
		{
			var response = await _mediator.Send(new ChangeStatusBookingCommandRequest { Id = id, Status = status });

			if (response.Success)
				return RedirectToAction(nameof(Index));


			return View();
		
		}
	}
}
