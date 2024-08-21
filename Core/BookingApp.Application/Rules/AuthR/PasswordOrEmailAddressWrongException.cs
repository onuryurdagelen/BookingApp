using BookingApp.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Rules.AuthR
{
	public class PasswordOrEmailAddressWrongException:BaseException
	{
        public PasswordOrEmailAddressWrongException() : base("Password or email address is wrong!") { }
        
    }
}
