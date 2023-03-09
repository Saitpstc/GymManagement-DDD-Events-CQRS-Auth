namespace Shared.Infrastructure;

using Core;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using Serilog;

public class RequestPipeline<TRequest, TResponse>:IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        
        var responseDetailsJson = JsonConvert.SerializeObject(request);
        Log.ForContext("Request",responseDetailsJson).Information($"{nameof(request)} asdfasdfasdf");
        
        var context = new ValidationContext<TRequest>(request);

        var validationFailures = _validators
                                 .Select(v => v.Validate(context))
                                 .SelectMany(result => result.Errors)
                                 .Where(failure => failure != null)
                                 .ToList();

        if (validationFailures.Any())
        {
            var es = validationFailures.Select(x => x.ErrorMessage).ToList();
            throw new RequestValidationException(errorMessages: validationFailures.Select(x => x.ErrorMessage).ToList());
        }

        return await next();
    }
}