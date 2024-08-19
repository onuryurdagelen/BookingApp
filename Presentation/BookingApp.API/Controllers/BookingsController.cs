using BookingApp.Application.Queries.Booking.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingsController : ControllerBase
	{
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(string id)
        {
            return Ok();
        }
        [HttpGet("bookings")]   
        public async Task<IActionResult> GetBookings()
        {
            var response = await _mediator.Send(new GetAllBookingQueryRequest());
            return Ok(response);
        }
    }
}
