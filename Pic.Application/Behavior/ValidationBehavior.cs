using FluentValidation;
using MediatR;

namespace Pic.Application;

public class ValidationBehavior <TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Response
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        
        if(_validator is null || validationResult.IsValid)
        {
            return await next();
        }
        
        var errors = validationResult
            .Errors
            .ConvertAll(
                val =>
                new Response(validationResult.Errors)).First();
        
        return (dynamic)errors; 
    }
}
