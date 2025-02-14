﻿using BookWise.Catalog.Application;
using BookWise.Catalog.Filters;
using BookWise.Catalog.Infrastructure;
using BookWise.Catalog.Infrastructure.Extensions;
using BookWise.Catalog.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace BookWise.Catalog;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    [Obsolete]
    public void ConfigureServices(IServiceCollection services)
    {
        var builder = WebApplication.CreateBuilder();

        var appConfigs = builder.AddAppConfigs();

        services.AddRepositories(appConfigs);
        services.AddServices();
        services.AddApplication();
        services.AddAwsServices(builder.Configuration);
        
        services.AddControllers(options =>
            options.Filters.Add(typeof(ExceptionFilter)))
            .AddFluentValidation(x => x.AutomaticValidationEnabled = false)
            .AddCustomJsonOptions()
            .ConfigureApiBehaviorOptions(options =>
                options.SuppressInferBindingSourcesForParameters = true
            );

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "BookWise Customer",
                Version = "v1"
            });
            setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            var scheme = new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                { scheme, Array.Empty<string>() }
            });
        });

        services
            .AddApiVersioning();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI();


        app.UseMiddleware<ValidationMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}