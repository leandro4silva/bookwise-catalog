namespace BookWise.Catalog.Domain.ValueObjects;

public sealed class Rate
{
    public decimal Valor { get; private set; }

    public Rate(decimal valor)
    {
        if (valor < 0 || valor > 5)
            throw new ArgumentException("Nota média inválida.");

        Valor = valor;
    }
}