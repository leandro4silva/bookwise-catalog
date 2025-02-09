using BookWise.Catalog.Application.Common.Models;
using BookWise.Catalog.Application.Handlers.v1.CreateBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Controllers.v1;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/books")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<CreateBookResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBook(
        CreateBookCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(CreateBook), response);
    }
}