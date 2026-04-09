using FitAgenda.Data.Context;
using FitAgenda.Domain.Dtos;
using FitAgenda.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitAgenda.Data.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly AppDbContext _context;

    public RelatorioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RelatorioAlunoDto?> ObterRelatorioMensalAluno(Guid codigoAluno, int mes, int ano)
    {
        var alunoExiste = await _context.Alunos
            .AsNoTracking()
            .AnyAsync(aluno => aluno.Id == codigoAluno);

        if (!alunoExiste)
            return null;

        var dadosAluno = await _context.Alunos
            .AsNoTracking()
            .Where(aluno => aluno.Id == codigoAluno)
            .Select(aluno => new
            {
                aluno.Id,
                aluno.Nome
            })
            .FirstAsync();

        var agendamentosDoMes = await _context.Agendamentos
            .AsNoTracking()
            .Where(agendamento =>
                agendamento.CodigoAluno == codigoAluno &&
                agendamento.Ativo &&
                agendamento.Aula.DataHora.Month == mes &&
                agendamento.Aula.DataHora.Year == ano)
            .Select(agendamento => new
            {
                NomeTipoAula = agendamento.Aula.TipoAula.Nome
            })
            .ToListAsync();

        var tiposAulaMaisFrequentes = agendamentosDoMes
            .GroupBy(item => item.NomeTipoAula)
            .Select(grupo => new TipoAulaFrequenciaDto
            {
                NomeTipoAula = grupo.Key,
                Quantidade = grupo.Count()
            })
            .OrderByDescending(tipoAula => tipoAula.Quantidade)
            .ThenBy(tipoAula => tipoAula.NomeTipoAula)
            .ToList();

        return new RelatorioAlunoDto
        {
            CodigoAluno = dadosAluno.Id,
            NomeAluno = dadosAluno.Nome,
            MesReferencia = mes,
            AnoReferencia = ano,
            TotalAulasAgendadasNoMes = agendamentosDoMes.Count,
            TiposAulaMaisFrequentes = tiposAulaMaisFrequentes
        };
    }
}
