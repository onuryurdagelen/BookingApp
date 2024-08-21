using BookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BookingApp.Application;
using BookingApp.Persistence;
using BookingApp.Application.Exceptions;
using BookingApp.Infrastructure;
using BookingApp.Application.Abstracts.TokenA;
using BookingApp.Infrastructure.Concretes.Token;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();


builder.Services.AddHttpContextAccessor(); ;
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
// Add services to the container.
builder.Services.AddApplicationServices();
// MediatR'ı ekleyin
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(BookingApp.Application.ServiceRegistration).Assembly));
builder.Services.AddInfrastructures(builder.Configuration);
builder.Services.AddPersistenceServices();
// DbContext ve Repository'leri ekleyin.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
// MediatR'ı ekleyin.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking API", Version = "v1", Description = "Booking API swagger client." });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "'Bearer' yazýp boþluk býraktýktan sonra Token'ý Girebilirsiniz \r\n\r\n Örneðin: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
	});
	c.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}

	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandlerMiddleware(); //Custome Exception Middleware
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
