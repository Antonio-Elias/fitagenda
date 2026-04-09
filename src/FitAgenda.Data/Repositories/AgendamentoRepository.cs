using FitAgenda.Data.Context;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitAgenda.Data.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<List<Agendamento>> ObterTodosComDetalhes()
        {
            return await Db.Agendamentos
                .Include(agendamento => agendamento.Aluno)
                .Include(agendamento => agendamento.Aula)
                    .ThenInclude(aula => aula.TipoAula)
                .ToListAsync();
        }

        public async Task<Agendamento?> ObterPorIdComDetalhes(Guid codigoAgendamento)
        {
            return await Db.Agendamentos
                .Include(agendamento => agendamento.Aluno)
                .Include(agendamento => agendamento.Aula)
                    .ThenInclude(aula => aula.TipoAula)
                .FirstOrDefaultAsync(agendamento => agendamento.Id == codigoAgendamento);
        }

        public async Task<Aluno?> ObterAlunoComAgendamentos(Guid codigoAluno)
        {
            return await Db.Alunos
                .Include(aluno => aluno.Agendamentos)
                    .ThenInclude(agendamento => agendamento.Aula)
                        .ThenInclude(aula => aula.TipoAula)
                .FirstOrDefaultAsync(aluno => aluno.Id == codigoAluno);
        }

        public async Task<Aula?> ObterAulaComAgendamentos(Guid codigoAula)
        {
            return await Db.Aulas
                .Include(aula => aula.Agendamentos)
                .Include(aula => aula.TipoAula)
                .FirstOrDefaultAsync(aula => aula.Id == codigoAula);
        }
    }
}
