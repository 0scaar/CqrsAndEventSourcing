using NerdStore.Core.Messages;

namespace NerdStore.Vendas.Application.Events;

// Deveria ser compartilhado: poderia mandar email pro usuario
public class PedidoFinalizadoEvent : Event
{
    public Guid PedidoId { get; private set; }

    public PedidoFinalizadoEvent(Guid pedidoId)
    {
        PedidoId = pedidoId;
        AggregateId = pedidoId;
    }
}