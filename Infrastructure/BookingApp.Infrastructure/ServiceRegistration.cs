using BookingApp.Infrastructure.Concretes.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
			});
		}
	}
}
