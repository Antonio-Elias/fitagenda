using FitAgenda.Api.Dtos.Relatorio;
using FitAgenda.Domain.Dtos;
using RelatorioTipoAulaFrequenciaDto = FitAgenda.Api.Dtos.Relatorio.TipoAulaFrequenciaDto;

namespace FitAgenda.Api.Mappers;

public static class RelatorioMapper
{
    public static RelatorioMensalAlunoDto ParaDto(RelatorioAlunoDto relatorio)
    {
        return new RelatorioMensalAlunoDto
        {
            CodigoAluno = relatorio.CodigoAluno,
            NomeAluno = relatorio.NomeAluno,
            MesReferencia = relatorio.MesReferencia,
            AnoReferencia = relatorio.AnoReferencia,
            TotalAulasAgendadasNoMes = relatorio.TotalAulasAgendadasNoMes,
            TiposAulaMaisFrequentes = relatorio.TiposAulaMaisFrequentes
                .Select(tipo => new RelatorioTipoAulaFrequenciaDto
                {
                    NomeTipoAula = tipo.NomeTipoAula,
                    Quantidade = tipo.Quantidade
                })
                .ToList()
        };
    }
}
