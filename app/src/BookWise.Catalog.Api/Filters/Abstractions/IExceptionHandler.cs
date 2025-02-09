using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Filters.Abstractions;

public interface IExceptionHandler
{
    ObjectResult Handle(Exception exception);
}