using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.Login
{
	public class LoginCommandRequest:IRequest<LoginCommandResponse>
	{
        [DefaultValue("yurdagelenonur1@gmail.com")]
        public string EmailAddress { get; set; }
        [DefaultValue("123onur")]
        public string Password { get; set; }

    }
}
