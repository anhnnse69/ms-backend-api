using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Common.AnnotationValidationBehavior
{
    public class DataAnnotationValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext(request);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(
                request,
                context,
                results,
                validateAllProperties: true
            );

            if (!isValid)
            {
                var errors = results.Select(r => r.ErrorMessage).ToList();
                throw new ValidationException(string.Join("; ", errors));
            }

            return await next();
        }
    }

}
