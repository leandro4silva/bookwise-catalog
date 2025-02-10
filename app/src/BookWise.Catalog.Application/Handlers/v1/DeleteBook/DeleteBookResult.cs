using System.Text.Json.Serialization;

namespace BookWise.Catalog.Application.Handlers.v1.DeleteBook;

public sealed class DeleteBookResult
{
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }
}