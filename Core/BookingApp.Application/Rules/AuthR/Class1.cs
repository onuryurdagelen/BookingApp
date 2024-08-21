using BookingApp.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Rules.AuthR
{
	public class UserAlreadyExistException:BaseException
	{
		public UserAlreadyExistException() : base("Such a user already exist!") { }
	}
}
