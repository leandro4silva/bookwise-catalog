using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace BookWise.Catalog.Application.Handlers.v1.CreateBook;

[ExcludeFromCodeCoverage]
public sealed class CreateBookResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}