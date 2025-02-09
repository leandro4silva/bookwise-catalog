using BookWise.Catalog.Domain.Entities.Abstraction;
using BookWise.Catalog.Domain.Events.Abstraction;

namespace BookWise.Catalog.Domain.Entities;

public class AggregateRoot : IEntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    private List<IDomainEvent> _events = new List<IDomainEvent>();

    public IEnumerable<IDomainEvent> Events => _events;

    protected void AddEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }
}