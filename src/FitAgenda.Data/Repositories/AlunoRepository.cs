using FitAgenda.Data.Context;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FitAgenda.Data.Repositories;

public class AlunoRepository : Repository<Aluno>, IAlunoRepository
{
    public AlunoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Aluno?> ObterPorIdComDetalhes(Guid codigoAluno)
    {
        return await Db.Alunos
            .Include(aluno => aluno.Agendamentos)
                .ThenInclude(agendamento => agendamento.Aula)
                    .ThenInclude(aula => aula.TipoAula)
            .FirstOrDefaultAsync(aluno => aluno.Id == codigoAluno);
    }
}
