using BookingApp.Application.Abstracts.TokenA;
using BookingApp.Application.Bases;
using BookingApp.Application.Rules.AuthR;
using BookingApp.Application.Rules.TokenR;
using BookingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.RefreshToken
{
	internal class RefreshTokenCommandHandler : BaseHandler,IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
	{
		private readonly UserManager<User> _userManager;
		private readonly ITokenService _tokenService;
		private readonly AuthRules _authRules;
		public RefreshTokenCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, ITokenService tokenService, AuthRules authRules) : base(httpContextAccessor)
		{
			_userManager = userManager;
			_tokenService = tokenService;
			_authRules = authRules;
		}

		public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
		{
			ClaimsPrincipal? principal =_tokenService.GetPrincipalFromExpiredToken(request.AccessToken);

			string email = principal.FindFirstValue(ClaimTypes.Email);

			User user = await _userManager.FindByEmailAsync(email);
			var roles = await _userManager.GetRolesAsync(user);

			//oturum süresi sona ermiş demektir.Artık refresh token ile oturumda kalamayız.Tekrar giriş yapması gerekir.
			await _authRules.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpirationTime!.Value);

			//oturum süresi dolmamış ise,yani refresh token süresi dolmamış ise refresh token üzerinden tekrar access token üreteceğiz.

			var tokenResponse = await _tokenService.CreateTokenAsync(user,roles);

			user.RefreshToken = tokenResponse.RefreshToken;
			await _userManager.UpdateAsync(user);

			var response = new RefreshTokenCommandResponse
			{
				AccessToken = tokenResponse.AccessToken,
				RefreshToken = tokenResponse.RefreshToken
			};


			return response;

		}
	}
}
