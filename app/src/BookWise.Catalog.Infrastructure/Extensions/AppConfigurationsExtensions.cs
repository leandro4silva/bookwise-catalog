using System.Diagnostics.CodeAnalysis;
using BookWise.Catalog.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BookWise.Catalog.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppConfigurationsExtensions
{
    public static AppConfiguration AddAppConfigs(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddConfiguration<AppConfiguration>(configuration);

        using var provider = services.BuildServiceProvider();
        return provider.GetRequiredService<AppConfiguration>();
    }

    private static IServiceCollection AddConfiguration<TPrameterType>(
        this IServiceCollection services, IConfiguration configuration, string? sectionName = null
    ) where TPrameterType : class, new()
    {
        var section = string.IsNullOrEmpty(sectionName) ? configuration : configuration.GetSection(sectionName);

        services.Configure<TPrameterType>(section);
        services.AddTransient(provider =>
            provider.GetRequiredService<IOptionsMonitor<TPrameterType>>().CurrentValue
        );

        return services;
    }
}
