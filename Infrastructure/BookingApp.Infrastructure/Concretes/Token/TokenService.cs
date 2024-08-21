using BookingApp.Application.Abstracts.TokenA;
using BookingApp.Application.Bases.Responses.Token;
using BookingApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
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

        public async Task<TokenResponse> CreateTokenAsync(User user, IList<string> roles)
		{
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Name,user.FullName),
			};
			//kullanıcıya ait rolleri claim listesine ekleriz.
            foreach (var role in roles)
            {
				claims.Add(new Claim(ClaimTypes.Role, role));
            }
			//jwt token security key oluşturulur.
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey));

			var jwtSecurityToken = new JwtSecurityToken(
				issuer:_tokenSettings.Issuer,
				audience:_tokenSettings.Audience,
				expires:DateTime.Now.AddMinutes(_tokenSettings.TokenValidityInMinutes),
				claims:claims,
				signingCredentials:new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256));

			//ilk token oluşturduğumuzda ilgili kullanıcıya ait claim'leri RoleClaims tablosuna claim'leri ekleme işlemi yaparız.
			await _userManager.AddClaimsAsync(user,claims);


			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

			string token = handler.WriteToken(jwtSecurityToken);

			string refreshToken = GenerateRefreshToken();

			//son olarak token döndürülür.
			return new TokenResponse { 
				AccessToken = token, 
				RefreshToken = refreshToken, 
				AccessTokenExpiryDate = DateTime.Now.AddMinutes(_tokenSettings.TokenValidityInMinutes),
				RefreshTokenExpiryDate = DateTime.Now.AddDays(_tokenSettings.RefreshTokenValidityInDays)
			};
        }

		public string GenerateRefreshToken()
		{
			var numberByte = new Byte[32];

			using var randomNumber = RandomNumberGenerator.Create();

			randomNumber.GetBytes(numberByte);

			return Convert.ToBase64String(numberByte);
		}

		public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
		{
			TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.SecretKey)),
				ValidateLifetime = true,
				ValidIssuer = _tokenSettings.Issuer,
				ValidAudience =_tokenSettings.Audience,
				ClockSkew = TimeSpan.Zero
			};

			JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var principal = jwtSecurityTokenHandler.ValidateToken(token,tokenValidationParameters,out SecurityToken securityToken);

			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Token not found!");

			return principal;
		}
	}
}
