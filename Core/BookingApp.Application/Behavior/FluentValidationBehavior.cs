using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.Behavior
{
	public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;
        public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
			_validators = validators;   
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);
			var failtures = _validators
				.Select(x => x.Validate(context))
				.SelectMany(x => x.Errors)
				.GroupBy(x => x.ErrorMessage)
				.Select(x => x.First())
				.Where(x => x != null)
				.ToList();

			//eğer bir hata var ise validation exception fırlatılır.
			if (failtures.Any()) 
				throw new ValidationException(failtures);

			//hata yok ise devam edilir.
			return next();
		}
	}
}
