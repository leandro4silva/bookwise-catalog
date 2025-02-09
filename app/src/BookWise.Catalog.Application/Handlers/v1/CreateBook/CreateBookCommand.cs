using BookWise.Catalog.Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Catalog.Application.Handlers.v1.CreateBook;

public sealed class CreateBookCommand : IRequest<CreateBookResult>
{
    [FromBody] 
    public PayloadCreateBookRequest? Payload { get; set; }
}