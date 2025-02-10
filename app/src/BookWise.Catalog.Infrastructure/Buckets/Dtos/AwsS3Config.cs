using System.Diagnostics.CodeAnalysis;

namespace BookWise.Catalog.Infrastructure.Buckets.Dtos;

[ExcludeFromCodeCoverage]
public class AwsS3Config
{
    public string? BucketName { get; set; }
}
