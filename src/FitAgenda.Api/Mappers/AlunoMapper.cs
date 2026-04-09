using FitAgenda.Api.Dtos.Aluno;
using FitAgenda.Domain.Models;

namespace FitAgenda.Api.Mappers;

public static class AlunoMapper
{
    public static AlunoDto ParaDto(Aluno aluno)
    {
        return new AlunoDto
        {
            Id = aluno.Id,
            Nome = aluno.Nome,
            TipoPlano = aluno.TipoPlano,
            Ativo = aluno.Ativo
        };
    }
}
