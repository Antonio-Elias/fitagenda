using FitAgenda.Api.Dtos.Agendamento;
using FitAgenda.Api.Mappers;
using FitAgenda.Api.Views.Common;
using FitAgenda.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitAgenda.Api.Controllers;

[Route("api/agendamento")]
[Produces("application/json")]
public class AgendamentosController : BaseController
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly IAgendamentoService _agendamentoService;

    public AgendamentosController(
        IAgendamentoRepository agendamentoRepository,
        IAgendamentoService agendamentoService,
        INotificador notificador) : base(notificador)
    {
        _agendamentoRepository = agendamentoRepository;
        _agendamentoService = agendamentoService;
    }

    [HttpGet("ListarAgendamentos", Name = "ListarAgendamentos")]
    [ProducesResponseType(typeof(ApiSuccessResponse<IEnumerable<AgendamentoDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarAgendamentos()
    {
        var agendamentos = await _agendamentoRepository.ObterTodosComDetalhes();
        var resultado = agendamentos.Select(AgendamentoMapper.ParaDto);

        return CustomResponse(HttpStatusCode.OK, resultado);
    }

    [HttpGet("ObterAgendamentoPorId/{id:guid}", Name = "ObterAgendamentoPorId")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AgendamentoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterAgendamentoPorId(Guid id)
    {
        var agendamento = await _agendamentoRepository.ObterPorIdComDetalhes(id);

        if (agendamento is null)
        {
            NotificarErro("Agendamento nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse(HttpStatusCode.OK, AgendamentoMapper.ParaDto(agendamento));
    }

    [HttpPost("CadastrarAgendamento", Name = "CadastrarAgendamento")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AgendamentoDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CadastrarAgendamento([FromBody] CadastrarAgendamentoDto dto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var aluno = await _agendamentoRepository.ObterAlunoComAgendamentos(dto.CodigoAluno);

        if (aluno is null)
        {
            NotificarErro("Aluno nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        var aula = await _agendamentoRepository.ObterAulaComAgendamentos(dto.CodigoAula);

        if (aula is null)
        {
            NotificarErro("Aula nao encontrada.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        var agendamento = _agendamentoService.Agendar(aluno, aula);

        if (!OperacaoValida() || agendamento is null)
            return CustomResponse(HttpStatusCode.BadRequest);

        await _agendamentoRepository.Adicionar(agendamento);

        var agendamentoCriado = await _agendamentoRepository.ObterPorIdComDetalhes(agendamento.Id);

        return CustomResponse(HttpStatusCode.Created, agendamentoCriado is null ? null : AgendamentoMapper.ParaDto(agendamentoCriado));
    }

    [HttpPut("CancelarAgendamento/{id:guid}", Name = "CancelarAgendamento")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AgendamentoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelarAgendamento(Guid id)
    {
        var agendamento = await _agendamentoRepository.ObterPorIdComDetalhes(id);

        if (agendamento is null)
        {
            NotificarErro("Agendamento nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        _agendamentoService.Cancelar(agendamento);

        if (!OperacaoValida())
            return CustomResponse(HttpStatusCode.BadRequest);

        await _agendamentoRepository.Atualizar(agendamento);

        return CustomResponse(HttpStatusCode.OK, AgendamentoMapper.ParaDto(agendamento));
    }
}
