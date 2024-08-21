using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Bases.Responses.Token
{
	public class TokenResponse
	{
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime AccessTokenExpiryDate { get; set; }
        public DateTime RefreshTokenExpiryDate { get; set; }


    }
}
