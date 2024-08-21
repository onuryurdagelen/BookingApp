using BookingApp.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Rules.TokenR
{
	public class SecurityTokenException:BaseException
	{
		public SecurityTokenException(string message):base(message) { }
	}
}
