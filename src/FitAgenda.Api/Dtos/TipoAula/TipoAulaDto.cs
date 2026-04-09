namespace FitAgenda.Api.Dtos.TipoAula;

public class TipoAulaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
