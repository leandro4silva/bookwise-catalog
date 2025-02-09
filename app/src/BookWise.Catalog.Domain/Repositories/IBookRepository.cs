using DomainEntity = BookWise.Catalog.Domain.Entities;

namespace BookWise.Catalog.Domain.Repositories;

public interface IBookRepository
{
    Task AddAsync(DomainEntity.Book book, CancellationToken cancellationToken);

    Task<bool>  Update(DomainEntity.Book book, CancellationToken cancellationToken);

    Task<DomainEntity.Book> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}