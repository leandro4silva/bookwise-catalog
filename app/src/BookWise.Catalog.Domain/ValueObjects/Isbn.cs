namespace BookWise.Catalog.Domain.ValueObjects;

public sealed class Isbn
{
    public string Valor { get; private set; }

    public Isbn(string valor)
    {
        if (!ValidateISBN(valor))
            throw new ArgumentException("ISBN inválido.");

        Valor = valor;
    }

    private bool ValidateISBN(string isbn)
    {
        return true; 
    }
}