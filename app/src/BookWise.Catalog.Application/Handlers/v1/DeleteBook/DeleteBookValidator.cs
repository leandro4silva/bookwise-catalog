using FluentValidation;

namespace BookWise.Catalog.Application.Handlers.v1.DeleteBook;

public sealed class DeleteBookValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithName("id");
    }
}