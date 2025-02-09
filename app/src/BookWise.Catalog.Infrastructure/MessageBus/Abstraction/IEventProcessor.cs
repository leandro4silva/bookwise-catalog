using BookWise.Catalog.Domain.Events.Abstraction;

namespace BookWise.Catalog.Infrastructure.MessageBus.Abstraction;

public interface IEventProcessor
{
    void Process(IEnumerable<IDomainEvent> events, string queueUrl, CancellationToken cancellationToken);
}
