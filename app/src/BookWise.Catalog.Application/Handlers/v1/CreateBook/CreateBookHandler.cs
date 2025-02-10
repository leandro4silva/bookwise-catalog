using System.Text.Json;
using AutoMapper;
using BookWise.Catalog.Application.Exceptions;
using BookWise.Catalog.Application.Helpers;
using BookWise.Catalog.Domain.Entities;
using BookWise.Catalog.Domain.Repositories;
using BookWise.Catalog.Infrastructure.LogAudit.Abstractions;
using BookWise.Catalog.Infrastructure.LogAudit.Dtos;
using BookWise.Catalog.Infrastructure.LogAudit.Enums;
using BookWise.Catalog.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookWise.Catalog.Application.Handlers.v1.CreateBook;

public sealed class CreateBookHandler : IRequestHandler<CreateBookCommand, CreateBookResult>
{
    private readonly ILogger<CreateBookHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly ILogAuditService _logAuditService;
    private readonly IBookRepository _bookRepository;

    public CreateBookHandler(
        ILogger<CreateBookHandler> logger, 
        INotificationService notificationService, 
        IMapper mapper, 
        ILogAuditService logAuditService, 
        IBookRepository bookRepository)
    {
        _logger = logger;
        _notificationService = notificationService;
        _mapper = mapper;
        _logAuditService = logAuditService;
        _bookRepository = bookRepository;
    }
    
    public async Task<CreateBookResult> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        _ = AuditLog(request, cancellationToken);

        return await CreateBookAsync(request, cancellationToken);
    }
    
    private async Task<CreateBookResult> CreateBookAsync(CreateBookCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var book = _mapper.Map<Book>(request);

            await _bookRepository.AddAsync(book, cancellationToken);

            return _mapper.Map<CreateBookResult>(book);
        }
        catch (ArgumentException ex)
        {
            var msg = ex.Message;
            NotificationHelper.Notificar(ex, ex.Message, _notificationService, _logger);
            throw new BadRequestException(msg);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido no cadastro de livro";
            NotificationHelper.Notificar(ex, msg, _notificationService, _logger);
            throw new InternalServerErrorException(msg);
        }
    }
    
    private Task AuditLog(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var log = new LogAuditCommand(
            operacao: AuditoriaOperacao.Insercao,
            descricao:$"Criacao de livro" + $"request {JsonSerializer.Serialize(request) }"
            );

        return _logAuditService.AuditAsync(log);
    }
}