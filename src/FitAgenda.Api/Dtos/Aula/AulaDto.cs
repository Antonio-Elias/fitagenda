namespace FitAgenda.Api.Dtos.Aula;

public class AulaDto
{
    public Guid Id { get; set; }
    public Guid CodigoTipoAula { get; set; }
    public string NomeTipoAula { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
    public int CapacidadeMaxima { get; set; }
    public bool Ativa { get; set; }
}
