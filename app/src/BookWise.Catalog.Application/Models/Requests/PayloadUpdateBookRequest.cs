using System.Text.Json.Serialization;
using BookWise.Catalog.Domain.Enums;

namespace BookWise.Catalog.Application.Models.Requests;

public class PayloadUpdateBookRequest
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("isbn")]
    public string? Isbn { get; set; }
    
    [JsonPropertyName("publisher")]
    public string? Publisher { get; set; }
    
    [JsonPropertyName("genre")]
    public GenreBook? GenreBook { get; set; }
    
    [JsonPropertyName("yearOfPublish")]
    public int? YearOfPublish { get; set; }
    
    [JsonPropertyName("numberOfPages")]
    public long? NumberOfPages { get; set; }

    [JsonPropertyName("cover")] 
    public string? BookCover { get; set; }
}