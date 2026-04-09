using FitAgenda.Api.Dtos.TipoAula;
using FitAgenda.Domain.Models;

namespace FitAgenda.Api.Mappers;

public static class TipoAulaMapper
{
    public static TipoAulaDto ParaDto(TipoAula tipoAula)
    {
        return new TipoAulaDto
        {
            Id = tipoAula.Id,
            Nome = tipoAula.Nome,
            Ativo = tipoAula.Ativo
        };
    }
}
