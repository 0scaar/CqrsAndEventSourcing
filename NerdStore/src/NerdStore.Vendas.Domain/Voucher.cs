using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using NerdStore.Vendas.Domain.Validation;

namespace NerdStore.Vendas.Domain;

public class Voucher : Entity
{
    public string Codigo { get; private set; }
    public decimal? Percentual { get; private set; }
    public decimal? ValorDesconto { get; private set; }
    public int Quantidade { get; private set; }
    public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
    public DateTime DataCriacao { get; private set; }
    public DateTime? DataUtilizacao { get; private set; }
    public DateTime DataValidade { get; private set; }
    public bool Ativo { get; private set; }
    public bool Utilizado { get; private set; }

    // EF Rel.
    public ICollection<Pedido> Pedidos { get; set; }
    
    internal ValidationResult ValidarSeAplicavel()
    {
        return new VoucherAplicavelValidation().Validate(this);
    }
}