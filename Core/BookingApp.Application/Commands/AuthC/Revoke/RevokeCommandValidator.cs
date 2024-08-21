using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.Revoke
{
	public class RevokeCommandValidator:AbstractValidator<RevokeCommandRequest>
	{
        public RevokeCommandValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress()
                .WithName("Email Address");
        }
    }
}
