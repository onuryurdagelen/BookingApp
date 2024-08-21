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

namespace BookingApp.Application.Commands.AuthC.Register
{
	public class RegisterCommandHandler :BaseHandler, IRequestHandler<RegisterCommandRequest, RegisterCommandResponse>
	{
		private readonly AuthRules _authRules;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		public RegisterCommandHandler(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, RoleManager<Role> roleManager, AuthRules authRules) : base(httpContextAccessor)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_authRules = authRules;
		}

		public async Task<RegisterCommandResponse> Handle(RegisterCommandRequest request, CancellationToken cancellationToken)
		{
			User user = await _userManager.FindByEmailAsync(request.EmailAddress);

			await _authRules.UserShouldNotBeExist(user);

			user = new User
			{
				FullName = request.FullName,
				Email = request.EmailAddress,
				UserName = request.EmailAddress,
				SecurityStamp = Guid.NewGuid().ToString(),
			};

			IdentityResult result = await _userManager.CreateAsync(user,request.Password);
			if(result.Succeeded)
			{
				//'user' adında rol oluşuturlmamış ise oluşturulur.
				if(!await _roleManager.RoleExistsAsync("user"))
					await _roleManager.CreateAsync(new Role { Id = Guid.NewGuid(),Name = "user",NormalizedName = "USER",ConcurrencyStamp = Guid.NewGuid().ToString() });

				//'user' adında role var ise ilgili kullanıcıya atarız.
				await _userManager.AddToRoleAsync(user, "user");
			}

			RegisterCommandResponse response = new RegisterCommandResponse();

			return response;
		}
	}
}
