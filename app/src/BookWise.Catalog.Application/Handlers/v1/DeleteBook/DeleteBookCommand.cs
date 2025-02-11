using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Application.Handlers.v1.DeleteBook;

public sealed class DeleteBookCommand : IRequest<DeleteBookResult>
{
    [FromRoute(Name = "id")] 
    public Guid Id { get; set; }
}