namespace FitAgenda.Api.Dtos.Agendamento;

public class AgendamentoDto
{
    public Guid Id { get; set; }
    public Guid CodigoAluno { get; set; }
    public string NomeAluno { get; set; } = string.Empty;
    public Guid CodigoAula { get; set; }
    public DateTime DataHoraAula { get; set; }
    public string NomeTipoAula { get; set; } = string.Empty;
    public DateTime DataAgendamento { get; set; }
    public bool Ativo { get; set; }
}
