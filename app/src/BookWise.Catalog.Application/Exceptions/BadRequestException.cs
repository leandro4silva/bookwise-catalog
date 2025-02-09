namespace BookWise.Catalog.Application.Exceptions;

public sealed class BadRequestException : ApplicationException
{
    public BadRequestException(string? message) : base(message)
    {
    }
}