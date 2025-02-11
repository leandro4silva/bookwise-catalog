using BookWise.Catalog.Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Application.Handlers.v1.UpdateBook;

public sealed class UpdateBookCommand : IRequest<UpdateBookResult>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }
    
    [FromBody] 
    public PayloadUpdateBookRequest? Payload { get; set; }
}