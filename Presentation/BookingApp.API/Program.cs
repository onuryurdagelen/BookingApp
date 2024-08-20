using BookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BookingApp.Application;
using BookingApp.Persistence;
using BookingApp.Application.Exceptions;
using BookingApp.Infrastructure;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddApplicationServices();
// MediatR'ý ekleyin
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(BookingApp.Application.ServiceRegistration).Assembly));
builder.Services.AddInfrastructures(builder.Configuration);
builder.Services.AddPersistenceServices();
// DbContext ve Repository'leri ekleyin.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));
// MediatR'ý ekleyin.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandlerMiddleware(); //Custome Exception Middleware
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
