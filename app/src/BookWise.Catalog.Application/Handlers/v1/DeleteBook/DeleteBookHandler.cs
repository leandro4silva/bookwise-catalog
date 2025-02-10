using System.Text.Json;
using AutoMapper;
using BookWise.Catalog.Application.Exceptions;
using BookWise.Catalog.Application.Handlers.v1.CreateBook;
using BookWise.Catalog.Application.Helpers;
using BookWise.Catalog.Domain.Repositories;
using BookWise.Catalog.Infrastructure.LogAudit.Abstractions;
using BookWise.Catalog.Infrastructure.LogAudit.Dtos;
using BookWise.Catalog.Infrastructure.LogAudit.Enums;
using BookWise.Catalog.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookWise.Catalog.Application.Handlers.v1.DeleteBook;

public sealed class DeleteBookHandler : IRequestHandler<DeleteBookCommand, DeleteBookResult>
{
    private readonly ILogger<CreateBookHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly ILogAuditService _logAuditService;
    private readonly IBookRepository _bookRepository;
    
    public DeleteBookHandler(ILogger<CreateBookHandler> logger, INotificationService notificationService, IMapper mapper, ILogAuditService logAuditService, IBookRepository bookRepository)
    {
        _logger = logger;
        _notificationService = notificationService;
        _mapper = mapper;
        _logAuditService = logAuditService;
        _bookRepository = bookRepository;
    }
    
    public async Task<DeleteBookResult> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        _ = AuditLog(request, cancellationToken);

        return await DeleteBookAsync(request, cancellationToken);
    }

    private async Task<DeleteBookResult> DeleteBookAsync(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
            
            NotFoundException.ThrowIfNull(book, "Livro não cadastrado");

            await _bookRepository.DeleteByIdAsync(book.Id, cancellationToken);

            return _mapper.Map<DeleteBookResult>(book);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido no cadastro de livro";
            NotificationHelper.Notificar(ex, msg, _notificationService, _logger);
            throw new InternalServerErrorException(msg);
        }
    }
    
    private Task AuditLog(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var log = new LogAuditCommand(
            operacao: AuditoriaOperacao.Insercao,
            descricao:$"Criacao de livro" + $"request {JsonSerializer.Serialize(request) }"
        );

        return _logAuditService.AuditAsync(log);
    }
}