using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Application.Handlers.v1.DeleteBook;

public sealed class DeleteBookCommand : IRequest<DeleteBookResult>
{
    [FromRoute] 
    public Guid Id { get; set; }
}