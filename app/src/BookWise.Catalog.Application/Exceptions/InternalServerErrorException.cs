namespace BookWise.Catalog.Application.Exceptions;

public sealed class InternalServerErrorException : ApplicationException
{
    public InternalServerErrorException(string? message) : base(message)
    {
    }
}