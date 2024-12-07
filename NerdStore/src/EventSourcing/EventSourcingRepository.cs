using System.Text;
using System.Text.Json;
using EventStore.Client;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;

namespace EventSourcing;

public class EventSourcingRepository : IEventSourcingRepository
{
    private readonly EventStoreClient _eventStoreClient;

    public EventSourcingRepository(IEventStoreService eventStoreService)
    {
        _eventStoreClient = eventStoreService.GetClient();
    }
    
    public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
    {
        var eventData = FormatarEvento(evento);

        await _eventStoreClient.AppendToStreamAsync(
            evento.AggregateId.ToString(),
            StreamState.Any,
            new[] { eventData });
    }
    
    public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId)
    {
        //TODO: pesquisar como paginar
        var eventos = _eventStoreClient.ReadStreamAsync(
            Direction.Forwards,
            aggregateId.ToString(),
            StreamPosition.Start);

        var listaEventos = new List<StoredEvent>();

        await foreach (var resolvedEvent in eventos)
        {
            var dataEncoded = Encoding.UTF8.GetString(resolvedEvent.Event.Data.Span);
            var jsonData = JsonSerializer.Deserialize<BaseEvent>(dataEncoded);

            var evento = new StoredEvent(
                resolvedEvent.Event.EventId.ToGuid(),
                resolvedEvent.Event.EventType,
                jsonData.Timestamp,
                dataEncoded);

            listaEventos.Add(evento);
        }

        return listaEventos.OrderBy(e => e.DataOcorrencia);
    }
    
    private static EventData FormatarEvento<TEvent>(TEvent evento) where TEvent : Event
    {
        return new EventData(
            Uuid.NewUuid(),
            evento.MessageType,
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(evento)),
            null);
    }
}

internal class BaseEvent
{
    public DateTime Timestamp { get; set; }
}