using BookingApp.Application.Abstracts.TokenA;
using BookingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Infrastructure.Concretes.Token
{
	public class TokenService : ITokenService
	{
		private readonly UserManager<User> _userManager;
		private readonly TokenSettings _tokenSettings;
        public TokenService(IOptions<TokenSettings> options,UserManager<User> userManager)
        {
            _userManager = userManager;
			_tokenSettings = options.Value;
        }

        public async Task<JwtSecurityToken> CreateToken(User user, IList<string> roles)
		{
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Name,user.FullName),
				new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
			};
			//kullanıcıya ait rolleri claim listesine ekleriz.
            foreach (var role in roles)
            {
				claims.Add(new Claim(ClaimTypes.Role, role));
            }
			//jwt token security key oluşturulur.
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));

			var token = new JwtSecurityToken(
				issuer:_tokenSettings.Issuer,
				audience:_tokenSettings.Audience,
				expires:DateTime.Now.AddMinutes(_tokenSettings.TokenValidityInMinutes),
				claims:claims,
				signingCredentials:new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256));

			//ilk token oluşturduğumuzda ilgili kullanıcıya ait claim'leri RoleClaims tablosuna claim'leri ekleme işlemi yaparız.
			await _userManager.AddClaimsAsync(user,claims);

			//son olarak token döndürülür.
			return token;
        }

		public string GenerateRefreshToken()
		{
			throw new NotImplementedException();
		}

		public ClaimsPrincipal? GetPrincipalFromExpiredToken()
		{
			throw new NotImplementedException();
		}
	}
}
