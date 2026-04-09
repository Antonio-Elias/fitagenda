using FitAgenda.Domain.Enums;

namespace FitAgenda.Api.Dtos.Aluno;

public class AlunoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoPlano TipoPlano { get; set; }
    public bool Ativo { get; set; }
}
