using FitAgenda.Api.Dtos.TipoAula;
using FitAgenda.Api.Mappers;
using FitAgenda.Api.Views.Common;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitAgenda.Api.Controllers;

[Route("api/tipo-aula")]
[Produces("application/json")]
public class TiposAulaController : BaseController
{
    private readonly ITipoAulaRepository _tipoAulaRepository;
    private readonly ITipoAulaService _tipoAulaService;

    public TiposAulaController(
        ITipoAulaRepository tipoAulaRepository,
        ITipoAulaService tipoAulaService,
        INotificador notificador) : base(notificador)
    {
        _tipoAulaRepository = tipoAulaRepository;
        _tipoAulaService = tipoAulaService;
    }

    [HttpGet("ListarTiposDeAula", Name = "ListarTiposDeAula")]
    [ProducesResponseType(typeof(ApiSuccessResponse<IEnumerable<TipoAulaDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarTiposDeAula()
    {
        var tiposAula = await _tipoAulaRepository.ObterTodos();
        var resultado = tiposAula.Select(TipoAulaMapper.ParaDto);

        return CustomResponse(HttpStatusCode.OK, resultado);
    }

    [HttpGet("ObterTipoAulaPorId/{id:guid}", Name = "ObterTipoAulaPorId")]
    [ProducesResponseType(typeof(ApiSuccessResponse<TipoAulaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterTipoAulaPorId(Guid id)
    {
        var tipoAula = await _tipoAulaRepository.ObterPorId(id);

        if (tipoAula is null)
        {
            NotificarErro("Tipo de aula nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse(HttpStatusCode.OK, TipoAulaMapper.ParaDto(tipoAula));
    }

    [HttpPost("CadastrarTipoDeAula", Name = "CadastrarTipoDeAula")]
    [ProducesResponseType(typeof(ApiSuccessResponse<TipoAulaDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarTipoDeAula([FromBody] CadastrarTipoAulaDto dto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var tipoAula = new TipoAula(dto.Nome, dto.Ativo);
        var tiposExistentes = await _tipoAulaRepository.ObterTodos();

        _tipoAulaService.Criar(tipoAula, tiposExistentes);

        if (!OperacaoValida())
            return CustomResponse(HttpStatusCode.BadRequest);

        await _tipoAulaRepository.Adicionar(tipoAula);

        return CustomResponse(HttpStatusCode.Created, TipoAulaMapper.ParaDto(tipoAula));
    }

    [HttpPut("AtualizarTipoDeAula/{id:guid}", Name = "AtualizarTipoDeAula")]
    [ProducesResponseType(typeof(ApiSuccessResponse<TipoAulaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarTipoDeAula(Guid id, [FromBody] AtualizarTipoAulaDto dto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var tipoAula = await _tipoAulaRepository.ObterPorId(id);

        if (tipoAula is null)
        {
            NotificarErro("Tipo de aula nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        tipoAula.Renomear(dto.Nome);
        tipoAula.DefinirStatus(dto.Ativo);

        var tiposExistentes = await _tipoAulaRepository.ObterTodos();

        _tipoAulaService.Atualizar(tipoAula, tiposExistentes);

        if (!OperacaoValida())
            return CustomResponse(HttpStatusCode.BadRequest);

        await _tipoAulaRepository.Atualizar(tipoAula);

        return CustomResponse(HttpStatusCode.OK, TipoAulaMapper.ParaDto(tipoAula));
    }
}
