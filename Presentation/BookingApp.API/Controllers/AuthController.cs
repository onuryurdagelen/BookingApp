using BookingApp.Application.Commands.AuthC.Login;
using BookingApp.Application.Commands.AuthC.RefreshToken;
using BookingApp.Application.Commands.AuthC.Register;
using BookingApp.Application.Commands.AuthC.Revoke;
using BookingApp.Application.Commands.AuthC.RevokeAll;
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
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return Ok(response);
		}
		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
		{
			var response = await _mediator.Send(request);
			return Ok(response);
		}
		[HttpPost("revoke")]
		public async Task<IActionResult> Revoke(RevokeCommandRequest request)
		{
			await _mediator.Send(request);
			return StatusCode(StatusCodes.Status200OK);
		}
		[HttpPost("revoke-all")]
		public async Task<IActionResult> RevokeAll()
		{
			await _mediator.Send(new RevokeAllCommandRequest());
			return StatusCode(StatusCodes.Status200OK);
		}


	}
}
