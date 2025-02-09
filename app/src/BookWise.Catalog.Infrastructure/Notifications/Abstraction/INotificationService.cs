﻿namespace BookWise.Catalog.Infrastructure.Notifications.Abstraction;

public interface INotificationService
{
    bool ExisteNotificacao();

    void Adicionar(ErroResponse erro);

    List<ErroResponse> ObterTodos();
}
