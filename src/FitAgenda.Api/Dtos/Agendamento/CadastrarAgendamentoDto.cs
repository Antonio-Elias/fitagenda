using System.ComponentModel.DataAnnotations;

namespace FitAgenda.Api.Dtos.Agendamento;

public class CadastrarAgendamentoDto
{
    [Required(ErrorMessage = "O codigo do aluno e obrigatorio.")]
    public Guid CodigoAluno { get; set; }

    [Required(ErrorMessage = "O codigo da aula e obrigatorio.")]
    public Guid CodigoAula { get; set; }
}
