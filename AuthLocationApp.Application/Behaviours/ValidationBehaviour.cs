using FluentValidation;
using MediatR;
using Serilog;

namespace AuthLocationApp.Application.Behaviours
{
   public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
   {
      private readonly IEnumerable<IValidator<TRequest>> _validators;
      private readonly ILogger _logger;

      public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
      {
         _validators = validators;
         _logger = Log.ForContext<ValidationBehaviour<TRequest, TResponse>>();
      }

      public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
      {
         if (!_validators.Any())
         {
            return await next();
         }

         var context = new ValidationContext<TRequest>(request);
         var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
         var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

         if (failures.Count > 0)
         {
            var errors = string.Join("; ", failures.Select(f => f.ErrorMessage));
            _logger.Warning("Validation failed: {Errors}", errors);
            throw new ValidationException(failures);
         }

         return await next();
      }
   }
}
