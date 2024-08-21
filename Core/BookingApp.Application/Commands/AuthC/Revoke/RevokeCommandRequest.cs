using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Commands.AuthC.Revoke
{
	public class RevokeCommandRequest:IRequest<RevokeCommandResponse>
	{
        public string EmailAddress { get; set; }
    }
}
