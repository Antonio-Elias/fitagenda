using FitAgenda.Domain.Models;
using FluentValidation;

namespace FitAgenda.Domain.Validations;

public class AlunoValidation : AbstractValidator<Aluno>
{
    public AlunoValidation()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório")
            .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");

        RuleFor(x => x.TipoPlano)
            .IsInEnum().WithMessage("Tipo de plano inválido");
    }
}
