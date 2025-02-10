namespace BookWise.Catalog.Infrastructure.Buckets.Abstractions;

public interface IBucketS3Service
{
    Task<string> UploadFileAsync(string filePath, string key, CancellationToken cancellationToken);
}
