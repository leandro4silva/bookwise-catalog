using BookWise.Catalog.Application.Common.Models;
using BookWise.Catalog.Application.Handlers.v1.CreateBook;
using BookWise.Catalog.Application.Handlers.v1.DeleteBook;
using BookWise.Catalog.Application.Handlers.v1.GetBookById;
using BookWise.Catalog.Application.Handlers.v1.UpdateBook;
using BookWise.Catalog.Application.Handlers.v1.UpdateBookCover;
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

        return CreatedAtAction(nameof(GetBookById), response);
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(BaseResponse<GetBookByIdResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetBookById(
        GetBookByIdCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(BaseResponse<DeleteBookResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteBook(
        DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(BaseResponse<UpdateBookResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateBook(
        UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
    
    [HttpPatch("{id}/cover")]
    [ProducesResponseType(typeof(BaseResponse<UpdateBookCoverResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCoverBook(
        UpdateBookCoverCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}