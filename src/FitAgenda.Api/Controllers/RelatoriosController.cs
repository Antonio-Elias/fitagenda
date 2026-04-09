using FitAgenda.Api.Dtos.Relatorio;
using FitAgenda.Api.Mappers;
using FitAgenda.Api.Views.Common;
using FitAgenda.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitAgenda.Api.Controllers;

[Route("api/relatorio")]
[Produces("application/json")]
public class RelatoriosController : BaseController
{
    private readonly IRelatorioService _relatorioService;

    public RelatoriosController(
        IRelatorioService relatorioService,
        INotificador notificador) : base(notificador)
    {
        _relatorioService = relatorioService;
    }

    [HttpGet("ObterRelatorioMensalAluno/{id:guid}", Name = "ObterRelatorioMensalAluno")]
    [ProducesResponseType(typeof(ApiSuccessResponse<RelatorioMensalAlunoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterRelatorioMensalAluno(Guid id, [FromQuery] int mes, [FromQuery] int ano)
    {
        var relatorio = await _relatorioService.ObterRelatorioMensalAluno(id, mes, ano);

        if (!OperacaoValida())
            return CustomResponse(HttpStatusCode.BadRequest);

        if (relatorio is null)
        {
            NotificarErro("Relatorio nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse(HttpStatusCode.OK, RelatorioMapper.ParaDto(relatorio));
    }
}
