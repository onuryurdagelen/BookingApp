using BookingApp.Application.Bases;
using BookingApp.Application.Rules.AuthR;
using BookingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.Revoke
{
	public class RevokeCommandHandler : BaseHandler,IRequestHandler<RevokeCommandRequest, RevokeCommandResponse>
	{
		private readonly AuthRules _authRules;
		private readonly UserManager<User> _userManager;
		public RevokeCommandHandler(IHttpContextAccessor httpContextAccessor, AuthRules authRules, UserManager<User> userManager) : base(httpContextAccessor)
		{
			_authRules = authRules;
			_userManager = userManager;
		}

		public async Task<RevokeCommandResponse> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
		{

			User user = await _userManager.FindByEmailAsync(request.EmailAddress);
			await _authRules.EmailAddressShouldBeValid(user);


			user.RefreshToken = null;
			user.RefreshTokenExpirationTime = null;
			await _userManager.UpdateAsync(user);

			return new RevokeCommandResponse();
		}
	}
}
