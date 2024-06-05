
using FluentValidation;
using MediatR;
using WebChat.Domain.Shared;
namespace WebChat.Application.StartServices.Behaviors
{

public sealed class ValidationPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : BaseResponse
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            if (errorsDictionary.Any())
            {
                return  (TResponse)CreateErrorResponse(errorsDictionary).Result;
            }

            return await next();
        }

        private async Task<BaseResponse> CreateErrorResponse(Dictionary<string, string[]> errors)
        {
            return await BaseResponse.BadRequestResponsAsync(ConvertErrorsToString(errors));
        }

        private string ConvertErrorsToString(Dictionary<string, string[]> errors)
        {
            return string.Join(";", errors.Select(kvp => $"{ kvp.Key }: { string.Join(",", kvp.Value) }"));
        }
    }

   
}

