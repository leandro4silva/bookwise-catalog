using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Application.Handlers.v1.UpdateBookCover;

[ExcludeFromCodeCoverage]
public sealed class UpdateBookCoverCommand : IRequest<UpdateBookCoverResult>
{
    [FromRoute] 
    public Guid Id { get; set; }
    
    [FromForm(Name = "image")]
    public IFormFile? Image { get; set; }
}