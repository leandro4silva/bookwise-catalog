using System.Diagnostics.CodeAnalysis;
using BookWise.Catalog.Infrastructure.Notifications.Abstraction;

namespace BookWise.Catalog.Infrastructure.Notifications;

[ExcludeFromCodeCoverage]
public sealed class NotificationService : INotificationService
{
    private readonly List<ErroResponse> _erros = new();

    public void Adicionar(ErroResponse erroResponse) =>
        _erros.Add(erroResponse);

    public bool ExisteNotificacao() =>
        _erros.ElementAtOrDefault(0) != null;

    public List<ErroResponse> ObterTodos() => _erros;
}
