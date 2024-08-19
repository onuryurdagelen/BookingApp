using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Rules
{
	public class BaseException:ApplicationException
	{
        public BaseException(){}
        public BaseException(string message):base(message){}
    }
}
