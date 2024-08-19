using BookingApp.Application.Rules;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Exceptions
{
	public class ExceptionMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context,ex);
			}
		}
		private static Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			int statusCode = GetStatusCode(ex);
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = statusCode;

			if(ex.GetType() == typeof(ValidationException))
				
				return context.Response.WriteAsync(new ExceptionModel 
				{ 
				Errors = ((ValidationException)ex).Errors.Select(x => x.ErrorMessage),
				StatusCode = StatusCodes.Status400BadRequest
				}.ToString());

			List<string> errors = new List<string>()
			{
				ex.Message,
			};
			return context.Response.WriteAsync(new ExceptionModel
			{
				Errors = errors,
				StatusCode = statusCode
			}.ToString());

		}

		private static int GetStatusCode(Exception ex) =>
			ex switch
			{
				BaseException => StatusCodes.Status400BadRequest,
				BadRequestException => StatusCodes.Status400BadRequest,
				NotFoundException => StatusCodes.Status404NotFound,
				ValidationException => StatusCodes.Status422UnprocessableEntity,
				_ => StatusCodes.Status500InternalServerError
			};
	}
	
}
