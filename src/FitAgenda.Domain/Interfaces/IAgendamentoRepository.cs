using FitAgenda.Domain.Models;

namespace FitAgenda.Domain.Interfaces;

public interface IAgendamentoRepository : IRepository<Agendamento>
{
    Task<List<Agendamento>> ObterTodosComDetalhes();
    Task<Agendamento?> ObterPorIdComDetalhes(Guid id);
    Task<Aluno?> ObterAlunoComAgendamentos(Guid codigoAluno);
    Task<Aula?> ObterAulaComAgendamentos(Guid codigoAula);
}
