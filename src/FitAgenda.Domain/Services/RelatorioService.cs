using FitAgenda.Domain.Dtos;
using FitAgenda.Domain.Interfaces;

namespace FitAgenda.Domain.Services;

public class RelatorioService : BaseService, IRelatorioService
{
    private readonly IRelatorioRepository _relatorioRepository;

    public RelatorioService(IRelatorioRepository relatorioRepository, INotificador notificador)
        : base(notificador)
    {
        _relatorioRepository = relatorioRepository;
    }

    public async Task<RelatorioAlunoDto?> ObterRelatorioMensalAluno(Guid codigoAluno, int mes, int ano)
    {
        ValidarParametros(codigoAluno, mes, ano);

        if (TemNotificacao())
            return null;

        var relatorio = await _relatorioRepository.ObterRelatorioMensalAluno(codigoAluno, mes, ano);

        if (relatorio is null)
        {
            Notificar("Aluno nao encontrado.");
            return null;
        }

        return relatorio;
    }

    private void ValidarParametros(Guid codigoAluno, int mes, int ano)
    {
        if (codigoAluno == Guid.Empty)
            Notificar("O aluno informado e invalido.");

        if (mes is < 1 or > 12)
            Notificar("O mes informado e invalido.");

        if (ano < 1)
            Notificar("O ano informado e invalido.");
    }
}
