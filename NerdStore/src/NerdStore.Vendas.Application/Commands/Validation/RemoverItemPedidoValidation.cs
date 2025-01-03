using FluentValidation;

namespace NerdStore.Vendas.Application.Commands.Validation;

public class RemoverItemPedidoValidation : AbstractValidator<RemoverItemPedidoCommand>
{
    public RemoverItemPedidoValidation()
    {
        RuleFor(c => c.ClienteId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do cliente inválido");

        RuleFor(c => c.ProdutoId)
            .NotEqual(Guid.Empty)
            .WithMessage("Id do produto inválido");
    }
}