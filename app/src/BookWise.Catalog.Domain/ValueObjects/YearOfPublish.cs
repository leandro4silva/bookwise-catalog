namespace BookWise.Catalog.Domain.ValueObjects;

public sealed class YearOfPublish
{
    public int Valor { get; private set; }

    public YearOfPublish(int valor)
    {
        if (valor < 1450 || valor > DateTime.Now.Year)
            throw new ArgumentException("Ano de publicação inválido.");

        Valor = valor;
    }
}