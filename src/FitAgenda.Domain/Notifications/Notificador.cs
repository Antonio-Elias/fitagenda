using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Notifications;

namespace FitAgenda.Domain.Notifications;

public class Notificador : INotificador
{
    private readonly List<Notificacao> _notificacoes = new();

    public void AdicionarNotificacao(Notificacao notificacao)
    {
        _notificacoes.Add(notificacao);
    }

    public List<Notificacao> ObterNotificacoes()
        => _notificacoes;

    public bool TemNotificacao()
        => _notificacoes.Any();
}