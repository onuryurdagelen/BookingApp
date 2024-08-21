using BookingApp.Application.Bases;
using BookingApp.Application.Rules.TokenR;
using BookingApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Rules.AuthR
{
	public class AuthRules:BaseRules
	{
		public Task UserShouldNotBeExist(User? user)
		{
			if (user is not null) throw new UserAlreadyExistException();

			return Task.CompletedTask;
		}
		public Task UserNotFould(User? user)
		{
			if (user is null) throw new UserNotFoundException();

			return Task.CompletedTask;
		}

		public Task PasswordOrEmailAddressWrong(bool checkPassword)
		{
			if (!checkPassword) throw new PasswordOrEmailAddressWrongException();

			return Task.CompletedTask;
		}
		public Task RefreshTokenShouldNotBeExpired(DateTime expiryTime)
		{
			if (expiryTime <= DateTime.Now)
				throw new SecurityTokenException("The session period has expired.Please log in again.");

			return Task.CompletedTask;
		}
		public Task EmailAddressShouldBeValid(User? user)
		{

			if (user is null) throw new EmailAddressShouldBeValidException();

			return Task.CompletedTask;
		}



	}
}
