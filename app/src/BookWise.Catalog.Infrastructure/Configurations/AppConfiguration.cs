using System.Diagnostics.CodeAnalysis;
using BookWise.Catalog.Infrastructure.LogAudit.Dtos;

namespace BookWise.Catalog.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed class AppConfiguration
{
    private const string EnviromentDev = "dev";
    private const string EnvironmentHom = "hom";

    public AuditoriaConfig? AuditoriaConfig { get; set; }
    
    public MongoDbConfiguration? MongoDb { get; set; }
    
    public string? Environment { get; set; }
    
    public bool IsDevelopment =>
        EnviromentDev.Equals(Environment, StringComparison.OrdinalIgnoreCase);

    public bool IsStaging =>
        EnvironmentHom.Equals(Environment, StringComparison.OrdinalIgnoreCase);
}