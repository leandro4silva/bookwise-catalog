using FluentValidation;

namespace BookWise.Catalog.Application.Handlers.v1.UpdateBook;

public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookValidator()
    {
        
    }
}