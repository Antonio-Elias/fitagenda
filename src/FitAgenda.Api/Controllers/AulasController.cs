using FitAgenda.Api.Dtos.Aula;
using FitAgenda.Api.Mappers;
using FitAgenda.Api.Views.Common;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitAgenda.Api.Controllers;

[Route("api/aula")]
[Produces("application/json")]
public class AulasController : BaseController
{
    private readonly IAulaRepository _aulaRepository;
    private readonly IAulaService _aulaService;
    private readonly ITipoAulaRepository _tipoAulaRepository;

    public AulasController(
        IAulaRepository aulaRepository,
        IAulaService aulaService,
        ITipoAulaRepository tipoAulaRepository,
        INotificador notificador) : base(notificador)
    {
        _aulaRepository = aulaRepository;
        _aulaService = aulaService;
        _tipoAulaRepository = tipoAulaRepository;
    }

    [HttpGet("ListarAulas", Name = "ListarAulas")]
    [ProducesResponseType(typeof(ApiSuccessResponse<IEnumerable<AulaDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarAulas()
    {
        var aulas = await _aulaRepository.ObterAulasComTipo();
        var resultado = aulas.Select(AulaMapper.ParaDto);

        return CustomResponse(HttpStatusCode.OK, resultado);
    }

    [HttpGet("ObterAulaPorId/{id:guid}", Name = "ObterAulaPorId")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AulaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterAulaPorId(Guid id)
    {
        var aula = await _aulaRepository.ObterAulaComTipo(id);

        if (aula is null)
        {
            NotificarErro("Aula nao encontrada.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse(HttpStatusCode.OK, AulaMapper.ParaDto(aula));
    }

    [HttpPost("CadastrarAula", Name = "CadastrarAula")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AulaDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CadastrarAula([FromBody] CadastrarAulaDto dto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var tipoAula = await _tipoAulaRepository.ObterPorId(dto.CodigoTipoAula);

        if (tipoAula is null)
        {
            NotificarErro("Tipo de aula nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        var aula = new Aula(dto.CodigoTipoAula, dto.DataHora, dto.CapacidadeMaxima);

        _aulaService.Criar(aula, tipoAula);

        if (!OperacaoValida())
            return CustomResponse(HttpStatusCode.BadRequest);

        await _aulaRepository.Adicionar(aula);

        var aulaCriada = await _aulaRepository.ObterAulaComTipo(aula.Id);

        return CustomResponse(HttpStatusCode.Created, aulaCriada is null ? null : AulaMapper.ParaDto(aulaCriada));
    }

    [HttpPut("AtualizarAula/{id:guid}", Name = "AtualizarAula")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AulaDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarAula(Guid id, [FromBody] AtualizarAulaDto dto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var aula = await _aulaRepository.ObterAulaComTipo(id);

        if (aula is null)
        {
            NotificarErro("Aula nao encontrada.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        var tipoAula = await _tipoAulaRepository.ObterPorId(dto.CodigoTipoAula);

        if (tipoAula is null)
        {
            NotificarErro("Tipo de aula nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        aula.DefinirTipoAula(dto.CodigoTipoAula);
        aula.AtualizarDataHora(dto.DataHora);
        aula.DefinirCapacidadeMaxima(dto.CapacidadeMaxima);

        if (dto.Ativa)
            aula.Ativar();
        else
            aula.Inativar();

        _aulaService.Atualizar(aula, tipoAula);

        if (!OperacaoValida())
            return CustomResponse(HttpStatusCode.BadRequest);

        await _aulaRepository.Atualizar(aula);

        var aulaAtualizada = await _aulaRepository.ObterAulaComTipo(aula.Id);

        return CustomResponse(HttpStatusCode.OK, aulaAtualizada is null ? null : AulaMapper.ParaDto(aulaAtualizada));
    }
}
