using FitAgenda.Domain.Dtos;

namespace FitAgenda.Domain.Interfaces;

public interface IRelatorioRepository
{
    Task<RelatorioAlunoDto?> ObterRelatorioMensalAluno(Guid codigoAluno, int mes, int ano);
}
