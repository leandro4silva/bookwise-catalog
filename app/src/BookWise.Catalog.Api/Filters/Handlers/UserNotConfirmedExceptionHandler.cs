using BookWise.Catalog.Filters.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Filters.Handlers;

public class UserNotConfirmedExceptionHandler : IExceptionHandler
{
    public ObjectResult Handle(Exception exception)
    {
        var details = new ProblemDetails
        {
            Title = "Bad Request",
            Status = StatusCodes.Status400BadRequest,
            Type = "BadRequest",
            Detail = exception.Message
        };

        return new ObjectResult(details) { StatusCode = details.Status };
    }
}