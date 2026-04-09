using FitAgenda.Domain.Notifications;

namespace FitAgenda.Domain.Interfaces;

public interface INotificador
{
    void AdicionarNotificacao(Notificacao notificacao);
    List<Notificacao> ObterNotificacoes();
    bool TemNotificacao();
}
