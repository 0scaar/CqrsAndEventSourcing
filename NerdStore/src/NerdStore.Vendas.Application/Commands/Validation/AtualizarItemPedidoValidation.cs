using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.Validation;

public class AtualizarItemPedidoValidation : AbstractValidator<AtualizarItemPedidoCommand>
{
    public AtualizarItemPedidoValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");

        RuleFor(c => c.Quantidade)
            .GreaterThan(0).WithMessage("A quantidade miníma de um item é 1")
            .LessThan(15).WithMessage("A quantidade máxima de um item é 15");
    }
}