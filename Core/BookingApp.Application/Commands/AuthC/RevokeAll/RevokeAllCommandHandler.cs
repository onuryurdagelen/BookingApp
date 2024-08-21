using BookingApp.Application.Bases;
using BookingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.RevokeAll
{
	public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommandRequest, RevokeAllCommandResponse>
	{
		private readonly UserManager<User> _userManager;
		public RevokeAllCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager) : base(httpContextAccessor)
		{
			_userManager = userManager;
		}

		public async Task<RevokeAllCommandResponse> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
		{
			var users = await _userManager.Users.ToListAsync(cancellationToken);

			users.ForEach(async user =>
			{
				user.RefreshToken = null;
				user.RefreshTokenExpirationTime = null;
				await _userManager.UpdateAsync(user);

			});

			return new RevokeAllCommandResponse();
		}
	}
}
