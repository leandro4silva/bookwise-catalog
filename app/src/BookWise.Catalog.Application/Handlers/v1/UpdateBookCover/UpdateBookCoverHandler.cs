using System.Text.Json;
using AutoMapper;
using BookWise.Catalog.Application.Exceptions;
using BookWise.Catalog.Application.Helpers;
using BookWise.Catalog.Domain.Repositories;
using BookWise.Catalog.Infrastructure.Buckets.Abstractions;
using BookWise.Catalog.Infrastructure.LogAudit.Abstractions;
using BookWise.Catalog.Infrastructure.LogAudit.Dtos;
using BookWise.Catalog.Infrastructure.LogAudit.Enums;
using BookWise.Catalog.Infrastructure.Notifications.Abstraction;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookWise.Catalog.Application.Handlers.v1.UpdateBookCover;

public sealed class UpdateBookCoverHandler : IRequestHandler<UpdateBookCoverCommand, UpdateBookCoverResult>
{
    private readonly ILogger<UpdateBookCoverHandler> _logger;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;
    private readonly ILogAuditService _logAuditService;
    private readonly IBookRepository _bookRepository;
    private readonly IBucketS3Service _bucketS3Service;
    
    public UpdateBookCoverHandler(
        ILogger<UpdateBookCoverHandler> logger, 
        INotificationService notificationService, 
        IMapper mapper, 
        ILogAuditService logAuditService, 
        IBookRepository bookRepository,
        IBucketS3Service bucketS3Service)
    {
        _logger = logger;
        _notificationService = notificationService;
        _mapper = mapper;
        _logAuditService = logAuditService;
        _bookRepository = bookRepository;
        _bucketS3Service = bucketS3Service;
    }
    
    public async Task<UpdateBookCoverResult> Handle(UpdateBookCoverCommand request, CancellationToken cancellationToken)
    {
        _ = AuditLog(request, cancellationToken);

        return await UpdateBookCover(request, cancellationToken);
    }

    private async Task<UpdateBookCoverResult> UpdateBookCover(UpdateBookCoverCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
            
            NotFoundException.ThrowIfNull(book, "Livro não cadastrado");
            
            var tempFilePath = Path.GetTempFileName();

            using (var stream = System.IO.File.Create(tempFilePath))
            {
                await request.Image!.CopyToAsync(stream);
            }

            var key = Guid.NewGuid().ToString() + Path.GetExtension(request.Image!.FileName);

            var bookCoverUrl = await _bucketS3Service.UploadFileAsync(tempFilePath, key, cancellationToken);
            
            book.BookCover = bookCoverUrl;
            
            await _bookRepository.Update(book, cancellationToken);
            
            return _mapper.Map<UpdateBookCoverResult>(book);
        }
        catch (Exception ex)
        {
            var msg = "Erro indefinido ao atualizar capa do livro";
            NotificationHelper.Notificar(ex, ex.Message, _notificationService, _logger);
            throw new InternalServerErrorException(msg);
        }
    }
    
    private Task AuditLog(UpdateBookCoverCommand request, CancellationToken cancellationToken)
    {
        var log = new LogAuditCommand(
            operacao: AuditoriaOperacao.Insercao,
            descricao:$"Atualizar capa do livro" + $"request {JsonSerializer.Serialize(request) }"
        );

        return _logAuditService.AuditAsync(log);
    }
}