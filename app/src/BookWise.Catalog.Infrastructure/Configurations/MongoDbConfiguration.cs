using System.Diagnostics.CodeAnalysis;

namespace BookWise.Catalog.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed class MongoDbConfiguration
{
    public string? Database { get; set; }

    public string? ConnectionString { get; set; }
}