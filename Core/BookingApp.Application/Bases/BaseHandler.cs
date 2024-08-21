using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Bases
{
	public class BaseHandler
	{
        private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string userId;
		public BaseHandler(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
			userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
		}

		
    }
}
