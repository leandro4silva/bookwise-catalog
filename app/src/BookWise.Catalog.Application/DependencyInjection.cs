using BookWise.Catalog.Application.Common;
using BookWise.Catalog.Application.Handlers.v1.CreateBook;
using BookWise.Catalog.Application.Mappers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookWise.Catalog.Application;

public static class DependencyInjection
{
    [Obsolete("Obsolete")]
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region MediatR

        services.AddMediatR(typeof(CreateBookHandler));
        services.AddAutoMapperProfiles();
        
        services.AddValidatorsFromAssembly(typeof(CreateBookValidator).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        #endregion

        return services;
    }

    private static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(BookProfile));
        return services;
    }
}