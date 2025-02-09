using FluentValidation;

namespace BookWise.Catalog.Application.Handlers.v1.CreateBook;

public sealed class CreateBookValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookValidator()
    {
        RuleFor(x => x.Payload!.Title)
            .NotEmpty()
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(100)
            .WithName("title");
        
        RuleFor(x => x.Payload!.Description)
            .NotEmpty()
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(1000)
            .WithName("description");
        
        RuleFor(x => x.Payload!.Isbn)
            .NotEmpty()
            .NotNull()
            .Must(BeAValidIsbn)
            .WithMessage("O ISBN fornecido não é válido.")
            .WithName("Isbn");
        
        RuleFor(x => x.Payload!.Publisher)
            .NotEmpty()
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(100)
            .WithName("publisher");
        
        RuleFor(x => x.Payload!.GenreBook)
            .NotNull()
            .NotEmpty()
            .IsInEnum()
            .WithName("genre");
        
        RuleFor(x => x.Payload!.YearOfPublish)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1450)
            .LessThanOrEqualTo(DateTime.Now.Year)
            .WithName("yearOfPublish");
        
        RuleFor(x => x.Payload!.NumberOfPages)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100000)
            .WithName("numberOfPages");
        
        RuleFor(x => x.Payload!.NumberOfPages)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100000)
            .WithName("numberOfPages");
    }
    
    private bool BeAValidIsbn(string? isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn))
            return false;
        
        isbn = isbn.Replace("-", "").Replace(" ", "");

        // Verifica se é ISBN-10 ou ISBN-13
        if (isbn.Length == 10)
            return IsValidIsbn10(isbn);
        else if (isbn.Length == 13)
            return IsValidIsbn13(isbn);
        else
            return false;
    }
    
    private bool IsValidIsbn10(string isbn)
    {
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            if (!char.IsDigit(isbn[i]))
                return false;

            sum += (isbn[i] - '0') * (10 - i);
        }
        
        char lastChar = isbn[9];
        if (lastChar == 'X')
            sum += 10;
        else if (char.IsDigit(lastChar))
            sum += lastChar - '0';
        else
            return false;

        return sum % 11 == 0;
    }
    
    private bool IsValidIsbn13(string isbn)
    {
        int sum = 0;
        for (int i = 0; i < 12; i++)
        {
            if (!char.IsDigit(isbn[i]))
                return false;

            int digit = isbn[i] - '0';
            sum += (i % 2 == 0) ? digit : digit * 3;
        }

        // Verifica o último dígito
        if (!char.IsDigit(isbn[12]))
            return false;

        int checkDigit = isbn[12] - '0';
        int remainder = sum % 10;
        int calculatedCheckDigit = (10 - remainder) % 10;

        return checkDigit == calculatedCheckDigit;
    }
}