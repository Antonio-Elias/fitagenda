namespace FitAgenda.Api.Dtos.Relatorio;

public class RelatorioMensalAlunoDto
{
    public Guid CodigoAluno { get; set; }
    public string NomeAluno { get; set; } = string.Empty;
    public int MesReferencia { get; set; }
    public int AnoReferencia { get; set; }
    public int TotalAulasAgendadasNoMes { get; set; }
    public List<TipoAulaFrequenciaDto> TiposAulaMaisFrequentes { get; set; } = [];
}
