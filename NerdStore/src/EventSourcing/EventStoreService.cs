using EventStore.Client;
using Microsoft.Extensions.Configuration;

namespace EventSourcing;

public class EventStoreService : IEventStoreService
{
    private readonly EventStoreClient _client;

    public EventStoreService(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EventStoreConnection");

        // Configura el cliente usando la cadena de conexión
        var settings = EventStoreClientSettings.Create(connectionString);
        settings.DefaultCredentials = new UserCredentials("admin", "changeit");
        //settings.ConnectivitySettings.KeepAliveTimeout = TimeSpan.FromMilliseconds(500);
        
        _client = new EventStoreClient(settings);
    }

    public EventStoreClient GetClient()
    {
        return _client;
    }
}