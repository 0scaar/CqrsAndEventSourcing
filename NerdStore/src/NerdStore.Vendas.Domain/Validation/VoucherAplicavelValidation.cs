using FluentValidation;

namespace NerdStore.Vendas.Domain.Validation;

public class VoucherAplicavelValidation : AbstractValidator<Voucher>
{
    public VoucherAplicavelValidation()
    {
        RuleFor(c => c.DataValidade)
            .Must(DataVencimentoSuperiorAtual)
            .WithMessage("Este voucher está expirado.");

        RuleFor(c => c.Ativo)
            .Equal(true)
            .WithMessage("Este voucher não é mais válido.");

        RuleFor(c => c.Utilizado)
            .Equal(false)
            .WithMessage("Este voucher já foi utilizado.");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0)
            .WithMessage("Este voucher não está mais disponível");
    }

    protected static bool DataVencimentoSuperiorAtual(DateTime dataValidade)
    {
        return dataValidade >= DateTime.Now;
    }
}