using Amazon.S3;
using Amazon.SQS;
using BookWise.Catalog.Domain.Repositories;
using BookWise.Catalog.Infrastructure.Configurations;
using BookWise.Catalog.Infrastructure.LogAudit.Abstractions;
using BookWise.Catalog.Infrastructure.LogAudit.Dtos;
using BookWise.Catalog.Infrastructure.LogAudit.Services;
using BookWise.Catalog.Infrastructure.MessageBus.Abstraction;
using BookWise.Catalog.Infrastructure.MessageBus.Clients;
using BookWise.Catalog.Infrastructure.MessageBus.Event;
using BookWise.Catalog.Infrastructure.Notifications;
using BookWise.Catalog.Infrastructure.Notifications.Abstraction;
using BookWise.Catalog.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace BookWise.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection services, AppConfiguration appConfiguration)
    {
        AddMongoDB(services, appConfiguration);
        
        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        AddS3Bucket(services);
        
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
    
    public static IServiceCollection AddAwsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAWSService<IAmazonSQS>();

        services.AddSingleton<IPublisher, AwsSqsClient>();
        services.AddScoped<IEventProcessor, EventProcessor>();
        services.AddScoped<ILogAuditService, LogAuditService>();
        
        services.Configure<AuditoriaConfig>(configuration.GetSection(nameof(AuditoriaConfig)));
        
        return services;
    }
    
    private static IServiceCollection AddS3Bucket(this IServiceCollection services)
    {
        services.AddScoped<IAmazonS3, AmazonS3Client>();

        return services;
    }
    
    private static IServiceCollection AddMongoDB(this IServiceCollection services, AppConfiguration appConfiguration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            return new MongoClient(appConfiguration.MongoDb?.ConnectionString);
        });

        services.AddTransient<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(appConfiguration.MongoDb?.Database);
        });
        
        return services;
    }
}