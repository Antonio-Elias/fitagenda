using FitAgenda.Domain.Models;
using FluentValidation;

namespace FitAgenda.Domain.Validations;

public class TipoAulaValidation : AbstractValidator<TipoAula>
{
    public TipoAulaValidation()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");
    }
}
