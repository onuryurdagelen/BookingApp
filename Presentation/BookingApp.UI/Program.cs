using BookingApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MediatR;
using BookingApp.Application;
using BookingApp.Persistence;
using BookingApp.UI.Hubs;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host)  => true)
        .AllowCredentials();
    });
});

// DbContext ve Repository'leri ekleyin.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

// MediatR'ý ekleyin
builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(BookingApp.Application.ServiceRegistration).Assembly));

builder.Services.AddPersistenceServices();

//Register SignalR services
builder.Services.AddSignalR();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map SignalR hubs
app.MapHub<BookingHub>("/bookingHub");
app.MapHub<DashboardHub>("/dashboardHub");

app.Run();
