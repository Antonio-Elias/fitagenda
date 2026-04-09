using System.ComponentModel.DataAnnotations;

namespace FitAgenda.Api.Dtos.TipoAula;

public class AtualizarTipoAulaDto
{
    [Required(ErrorMessage = "O nome do tipo de aula e obrigatorio.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "O nome do tipo de aula deve ter entre 2 e 150 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    public bool Ativo { get; set; }
}
