﻿using Codend.Application.Core.Abstractions.Authentication;
using Codend.Application.Core.Abstractions.Common;
using Codend.Application.Core.Abstractions.Data;
using Codend.Application.Core.Abstractions.Services;
using Codend.Infrastructure.Authentication;
using Codend.Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codend.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTime, MachineDateTime>();

        services.AddScoped<IUserIdentityProvider, UserIdentityProvider>();
        services.AddScoped<IAuthService, FusionAuthService>();
        services.AddScoped<IUserService, FusionAuthService>(
            serviceProvider => serviceProvider.GetRequiredService<FusionAuthService>());

        return services;
    }
}