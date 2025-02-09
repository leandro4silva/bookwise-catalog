using BookWise.Catalog.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Filters.Handlers;

public class UnexpectedExceptionHandler : IExceptionHandler
{
    public ObjectResult Handle(Exception exception)
    {
        var details = new ProblemDetails
        {
            Title = "An unexpected error occurred",
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = "Unexpected",
            Detail = exception.Message
        };

        return new ObjectResult(details) { StatusCode = details.Status };
    }
}