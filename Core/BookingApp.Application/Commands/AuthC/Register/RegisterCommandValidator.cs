using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.Register
{
	public class RegisterCommandValidator:AbstractValidator<RegisterCommandRequest>
	{
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2)
                .WithName("Full Name");

            RuleFor(x => x.EmailAddress)
                .NotNull()
                .NotEmpty()
                .MaximumLength(60)
                .EmailAddress()
                .MinimumLength(8)
                .WithName("Email Address");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Password");

            RuleFor(x => x.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Confirm Password");

			RuleFor(x => x)
		        .Custom((model, context) =>
		          {
			          if (model.Password != model.ConfirmPassword)
			          {
				          context.AddFailure("ConfirmPassword", "Passwords do not match.");
			          }
		          });

		}
    }
}
