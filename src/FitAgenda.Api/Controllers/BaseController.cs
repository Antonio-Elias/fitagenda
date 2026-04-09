using FitAgenda.Api.Views.Common;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;
using System.Text.Json;

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
        var mensagens = new HashSet<string>();

        foreach (var entrada in modelState)
        {
            foreach (var erro in entrada.Value.Errors)
            {
                var mensagem = ObterMensagemErroModelState(entrada.Key, erro);

                if (!string.IsNullOrWhiteSpace(mensagem))
                    mensagens.Add(mensagem);
            }
        }

        foreach (var mensagem in mensagens)
        {
            NotificarErro(mensagem);
        }
    }

    protected void NotificarErro(string mensagem)
    {
        _notificador.AdicionarNotificacao(new Notificacao(mensagem));
    }

    private static string? ObterMensagemErroModelState(string chaveCampo, ModelError erro)
    {
        if (erro.Exception is JsonException jsonException)
        {
            var campo = ObterNomeCampo(jsonException.Path) ?? ObterNomeCampo(chaveCampo);

            return string.IsNullOrWhiteSpace(campo)
                ? "O corpo da requisicao esta invalido."
                : $"O valor informado para o campo '{campo}' e invalido.";
        }

        if (string.IsNullOrWhiteSpace(erro.ErrorMessage))
            return null;

        if (EhMensagemErroJson(erro.ErrorMessage))
        {
            var campo = ObterNomeCampoExtraiDoTexto(erro.ErrorMessage) ?? ObterNomeCampo(chaveCampo);

            return string.IsNullOrWhiteSpace(campo)
                ? "O corpo da requisicao esta invalido."
                : $"O valor informado para o campo '{campo}' e invalido.";
        }

        if (erro.ErrorMessage.Contains("field is required", StringComparison.OrdinalIgnoreCase))
        {
            var campo = ObterNomeCampo(chaveCampo);

            if (string.IsNullOrWhiteSpace(campo) ||
                campo.Equals("dto", StringComparison.OrdinalIgnoreCase) ||
                campo.Equals("request", StringComparison.OrdinalIgnoreCase))
            {
                return "O corpo da requisicao e obrigatorio.";
            }

            return $"O campo '{campo}' e obrigatorio.";
        }

        return erro.ErrorMessage;
    }

    private static bool EhMensagemErroJson(string mensagem)
    {
        return mensagem.Contains("invalid start of a value", StringComparison.OrdinalIgnoreCase) ||
               mensagem.Contains("could not be converted", StringComparison.OrdinalIgnoreCase) ||
               mensagem.Contains("is not valid json", StringComparison.OrdinalIgnoreCase) ||
               mensagem.Contains("Path: $.", StringComparison.OrdinalIgnoreCase);
    }

    private static string? ObterNomeCampo(string? caminho)
    {
        if (string.IsNullOrWhiteSpace(caminho))
            return null;

        var campo = caminho.Trim();

        if (campo.StartsWith("$.", StringComparison.Ordinal))
            campo = campo[2..];

        var ultimaParte = campo.Split('.', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();

        return string.IsNullOrWhiteSpace(ultimaParte) ? null : ultimaParte;
    }

    private static string? ObterNomeCampoExtraiDoTexto(string mensagem)
    {
        var indicePath = mensagem.IndexOf("Path: $.", StringComparison.OrdinalIgnoreCase);

        if (indicePath < 0)
            return null;

        var trecho = mensagem[(indicePath + "Path: ".Length)..];
        var fimCampo = trecho.IndexOfAny([' ', '|', '\r', '\n']);

        if (fimCampo >= 0)
            trecho = trecho[..fimCampo];

        return ObterNomeCampo(trecho);
    }
}
