using FitAgenda.Api.Views.Common;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace FitAgenda.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    private readonly INotificador _notificador;

    protected BaseController(INotificador notificador)
    {
        _notificador = notificador;
    }

    protected bool OperacaoValida()
    {
        return !_notificador.TemNotificacao();
    }

    protected ActionResult CustomResponse(HttpStatusCode statusCode = HttpStatusCode.OK, object? result = null)
    {
        if (OperacaoValida())
        {
            return new ObjectResult(new ApiSuccessResponse<object>
            {
                Sucesso = true,
                Dados = result
            })
            {
                StatusCode = (int)statusCode
            };
        }

        var errorStatusCode = (int)statusCode < 400
            ? HttpStatusCode.BadRequest
            : statusCode;

        return new ObjectResult(new ApiErrorResponse
        {
            Sucesso = false,
            Erros = _notificador.ObterNotificacoes().Select(notificacao => notificacao.Mensagem)
        })
        {
            StatusCode = (int)errorStatusCode
        };
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
            NotificarErroModelInvalida(modelState);

        return CustomResponse(HttpStatusCode.BadRequest);
    }

    protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(entrada => entrada.Errors);

        foreach (var erro in erros)
        {
            var mensagem = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotificarErro(mensagem);
        }
    }

    protected void NotificarErro(string mensagem)
    {
        _notificador.AdicionarNotificacao(new Notificacao(mensagem));
    }
}
