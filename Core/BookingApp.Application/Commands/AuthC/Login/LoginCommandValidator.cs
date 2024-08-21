using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.Login
{
	public class LoginCommandValidator:AbstractValidator<LoginCommandRequest>
	{
        public LoginCommandValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotNull()
                .NotEmpty()
                .WithName("Email Address")
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithName("Password");
        }
    }
}
