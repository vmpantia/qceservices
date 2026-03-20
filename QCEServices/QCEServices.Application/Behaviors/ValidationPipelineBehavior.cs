using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QCEServices.Application.Contracts;

namespace QCEServices.Application.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(IServiceProvider serviceProvider, ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            if (request is not ICommand)
            {
                logger.LogWarning($"{nameof(ValidationPipelineBehavior<,>)}: Skipped due to '{typeof(TRequest).Name}' request is not a command.");
                return await next(cancellationToken);
            }

            logger.LogInformation($"{nameof(ValidationPipelineBehavior<,>)}: Running...");
            
            var validator = serviceProvider.GetService<IValidator<TRequest>>();
            if (validator is not null)
            {
                await validator.ValidateAndThrowAsync(request, cancellationToken);
                logger.LogInformation($"{nameof(ValidationPipelineBehavior<,>)}: Completed without any validation errors.");
            }
            else
            {
                logger.LogWarning($"{nameof(ValidationPipelineBehavior<,>)}: No validator found.");
            }
            
            return await next(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(ValidationPipelineBehavior<,>)}: Error due to {ex.Message}");
            throw;
        }
    }
}