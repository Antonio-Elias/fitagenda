using FitAgenda.Domain.Models;
using FluentValidation;

namespace FitAgenda.Domain.Validations;

public class AgendamentoValidation : AbstractValidator<Agendamento>
{
    public AgendamentoValidation()
    {
        RuleFor(x => x.CodigoAluno)
            .NotEmpty().WithMessage("O aluno é obrigatório");

        RuleFor(x => x.CodigoAula)
            .NotEmpty().WithMessage("A aula é obrigatória");

        RuleFor(x => x.DataAgendamento)
            .NotEmpty().WithMessage("A data do agendamento é obrigatória");
    }
}
