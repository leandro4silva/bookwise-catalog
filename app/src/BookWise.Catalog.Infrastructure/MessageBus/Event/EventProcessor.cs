using System.Text;
using BookWise.Catalog.Domain.Events.Abstraction;
using BookWise.Catalog.Infrastructure.MessageBus.Abstraction;

namespace BookWise.Catalog.Infrastructure.MessageBus.Event;

public sealed class EventProcessor : IEventProcessor
{
    private readonly IPublisher _publisher;
    private readonly string _queueUrl = "";

    public EventProcessor(
        IPublisher publisher)
    {
        _publisher = publisher;
    }

    public IEnumerable<IEvent> MapAll(IEnumerable<IDomainEvent> events)
    {
        return events.Select(Map);
    }

    public IEvent Map(IDomainEvent @event)
    {
        return @event switch
        {
            _ => throw new InvalidOperationException($"Evento não suportado: {@event.GetType().Name}")
        };
    }

    public async void Process(IEnumerable<IDomainEvent> events, string queueUrl, CancellationToken cancellationToken)
    {
        var integrationEvents = MapAll(events);

        foreach (var @event in integrationEvents)
        {
            await _publisher.PublishAsync(@event, _queueUrl, cancellationToken);
        }
    }

    private string MapConvention(IEvent @event)
    {
        return ToDashCase(@event.GetType().Name);
    }

    public string ToDashCase(string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }
        if (text.Length < 2)
        {
            return text;
        }
        var sb = new StringBuilder();
        sb.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                sb.Append('-');
                sb.Append(char.ToLowerInvariant(c));
            }
            else
            {
                sb.Append(c);
            }
        }

        Console.WriteLine($"ToDashCase: " + sb.ToString());

        return sb.ToString();
    }
}
