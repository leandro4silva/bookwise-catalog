using System.Text.Json.Serialization;
using BookWise.Catalog.Domain.Enums;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace BookWise.Catalog.Application.Models.Requests;

[ExcludeFromDescription]
public sealed class PayloadCreateBookRequest
{
    [JsonProperty("title")]
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
    public long NumberOfPages { get; set; }

    [Newtonsoft.Json.JsonIgnore]
    public DateTime CreatedDate
    {
        get => DateTime.Now;
    }
}