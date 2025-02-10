using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Application.Handlers.v1.GetBookById;

public sealed class GetBookByIdCommand : IRequest<GetBookByIdResult>
{
    [FromRoute] 
    public Guid Id { get; set; }
}