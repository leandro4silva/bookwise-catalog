using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace BookWise.Catalog.Application.Handlers.v1.UpdateBookCover;

[ExcludeFromCodeCoverage]
public sealed class UpdateBookCoverHandler : IRequestHandler<UpdateBookCoverCommand, UpdateBookCoverResult>
{
    public Task<UpdateBookCoverResult> Handle(UpdateBookCoverCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}