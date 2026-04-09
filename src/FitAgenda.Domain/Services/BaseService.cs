using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace FitAgenda.Domain.Services;

public abstract class BaseService
{
    private readonly INotificador _notificador;

    protected BaseService(INotificador notificador)
    {
        _notificador = notificador;
    }

    protected void Notificar(ValidationResult validationResult)
    {
        foreach (var erro in validationResult.Errors)
        {
            Notificar(erro.ErrorMessage);
        }
    }

    protected void Notificar(string mensagem)
    {
        _notificador.AdicionarNotificacao(new Notificacao(mensagem));
    }

    protected bool TemNotificacao()
    {
        return _notificador.TemNotificacao();
    }

    protected bool ExecutarValidacao<TValidador, TEntidade>(TValidador validador, TEntidade entidade)
        where TValidador : AbstractValidator<TEntidade>
    {
        var resultado = validador.Validate(entidade);

        if (resultado.IsValid)
            return true;

        Notificar(resultado);
        return false;
    }
}
