using BookWise.Catalog.Infrastructure.LogAudit.Dtos;

namespace BookWise.Catalog.Infrastructure.LogAudit.Abstractions;

public interface ILogAuditService
{
    Task AuditAsync(LogAuditCommand request);
}
