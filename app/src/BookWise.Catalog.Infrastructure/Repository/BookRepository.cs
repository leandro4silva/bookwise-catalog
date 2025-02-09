using BookWise.Catalog.Domain.Entities;
using BookWise.Catalog.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BookWise.Catalog.Infrastructure.Repository;

public class BookRepository : IBookRepository
{
    private readonly IMongoCollection<Book> _collection;
    private readonly ILogger<BookRepository> _logger;

    public BookRepository(IMongoDatabase database, ILogger<BookRepository> logger)
    {
        _collection = database.GetCollection<Book>("books");
        _logger = logger;
    }
    
    public async Task AddAsync(Book book, CancellationToken cancellationToken)
    {
        try
        {
            await _collection.InsertOneAsync(book, cancellationToken: cancellationToken);
            _logger.LogInformation("Livro inserido com sucesso: {BookId}, Título: {Title}", book.Id, book.Title);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao inserir o livro na coleção {CollectionName}. Livro: {BookId}, Título: {Title}. Detalhes: {ErrorMessage}",
                _collection.CollectionNamespace.CollectionName, 
                book.Id,
                book.Title, 
                ex.Message);
            throw;
        }
    }

    public async Task<bool> Update(Book book, CancellationToken cancellationToken)
    {
        try
        {
            var updateResult = await _collection.ReplaceOneAsync(
                filter: c => c.Id == book.Id,
                replacement: book,
                cancellationToken: cancellationToken
            );
            
            if (updateResult.MatchedCount == 0)
            {
                _logger.LogWarning("Livro não encontrado para atualização. ID: {BookId}", book.Id);
                return false;
            }

            _logger.LogInformation("Livro atualizado com sucesso. ID: {BookId}, Título: {Title}", book.Id, book.Title);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar o livro na coleção {CollectionName}. ID: {BookId}, Título: {Title}. Detalhes: {ErrorMessage}",
                _collection.CollectionNamespace.CollectionName,
                book.Id, 
                book.Title, 
                ex.Message); 
            throw;
        }
    }

    public async Task<Book> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var book = await _collection.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            _logger.LogInformation("Livro encontrado com sucesso: {BookId}, Título: {Title}", book.Id, book.Title);
            return book;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar o livro na coleção {CollectionName}. ID: {BookId}. Detalhes: {ErrorMessage}",
                _collection.CollectionNamespace.CollectionName,
                id, 
                ex.Message); 
            throw;
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<Book>.Filter.Eq(b => b.Id, id);
            
            var deleteResult = await _collection.DeleteOneAsync(filter, cancellationToken);
            
            if (deleteResult.DeletedCount == 0)
            {
                _logger.LogWarning("Livro não encontrado para exclusão. ID: {BookId}", id);
                return false;
            }

            _logger.LogInformation("Livro excluído com sucesso. ID: {BookId}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir o livro na coleção {CollectionName}. ID: {BookId}. Detalhes: {ErrorMessage}",
                _collection.CollectionNamespace.CollectionName,
                id,
                ex.Message); 
            throw; 
        }
    }
}