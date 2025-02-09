using BookWise.Catalog.Domain.Enums;
using BookWise.Catalog.Domain.ValueObjects;

namespace BookWise.Catalog.Domain.Entities;

public sealed class Book : AggregateRoot
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public Isbn? Isbn { get; set; }

    public string? Publisher { get; set; }

    public GenreBook? GenreBook { get; set; }

    public YearOfPublish? YearOfPublish { get; set; }
    
    public long NumberOfPages { get; set; }
    
    public DateTime CreatedDate { get; set; }

    public Rate? Rate { get; set; }

    public string? BookCover { get; set; }
}