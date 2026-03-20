using System.Transactions;
using MediatR;
using Microsoft.Extensions.Logging;
using QCEServices.Application.Contracts;

namespace QCEServices.Application.Behaviors;

internal sealed class DbTransactionPipelineBehavior<TRequest, TResponse>(ILogger<ValidationPipelineBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            if (request is not ICommand)
            {
                logger.LogWarning($"{nameof(DbTransactionPipelineBehavior<,>)}: Skipped due to '{typeof(TRequest).Name}' request is not a command.");
                return await next(cancellationToken);
            }
        
            logger.LogInformation($"{nameof(ValidationPipelineBehavior<,>)}: Running...");
            
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var response = await next(cancellationToken);
            transaction.Complete();
            
            logger.LogInformation($"{nameof(ValidationPipelineBehavior<,>)}: Completed without any validation errors.");
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError($"{nameof(DbTransactionPipelineBehavior<,>)}: Error due to {ex.Message}");
            throw;
        }
    }
}