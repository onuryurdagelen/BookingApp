using BookingApp.Application.Commands.ProductC.Create;
using BookingApp.Application.Queries.Booking.GetAll;
using BookingApp.Application.Queries.ProductQ.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("products")]
		public async Task<IActionResult> GetProducts()
		{
			var response = await _mediator.Send(new GetAllProductQueryRequest());
			return Ok(response);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateProductCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return Ok(response);
		}
	}
}
