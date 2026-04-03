using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace QCEServices.Shared;

public static class DependencyInjection
{
    public static void AddValidators(this IServiceCollection services)
    {
        // Auto registration from assembly
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}