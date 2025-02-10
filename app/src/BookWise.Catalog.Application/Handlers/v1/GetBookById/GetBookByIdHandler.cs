using MediatR;

namespace BookWise.Catalog.Application.Handlers.v1.GetBookById;

public sealed class GetBookByIdHandler : IRequestHandler<GetBookByIdCommand, GetBookByIdResult>
{
    public Task<GetBookByIdResult> Handle(GetBookByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}