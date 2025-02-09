using FluentValidation;

namespace BookWise.Catalog.Application.Handlers.v1.UpdateBookCover;

public class UpdateBookCoverValidator : AbstractValidator<UpdateBookCoverCommand>
{
    public UpdateBookCoverValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("O campo 'email' não pode estar vazio.");
        
        RuleFor(x => x.Image)
            .NotNull()
            .WithMessage("A imagem é obrigatória.")
            .Must(file => file?.Length > 0)
            .WithMessage("O arquivo de imagem não pode estar vazio.")
            .Must(file => file?.ContentType.StartsWith("image/") ?? false)
            .WithMessage("O arquivo enviado não é uma imagem válida.");
    }
}