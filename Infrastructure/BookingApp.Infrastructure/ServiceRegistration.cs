using BookingApp.Application.Abstracts.TokenA;
using BookingApp.Infrastructure.Concretes.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructures(this IServiceCollection services,IConfiguration configuration)
		{
			services.Configure<TokenSettings>(options =>
			{
				options.Audience = configuration.GetSection("JWT:Audience").Value;
				options.Issuer = configuration.GetSection("JWT:Issuer").Value;
				options.SecretKey = configuration.GetSection("JWT:SecretKey").Value;
				options.TokenValidityInMinutes = int.Parse(configuration.GetSection("JWT:TokenValidityInMinutes").Value);
				options.RefreshTokenValidityInDays = int.Parse(configuration.GetSection("JWT:RefreshTokenValidityInDays").Value);
			});
			services.AddScoped<ITokenService,TokenService>();

			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
			{
				opt.SaveToken = true;
				opt.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:SecretKey").Value)),
					ValidateLifetime = true,
					ValidIssuer = configuration.GetSection("JWT:Audience").Value,
					ValidAudience = configuration.GetSection("JWT:Issuer").Value,
					ClockSkew = TimeSpan.Zero
				};
			});
		}
	}
}
