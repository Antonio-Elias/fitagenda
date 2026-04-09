using System.ComponentModel.DataAnnotations;

namespace FitAgenda.Api.Dtos.Aula;

public class CadastrarAulaDto
{
    [Required(ErrorMessage = "O codigo do tipo de aula e obrigatorio.")]
    public Guid CodigoTipoAula { get; set; }

    [Required(ErrorMessage = "A data e hora da aula e obrigatoria.")]
    public DateTime DataHora { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "A capacidade maxima deve ser maior que zero.")]
    public int CapacidadeMaxima { get; set; }
}
