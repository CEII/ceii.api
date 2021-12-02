using Ceii.Api.Services.Abstract;
using Ceii.Api.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ceii.Api.Core.Injections;

public static class AddTransientsExtension
{
    /// <summary>
    /// Adds services for Dependency Injection by transient
    /// </summary>
    public static void AddTransientServices(this IServiceCollection services)
    {
        services.AddTransient<IIdentityRepository, IdentityService>();
    }
}