using BookingApp.Application.Abstracts.TokenA;
using BookingApp.Application.Bases;
using BookingApp.Application.Bases.Responses.Token;
using BookingApp.Application.Rules.AuthR;
using BookingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.Login
{
	public class LoginCommandHandler: BaseHandler, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
	{
		private readonly AuthRules _authRules;
		private readonly UserManager<User> _userManager;
		private readonly ITokenService _tokenService;
		public LoginCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, AuthRules authRules, ITokenService tokenService) : base(httpContextAccessor)
		{
			_userManager = userManager;
			_authRules = authRules;
			_tokenService = tokenService;
		}
		public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
		{
			User user = await _userManager.FindByEmailAsync(request.EmailAddress);

			await _authRules.UserNotFould(user);

			bool checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);

			//if checkPassword is false,this method throws an exception.
			await _authRules.PasswordOrEmailAddressWrong(checkPassword);

			var rolesByUser = await _userManager.GetRolesAsync(user);

			TokenResponse tokenResponse =  await _tokenService.CreateTokenAsync(user, rolesByUser);

			user.RefreshToken = tokenResponse.RefreshToken;
			user.RefreshTokenExpirationTime = tokenResponse.RefreshTokenExpiryDate;

			await _userManager.UpdateAsync(user);
			await _userManager.UpdateSecurityStampAsync(user);

			LoginCommandResponse response = new LoginCommandResponse
			{
				AccessToken = tokenResponse.AccessToken,
				RefreshToken = tokenResponse.RefreshToken,
				AccessTokenExpiryDate = tokenResponse.AccessTokenExpiryDate,
			};

			return response;
		}
	}
}
