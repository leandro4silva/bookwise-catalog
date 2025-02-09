using BookWise.Catalog.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Filters.Handlers;

public class InternalServerErrorExceptionHandler : IExceptionHandler
{
    public ObjectResult Handle(Exception exception)
    {
        var details = new ProblemDetails
        {
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Type = "InternalServerError",
            Detail = exception.Message
        };

        return new ObjectResult(details) { StatusCode = details.Status };
    }
}