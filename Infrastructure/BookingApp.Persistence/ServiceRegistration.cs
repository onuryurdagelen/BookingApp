﻿using BookingApp.Application.Abstracts.BookingA;
using BookingApp.Application.Abstracts.CategoryA;
using BookingApp.Application.Abstracts.ProductA;
using BookingApp.Domain.Entities;
using BookingApp.Infrastructure.Data;
using BookingApp.Persistence.Concretes.BookingC;
using BookingApp.Persistence.Concretes.CategoryC;
using BookingApp.Persistence.Concretes.ProductC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IBookingReadRepository, BookingReadRepository>();
            services.AddScoped<IBookingWriteRepository, BookingWriteRepository>();

            services.AddScoped<ICategoryReadRepository,CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();

            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository,ProductWriteRepository>();

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 2;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
			})
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
