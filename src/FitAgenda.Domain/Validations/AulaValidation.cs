using FitAgenda.Domain.Models;
using FluentValidation;

namespace FitAgenda.Domain.Validations;

public class AulaValidation : AbstractValidator<Aula>
{
    public AulaValidation()
    {
        RuleFor(x => x.CapacidadeMaxima)
            .GreaterThan(0).WithMessage("Capacidade deve ser maior que zero");

        RuleFor(x => x.DataHora)
            .GreaterThan(DateTime.UtcNow).WithMessage("A aula com periodo inválido");
    }
}
