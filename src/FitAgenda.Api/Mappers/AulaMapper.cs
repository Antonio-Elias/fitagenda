using FitAgenda.Api.Dtos.Aula;
using FitAgenda.Domain.Models;

namespace FitAgenda.Api.Mappers;

public static class AulaMapper
{
    public static AulaDto ParaDto(Aula aula)
    {
        return new AulaDto
        {
            Id = aula.Id,
            CodigoTipoAula = aula.CodigoTipoAula,
            NomeTipoAula = aula.TipoAula?.Nome ?? string.Empty,
            DataHora = aula.DataHora,
            CapacidadeMaxima = aula.CapacidadeMaxima,
            Ativa = aula.Ativa
        };
    }
}
