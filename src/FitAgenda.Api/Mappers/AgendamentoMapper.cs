using FitAgenda.Api.Dtos.Agendamento;
using FitAgenda.Domain.Models;

namespace FitAgenda.Api.Mappers;

public static class AgendamentoMapper
{
    public static AgendamentoDto ParaDto(Agendamento agendamento)
    {
        return new AgendamentoDto
        {
            Id = agendamento.Id,
            CodigoAluno = agendamento.CodigoAluno,
            NomeAluno = agendamento.Aluno?.Nome ?? string.Empty,
            CodigoAula = agendamento.CodigoAula,
            DataHoraAula = agendamento.Aula?.DataHora ?? default,
            NomeTipoAula = agendamento.Aula?.TipoAula?.Nome ?? string.Empty,
            DataAgendamento = agendamento.DataAgendamento,
            Ativo = agendamento.Ativo
        };
    }
}
