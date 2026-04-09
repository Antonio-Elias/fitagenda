using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using FitAgenda.Domain.Rules;
using FitAgenda.Domain.Validations;

namespace FitAgenda.Domain.Services;

public class AgendamentoService : BaseService, IAgendamentoService
{
    public AgendamentoService(INotificador notificador)
        : base(notificador)
    {
    }

    public Agendamento? Agendar(Aluno aluno, Aula aula)
    {
        ValidarEntidades(aluno, aula);
        ValidarRegrasDeNegocio(aluno, aula);

        if (TemNotificacao()) return null;

        return new Agendamento(aluno.Id, aula.Id);
    }

    public void Cancelar(Agendamento agendamento)
    {
        if (!agendamento.Ativo)
        {
            Notificar("Agendamento ja esta cancelado.");
            return;
        }

        agendamento.Cancelar();
    }

    private void ValidarEntidades(Aluno aluno, Aula aula)
    {
        ExecutarValidacao(new AlunoValidation(), aluno);
        ExecutarValidacao(new AulaValidation(), aula);
    }

    private void ValidarRegrasDeNegocio(Aluno aluno, Aula aula)
    {
        ValidarAlunoAtivo(aluno);
        ValidarAulaAtiva(aula);
        ValidarTipoAulaAtivo(aula);
        ValidarCapacidadeDaAula(aula);
        ValidarAlunoJaAgendado(aluno, aula.Id);
        ValidarLimiteDoPlano(aluno);
    }

    private void ValidarAlunoAtivo(Aluno aluno)
    {
        if (!aluno.Ativo)
            Notificar("Aluno inativo nao pode realizar agendamentos.");
    }

    private void ValidarAulaAtiva(Aula aula)
    {
        if (!aula.Ativa)
            Notificar("Aula inativa nao permite novos agendamentos.");
    }

    private void ValidarTipoAulaAtivo(Aula aula)
    {
        if (!aula.TipoAula.Ativo)
            Notificar("Tipo de aula inativo nao permite novos agendamentos.");
    }

    private void ValidarCapacidadeDaAula(Aula aula)
    {
        var totalAgendamentosAtivos = aula.Agendamentos.Count(agendamento => agendamento.Ativo);

        if (totalAgendamentosAtivos >= aula.CapacidadeMaxima)
            Notificar("A aula ja esta lotada.");
    }

    private void ValidarAlunoJaAgendado(Aluno aluno, Guid aulaId)
    {
        if (aluno.Agendamentos.Any(agendamento => agendamento.CodigoAula == aulaId && agendamento.Ativo))
            Notificar("Aluno ja esta agendado nesta aula.");
    }

    private void ValidarLimiteDoPlano(Aluno aluno)
    {
        var agoraUtc = DateTime.UtcNow;
        var limitePlano = TipoPlanoRules.ObterLimitePlano(aluno.TipoPlano);

        var totalAulasNoMes = aluno.Agendamentos.Count(agendamento =>
            agendamento.Ativo &&
            agendamento.Aula.DataHora.Month == agoraUtc.Month &&
            agendamento.Aula.DataHora.Year == agoraUtc.Year);

        if (totalAulasNoMes >= limitePlano)
            Notificar("Limite de agendamentos do plano atingido.");
    }
}
