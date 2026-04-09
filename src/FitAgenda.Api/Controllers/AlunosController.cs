using FitAgenda.Api.Dtos.Aluno;
using FitAgenda.Api.Mappers;
using FitAgenda.Api.Views.Common;
using FitAgenda.Domain.Interfaces;
using FitAgenda.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FitAgenda.Api.Controllers;

[Route("api/aluno")]
[Produces("application/json")]
public class AlunosController : BaseController
{
    private readonly IAlunoRepository _alunoRepository;
    private readonly IAlunoService _alunoService;

    public AlunosController(
        IAlunoRepository alunoRepository,
        IAlunoService alunoService,
        INotificador notificador) : base(notificador)
    {
        _alunoRepository = alunoRepository;
        _alunoService = alunoService;
    }

    [HttpGet("ListarAlunos", Name = "ListarAlunos")]
    [ProducesResponseType(typeof(ApiSuccessResponse<IEnumerable<AlunoDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarAlunos()
    {
        var alunos = await _alunoRepository.ObterTodos();
        var resultado = alunos.Select(AlunoMapper.ParaDto);

        return CustomResponse(HttpStatusCode.OK, resultado);
    }

    [HttpGet("ObterAlunoPorId/{id:guid}", Name = "ObterAlunoPorId")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AlunoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterAlunoPorId(Guid id)
    {
        var aluno = await _alunoRepository.ObterPorId(id);

        if (aluno is null)
        {
            NotificarErro("Aluno nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        return CustomResponse(HttpStatusCode.OK, AlunoMapper.ParaDto(aluno));
    }

    [HttpPost("CadastrarAluno", Name = "CadastrarAluno")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AlunoDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CadastrarAluno([FromBody] CadastrarAlunoDto dto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var aluno = new Aluno(dto.Nome, dto.TipoPlano);

        _alunoService.Criar(aluno);

        if (!OperacaoValida())
            return CustomResponse(HttpStatusCode.BadRequest);

        await _alunoRepository.Adicionar(aluno);

        return CustomResponse(HttpStatusCode.Created, AlunoMapper.ParaDto(aluno));
    }

    [HttpPut("AtualizarAluno/{id:guid}", Name = "AtualizarAluno")]
    [ProducesResponseType(typeof(ApiSuccessResponse<AlunoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarAluno(Guid id, [FromBody] AtualizarAlunoDto dto)
    {
        if (!ModelState.IsValid)
            return CustomResponse(ModelState);

        var aluno = await _alunoRepository.ObterPorId(id);

        if (aluno is null)
        {
            NotificarErro("Aluno nao encontrado.");
            return CustomResponse(HttpStatusCode.NotFound);
        }

        aluno.AtualizarNome(dto.Nome);
        aluno.AlterarPlano(dto.TipoPlano);

        if (dto.Ativo)
            aluno.Ativar();
        else
            aluno.Inativar();

        _alunoService.Atualizar(aluno);

        if (!OperacaoValida())
            return CustomResponse(HttpStatusCode.BadRequest);

        await _alunoRepository.Atualizar(aluno);

        return CustomResponse(HttpStatusCode.OK, AlunoMapper.ParaDto(aluno));
    }
}
