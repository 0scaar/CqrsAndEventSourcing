using NerdStore.Core.Data;

namespace NerdStore.Pagamentos.Business.Repositories;

public interface IPagamentoRepository : IRepository<Pagamento>
{
    void Adicionar(Pagamento pagamento);
    void AdicionarTransacao(Transacao transacao);
}