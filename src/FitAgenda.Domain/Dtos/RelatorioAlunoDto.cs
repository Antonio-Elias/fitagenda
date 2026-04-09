namespace FitAgenda.Domain.Dtos;

public class RelatorioAlunoDto
{
    public Guid CodigoAluno { get; set; }
    public string NomeAluno { get; set; } = string.Empty;
    public int MesReferencia { get; set; }
    public int AnoReferencia { get; set; }
    public int TotalAulasAgendadasNoMes { get; set; }
    public List<TipoAulaFrequenciaDto> TiposAulaMaisFrequentes { get; set; } = [];
}
