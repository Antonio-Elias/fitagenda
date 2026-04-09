using FitAgenda.Domain.Models;

namespace FitAgenda.Domain.Interfaces;

public interface IAlunoRepository : IRepository<Aluno>
{
    Task<Aluno?> ObterPorIdComDetalhes(Guid codigoAluno);
}
