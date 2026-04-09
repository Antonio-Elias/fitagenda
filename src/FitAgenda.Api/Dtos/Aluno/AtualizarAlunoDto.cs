using FitAgenda.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitAgenda.Api.Dtos.Aluno;

public class AtualizarAlunoDto
{
    [Required(ErrorMessage = "O nome do aluno e obrigatorio.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "O nome do aluno deve ter entre 2 e 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O tipo de plano e obrigatorio.")]
    [EnumDataType(typeof(TipoPlano), ErrorMessage = "O tipo de plano informado e invalido.")]
    public TipoPlano TipoPlano { get; set; }

    public bool Ativo { get; set; }
}
