using NerdStore.Core.DomainObjects.DTO;

namespace NerdStore.Pagamentos.Business.Services;

public interface IPagamentoService
{
    Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
}