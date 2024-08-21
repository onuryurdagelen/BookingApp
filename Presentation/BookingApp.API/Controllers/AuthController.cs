using BookingApp.Application.Commands.AuthC.Register;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterCommandRequest request)
		{
			await _mediator.Send(request);
			return StatusCode(StatusCodes.Status201Created);
		}
		
	}
}
