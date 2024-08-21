using BookingApp.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Rules.AuthR
{
	public class EmailAddressShouldBeValidException:BaseException
	{
        public EmailAddressShouldBeValidException() : base("Such an email address is not found!") { }
            
        
    }
}
